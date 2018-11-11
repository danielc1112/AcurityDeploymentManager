using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System.Diagnostics;
using Microsoft.Win32;
//Need Office installed before finding in reference list
//using Microsoft.Office.Interop.Word;

namespace SMAReportCleaner
{
    public enum ScrollBarType : uint
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }

    public enum Message : uint
    {
        WM_VSCROLL = 0x0115
    }

    public enum ScrollBarCommands : uint
    {
        SB_THUMBPOSITION = 4
    }

    public enum FileDifferenceState
    {
        Unknown,
        Same, //green
        Different, //orange/pink
        Missing //red
    };

    public partial class Main : Form
    {
        //Contains the prod tab information
        private List<FileDifferenceTab> FileTabs = new List<FileDifferenceTab>();
        //These are used to colour the master listbox
        private List<MasterFile> masterFiles = new List<MasterFile>();
        //Re-calculated every time the prod tab changes, or initially loaded, for text comparison.
        private SideBySideDiffBuilder diffBuilder = new SideBySideDiffBuilder(new Differ());

        private bool masterHasChanged = false;
        private bool loading = false;
        private Stats stats;
        private RichTextBox selectedRB = null;

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            loading = true;

            rbMaster.MouseDown += RBHelper.rb_MouseDown;
            Config.InitialiseSettings();
            Config.FileType = FileType.Reports; 
            cbFileType.SelectedIndex = 0; //This will trigger ResetUI()
            tcFiles.DrawMode = TabDrawMode.OwnerDrawFixed;
            lbMaster.DrawMode = DrawMode.OwnerDrawFixed;

            tbFont.Value = 10;

            LinkLabel.Link sLink = new LinkLabel.Link();
            sLink.LinkData = "https://onevue.sharepoint.com/it/SitePages/OV%20Super%20Services.aspx";
            lblSharepoint.Links.Add(sLink);

            LinkLabel.Link iLink = new LinkLabel.Link();
            iLink.LinkData = "https://onevue.atlassian.net/projects/SSKB/issues/";
            lblSSKB.Links.Add(iLink);

            LinkLabel.Link irLink = new LinkLabel.Link();
            irLink.LinkData = "NotSureOfThisWebAddress";
            lblIress.Links.Add(irLink);

            tbFont.SendToBack();

            loading = false;
        }

        //Reload everything
        private void ResetUI(bool ClickSettings = true)
        {
            loading = true;

            //Clear everything
            ClearAllTextBoxes();
            tcFiles.TabPages.Clear();
            FileTabs.Clear();            
            lbMaster.Items.Clear();
            masterFiles.Clear();

            masterHasChanged = false;
            EnableMasterSaveAndUndo();

            lblMaster.Text = "Master";
            lblMasterMod.Text = "";
            lblMasterSize.Text = "";
            lblProdMod.Text = "";
            lblProdSize.Text = "";

            //Check settings
            bool settingsValid = Config.ValidateAllFolderSettings() && Config.ValidateAllBackupSettings();
            if (settingsValid)
            {
                LoadMasterFiles();
                CreateTabs();
                CompareAll();
                if (lbMaster.Items.Count > 0)
                   lbMaster.SelectedIndex = 0; //Will trigger event
                EnableControls(true);
            }
            else
            {
                EnableControls(false);
                if (ClickSettings)
                    btnSettings.PerformClick();
            }

            loading = false;
            rbMaster_VScroll(this, null);
        }

        //Used in ResetUI()
        private void EnableControls(bool value)
        {
            btnRefresh.Enabled = value;
            btnMissingFiles.Enabled = value;
            if (Config.FileType == FileType.XMLUI)
            {
                btnMissingFiles.Enabled = false;
            }
        }

        //Used in ResetUI()
        private void LoadMasterFiles()
        {
            string masterFolder = Config.ReadSetting(Config.FileTypePrefix() + Config.MasterLabel);

            DirectoryInfo di = new DirectoryInfo(masterFolder);
            FileInfo[] files;

            if (Config.FileType == FileType.XMLUI)
            {
                files = di.GetFiles("Hierarchy.xml", SearchOption.AllDirectories);
            }
            else
            {
                files = di.GetFiles();
            }

            foreach (FileInfo f in files)
            {
                masterFiles.Add(new MasterFile(f.FullName, f.Name.ToUpper(), f.LastWriteTime));               
            }
            OrderBy();
        }

        //Used in ResetUI()
        private void CreateTabs()
        {
            string[] allKeys = Config.AllSettings();
            for (int i = 0; i < allKeys.Length; i++)
            {
                FileDifferenceTab fdt;
                string key = allKeys[i];
                string label = Config.LabelFromKey(key);
                if (!label.StartsWith(Config.MasterLabel) && key.StartsWith(Config.FileTypePrefix()) && !key.EndsWith(Config.BackupSuffix))
                {
                    fdt = new FileDifferenceTab(label, Config.FileType);
                    fdt.rb.TextChanged      += rbProd_TextChanged;
                    fdt.rb.SelectionChanged += rbProd_SelectionChanged;
                    fdt.rb.FontChanged      += rbProd_FontChanged;
                    fdt.rb.VScroll          += rbProd_VScroll;
                    fdt.rb.KeyDown          += rbProd_KeyDown;
                    fdt.rb.MouseDown        += RBHelper.rb_MouseDown;
                    FileTabs.Add(fdt);
                    tcFiles.TabPages.Add(fdt.tp);
                }
            }
        }

        //Used in ResetUI()
        private void CompareAll()
        {
            stats = new Stats();
            lbMaster.Invalidate();

            pbProgress.Maximum = masterFiles.Count();
            pbProgress.Value = 0;
            lblMaster.Visible = false;
            pbProgress.Visible = true;
            pbProgress.BringToFront();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //Using parallel processing with 6 tasks, is more than double the speed
            Task task1 = null;
            Task task2 = null;
            Task task3 = null;
            Task task4 = null;
            Task task5 = null;
            Task task6 = null;

            int fileCount = masterFiles.Count();
            for (int i = 0; i < fileCount; i+=6)
            {
                int index;

                index = i;
                if (index < fileCount)
                {
                    if (task1 != null)
                        task1.Wait();
                    task1 = Task.Factory.StartNew((x) => { DetermineFileDifference((int)x); }, index);
                    //pbProgress.Value += 1;
                }

                index = i + 1;
                if (index < fileCount)
                {
                    if (task2 != null)
                        task2.Wait();
                    task2 = Task.Factory.StartNew((x) => { DetermineFileDifference((int)x); }, index);
                    //pbProgress.Value += 1;
                }

                index = i + 2;
                if (index < fileCount)
                {
                    if (task3 != null)
                        task3.Wait();
                    task3 = Task.Factory.StartNew((x) => { DetermineFileDifference((int)x); }, index);
                    //pbProgress.Value += 1;
                }

                index = i + 3;
                if (index < fileCount)
                {
                    if (task4 != null)
                        task4.Wait();
                    task4 = Task.Factory.StartNew((x) => { DetermineFileDifference((int)x); }, index);
                    //pbProgress.Value += 1;
                }

                index = i + 4;
                if (index < fileCount)
                {
                    if (task5 != null)
                        task5.Wait();
                    task5 = Task.Factory.StartNew((x) => { DetermineFileDifference((int)x); }, index);
                    //pbProgress.Value += 1;
                }

                index = i + 5;
                if (index < fileCount)
                {
                    if (task6 != null)
                        task6.Wait();
                    task6 = Task.Factory.StartNew((x) => { DetermineFileDifference((int)x); }, index);
                    //pbProgress.Value += 1;
                }
                if(pbProgress.Value + 6 <= pbProgress.Maximum)
                   pbProgress.Value += 6;
            }

            if (task1 != null)
                task1.Wait();
            if (task2 != null)
                task2.Wait();
            if (task3 != null)
                task3.Wait();
            if (task4 != null)
                task4.Wait();
            if (task5 != null)
                task5.Wait();
            if (task6 != null)
                task6.Wait();

            sw.Stop();
            stats.elapsedTime = sw.Elapsed;
            pbProgress.Visible = false;
            lblMaster.Visible = true;
        }

        //Used in CompareAll()
        //This function breaks out as early as possible
        private void DetermineFileDifference(int index)
        {
            bool differenceFound = false;
            bool missingFound = false;

            string fileName = lbMaster.Items[index].ToString();
            string fullMasterFileName = masterFiles[index].fullFileName;

            //Keep the master file open while we open and close all the prod files
            FileStream masterFile = new FileStream(fullMasterFileName, FileMode.Open, FileAccess.Read);
            try
            {
                foreach (FileDifferenceTab fdt in FileTabs)
                {
                    string fullProdFileName = GetProdFullFileName(fdt.label, fileName, index);

                    FileDifferenceState fds = FileCompareToMaster(masterFile, fullProdFileName);
                    //If we have found one missing file, label the master file a state of missing
                    if (fds == FileDifferenceState.Missing)
                    {
                        missingFound = true;
                        break;
                    }
                    else if (fds == FileDifferenceState.Different)
                    {
                        differenceFound = true;
                    }
                }
            }
            finally
            {
                masterFile.Close();
            }

            if (missingFound)
            {
                stats.missingFiles++;
                masterFiles[index].fds = FileDifferenceState.Missing;
            }
            else if (differenceFound)
            {
                stats.differentFiles++;
                masterFiles[index].fds = FileDifferenceState.Different;
            }
            else
            {
                stats.sameFiles++;
                masterFiles[index].fds = FileDifferenceState.Same;
            }
        }

        private void FullDetermineFileDifference()
        {
            int selectedIndex = lbMaster.SelectedIndex;
            MasterFile mf = masterFiles[selectedIndex];

            bool differenceFound = false;
            bool missingFound = false;

            string fileName = mf.fileName;
            string fullMasterFileName = mf.fullFileName;

            //Keep the master file open while we open and close all the prod files
            FileStream masterFile = new FileStream(fullMasterFileName, FileMode.Open, FileAccess.Read);
            try
            {
                foreach (FileDifferenceTab fdt in FileTabs)
                {
                    fdt.fds = FileCompareToMaster(masterFile, fdt.fullProdFileName);

                    if (fdt.fds == FileDifferenceState.Missing)
                    {
                        missingFound = true;
                    }
                    else if (fdt.fds == FileDifferenceState.Different)
                    {
                        differenceFound = true;
                    }
                }
            }
            finally
            {
                masterFile.Close();
            }

            if (missingFound)
                mf.fds = FileDifferenceState.Missing;
            else if (differenceFound)
                mf.fds = FileDifferenceState.Different;
            else
                mf.fds = FileDifferenceState.Same;

            tcFiles.Invalidate();
            lbMaster.Invalidate();
        }

        private string GetProdFullFileName(string label, string fileName, int index)
        {
            string fullMasterFileName = masterFiles[index].fullFileName;
            string prodFolder = Config.ReadSetting(Config.FileTypePrefix() + label);
            string fullProdFileName;
            if (Config.FileType == FileType.XMLUI)
            {
                //In this case prodFolder is \xmlui\. So we need to add last directory as well i.e. \xmlui\StandardAdminstration\
                FileInfo fi = new FileInfo(fullMasterFileName);
                string dirName = new DirectoryInfo(fi.DirectoryName).Name;
                fullProdFileName = prodFolder + "\\" + dirName + "\\" + fileName;
            }
            else
            {
                fullProdFileName = prodFolder + "\\" + fileName;
            }
            return fullProdFileName;
        }

        //Used in DetermineFileDifference(). Compares the bytes. Fast.
        private FileDifferenceState FileCompareToMaster(FileStream masterFile, string prodFileName)
        {
            int masterByte;
            int prodByte;
            FileStream prodFile;

            //We are keeping the master file open. Reset the cursor back to start of file.
            masterFile.Position = 0;

            if (File.Exists(prodFileName))
            {
                prodFile = new FileStream(prodFileName, FileMode.Open, FileAccess.Read);
            }
            else
                return FileDifferenceState.Missing;

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (masterFile.Length != prodFile.Length)
            {
                // Close the file
                prodFile.Close();

                // Return false to indicate files are different
                return FileDifferenceState.Different;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // masterFile is reached.
            do
            {
                // Read one byte from each file.
                masterByte = masterFile.ReadByte();
                prodByte = prodFile.ReadByte();
            }
            while ((masterByte == prodByte) && (masterByte != -1));

            // Close the files.
            prodFile.Close();

            // Return the success of the comparison. "masterByte" is 
            // equal to "prodByte" at this point only if the files are 
            // the same.
            return ((masterByte - prodByte) == 0)? FileDifferenceState.Same : FileDifferenceState.Different;
        }

        //Used in ResetUI() and lbFiles_SelectedIndexChanged()
        private void ClearAllTextBoxes()
        {
            rbMaster.Clear();
            foreach (FileDifferenceTab fdt in FileTabs)
                fdt.rb.Clear();
        }

        //Used in lbFiles_SelectedIndexChanged()
        //Way to make faster, need to extend Richtextbox with the functions in RBHelper:
        //https://stackoverflow.com/questions/9418024/richtextbox-beginupdate-endupdate-extension-methods-not-working
        private void ShowFileInTextBox(string fullFileName, RichTextBox rb)
        {
            FileInfo file = new FileInfo(fullFileName);
            
            if (file == null || !file.Exists)
            {
                rb.Text = "Unable to locate file:\n" + fullFileName;
                rb.ForeColor = Config.MISSING_COLOUR;
                rb.ReadOnly = true;
            }
            else
            {                
                rb.Clear();
                rb.ForeColor = Color.Black;
                if ((file.Extension.ToUpper() == ".RTF") || (file.Extension.ToUpper() == ".DOC") || (file.Extension.ToUpper() == ".DOCX"))
                {
                    rb.LoadFile(file.FullName, RichTextBoxStreamType.RichText);
                }
                else
                {
                    rb.LoadFile(file.FullName, RichTextBoxStreamType.PlainText);
                }

                if ((Config.FileType == FileType.Customisations) || (Config.FileType == FileType.Templates))
                {
                    rb.ReadOnly = true;
                }

                int selectedIndex = lbMaster.SelectedIndex;
                string fullMasterFileName = masterFiles[selectedIndex].fullFileName;
                if (fullFileName == fullMasterFileName)
                {
                    lblMasterMod.Text = file.LastWriteTime.ToString();
                    lblMasterSize.Text = file.Length.ToString() + " bytes";
                }
            }
        }

        //Used in btnBackupFile_Click() and btnDeploy_Click()
        private bool MoveFileToBackup(string fileName, string label, int index, ref bool fileBackedup, bool returnTrueIfNotFoundAndDontShowWarning = false)
        {
            string from;
            string fullMasterFileName = masterFiles[index].fullFileName;
            if (label == Config.MasterLabel)
                from = fullMasterFileName;
            else
                from = GetProdFullFileName(label, fileName, index);

            string backupFolder = Config.ReadSetting(Config.FileTypePrefix() + label + Config.BackupSuffix);
            string to = backupFolder + "\\" + Config.ToBackupFileName(fileName);

            fileBackedup = false;

            if (File.Exists(from))
            {
                try
                {
                    File.Move(from, to); // Try to move
                    fileBackedup = true;
                    return true;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error moving file\n" + from + "\nto\n" + to + "\n: " + ex.Message,
                    "Error Backing Up File", //title
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                    return false;
                }
            }
            else if(!returnTrueIfNotFoundAndDontShowWarning)
            {
                MessageBox.Show("Warning:\n" + from + "\ndoes not exist",
                "Unable To Back Up File", //title
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
                return false;
            }
            return true;
        }

        //Used in btnDeploy_Click()
        private bool CopyFileToProd(string fileName, string label, int index)
        {
            string fullMasterFileName = masterFiles[index].fullFileName;
            string fullProdName = GetProdFullFileName(label, fileName, index);

            string from = fullMasterFileName;
            string to = fullProdName;

            if (File.Exists(from))
            {
                try
                {
                    File.Copy(from, to); // Try to copy
                    return true;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error copying " + from + " to " + to + ": " + ex.Message,
                    "Error Deploying File", //title
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                    return false;
                }
            }
            else
            {
                MessageBox.Show(from + " does not exist",
                "Error Deploying File", //title
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
                return false;
            }
        }

        private bool SaveFile(string fileName, string label, RichTextBox rb)
        {
            int index = lbMaster.SelectedIndex;
            string from;
            string fullMasterFileName = masterFiles[index].fullFileName;
            if (label == Config.MasterLabel)
                from = fullMasterFileName;
            else
                from = GetProdFullFileName(label, fileName, index);

            string backupFolder = Config.ReadSetting(Config.FileTypePrefix() + label + Config.BackupSuffix);
            FileInfo fi = new FileInfo(from);
            string folder = fi.DirectoryName;

            string fromRenamed = folder + "\\" + Config.ToBackupFileName(fileName);
            string to = backupFolder + "\\" + Config.ToBackupFileName(fileName);

            if (File.Exists(from))
            {
                try
                {
                    File.Copy(from, fromRenamed); // Try to copy and rename file
                    rb.SaveFile(from, RichTextBoxStreamType.PlainText); //Save file
                    File.Move(fromRenamed, to); // Try to backup old file
                    FullDetermineFileDifference();
                    tcFiles_SelectedIndexChanged(this, EventArgs.Empty); //This will trigger file diff
                    return true;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error backing up and saving file\n" + from + "\nto\n" + to + "\n: " + ex.Message + "\nPlease check the file exists in " + label,
                    "Error Saving File", //title
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Error:\n" + from + "\ndoes not exist. File not saved.",
                "Unable To Save File", //title
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
                return false;
            }
        }

        //--------------EVENTS------------------------

        //When selecting a different file
        private void lbMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            loading = true;

            WhoopsWarning(false);            
            ClearAllTextBoxes();

            masterHasChanged = false;
            EnableMasterSaveAndUndo();

            int selectedIndex = lbMaster.SelectedIndex;
            string fileName = "";
            string fullMasterFileName = "";
            if (selectedIndex != -1)
            {
                fullMasterFileName = masterFiles[selectedIndex].fullFileName;

                fileName = lbMaster.SelectedItem.ToString();
                if (Config.FileType == FileType.XMLUI)
                {
                    FileInfo fi = new FileInfo(fullMasterFileName);
                    string dirName = new DirectoryInfo(fi.DirectoryName).Name;
                    lblMaster.Text = "Master - " + dirName + "\\" + fileName;
                }
                else
                {
                    lblMaster.Text = "Master - " + fileName;
                }

                ShowFileInTextBox(fullMasterFileName, rbMaster);

                RBHelper.HighlightText(rbMaster);

                EnableMasterHistory(fileName);

                foreach (FileDifferenceTab ft in FileTabs)
                {
                    ft.hasChanged = false;
                    ft.fullProdFileName = GetProdFullFileName(ft.label, fileName, selectedIndex);
                    ShowFileInTextBox(ft.fullProdFileName, ft.rb);
                }

                FullDetermineFileDifference();
            }

            tcFiles_SelectedIndexChanged(null, EventArgs.Empty);
            loading = false;

            rbMaster.Select(0, 0);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (!UnSavedFileWarning("changing the settings"))
                return;
            Settings s = new Settings();
            s.ShowDialog(this);
            ResetUI(false);
        }

        private void BackupFileEverywhere()
        {
            if (Config.FileType == FileType.XMLUI || !UnSavedFileWarning("backing up"))
            {
                return;
            }

            if (Config.ValidateAllFolderSettings() && Config.ValidateAllBackupSettings())
            {
                string fileName = lbMaster.SelectedItem.ToString();
                int selectedIndex = lbMaster.SelectedIndex;
                string fullMasterFileName = masterFiles[selectedIndex].fullFileName;
                if (fileName != "")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to backup and remove\n" + fileName + "\nin Master and all prod environments?", "Backup File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        string successMoves = "";
                        bool masterMoved = false;

                        bool fileBackedUp = false;
                        if (MoveFileToBackup(fileName, Config.MasterLabel, selectedIndex, ref fileBackedUp))
                        {
                            if(fileBackedUp)
                            {
                                successMoves += "\n" + Config.MasterLabel;
                                if (lbMaster.SelectedIndex == lbMaster.Items.Count - 1)
                                    lbMaster.SelectedIndex--;
                                else
                                    lbMaster.SelectedIndex++;
                                masterMoved = true;
                            }                         
                        }

                        foreach (FileDifferenceTab fdt in FileTabs)
                        {
                            if (MoveFileToBackup(fileName, fdt.label, selectedIndex, ref fileBackedUp))
                            {
                                if (fileBackedUp)
                                {
                                    successMoves += "\n" + fdt.label;
                                }
                            }
                        }

                        if(masterMoved)
                            lbMaster.Items.Remove(fileName);

                        if (successMoves != "")
                            MessageBox.Show(fileName + " moved to backup folder in:\n" + successMoves);
                    }
                }
            }
        }

        //Used to colour the tab headings
        private void tcFiles_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tp = tcFiles.TabPages[e.Index];

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;  //optional

            // This is the rectangle to draw "over" the tabpage title
            RectangleF headerRect = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2);

            // This is the default colour to use for the non-selected tabs
            TabControl tc = sender as TabControl;
            SolidBrush sb = new SolidBrush(Color.FromKnownColor(KnownColor.Control));

            // Colour the header of the current tabpage based on what we did above
            g.FillRectangle(sb, e.Bounds);

            //Redraw the text
            if (FileTabs[e.Index].fds == FileDifferenceState.Missing)
                g.DrawString(tp.Text, tcFiles.Font, new SolidBrush(Config.TAB_MISSING_COLOUR), headerRect, sf);
            else if (FileTabs[e.Index].fds == FileDifferenceState.Different)
                g.DrawString(tp.Text, tcFiles.Font, new SolidBrush(Config.TAB_DIFFERENT_COLOUR), headerRect, sf);
            else if (FileTabs[e.Index].fds == FileDifferenceState.Same)
                g.DrawString(tp.Text, tcFiles.Font, new SolidBrush(Config.TAB_SAME_COLOUR), headerRect, sf);
            else
                g.DrawString(tp.Text, tcFiles.Font, new SolidBrush(Config.TAB_UNKNOWN_COLOUR), headerRect, sf);
        }
       
        private void lbMaster_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            e.ItemHeight = listBox.Font.Height;
        }

        //Used to colour the file names
        private void lbMaster_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            ListBox listBox = (ListBox)sender;
            e.DrawBackground();
            Brush myBrush = Brushes.Black; //pen color
            Font myFont = e.Font;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                myFont = new Font(myFont, FontStyle.Bold);
            }

            if ((masterFiles.Count != 0) && (masterFiles[e.Index].fds == FileDifferenceState.Missing))
            {
                e.Graphics.FillRectangle(new SolidBrush(Config.MISSING_COLOUR), e.Bounds);
            }
            else if ((masterFiles.Count != 0) && (masterFiles[e.Index].fds == FileDifferenceState.Different))
            {
                e.Graphics.FillRectangle(new SolidBrush(Config.DIFFERENT_COLOUR), e.Bounds);
            }
            else if ((masterFiles.Count != 0) && (masterFiles[e.Index].fds == FileDifferenceState.Same))
            {
                e.Graphics.FillRectangle(new SolidBrush(Config.SAME_COLOUR), e.Bounds);
            }
            else
            {
                myBrush = Brushes.Gray;
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            e.Graphics.DrawString(listBox.Items[e.Index].ToString(), myFont, myBrush, e.Bounds);
            e.DrawFocusRectangle();

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                Pen pen = new Pen(Color.Black);
                int y = e.Index * listBox.ItemHeight;
                e.Graphics.DrawRectangle(pen, 0, y, listBox.Width - 5, listBox.ItemHeight - 1);
            }
        }

        //Master vertical scrolling
        private void rbMaster_VScroll(object sender, EventArgs e)
        {   
            if(!loading)
            {
                MasterLineNumberTextBox.Text = "";
                AddLineNumbers(true);
                MasterLineNumberTextBox.Invalidate();

                int nPos = GetScrollPos(rbMaster.Handle, (int)ScrollBarType.SbVert);
                nPos <<= 16;
                uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;

                FileDifferenceTab ft = GetSelectedTab();
                if (ft != null)
                    SendMessage(ft.rb.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
            }
        }

        private void rbProd_VScroll(object sender, EventArgs e)
        {
            if (!loading)
            {
                ProdLineNumberTextBox.Text = "";
                AddLineNumbers(false);
                ProdLineNumberTextBox.Invalidate();
            }

            //Hard not to get into an infinite event loop here..
            //RichTextBox rb = (RichTextBox)sender;
            //int nPos = GetScrollPos(rb.Handle, (int)ScrollBarType.SbVert);
            //nPos <<= 16;
            //uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;

            //SendMessage(rbMaster.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }

        private bool UnSavedFileWarning(string str)
        {
            if(!loading && (masterHasChanged || (FileTabs.Where(x => x.hasChanged).Count() != 0)))
            {
                DialogResult result = MessageBox.Show("You have unsaved changes. Proceed?", "Unsaved Changes",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return result == DialogResult.Yes;
            }
            return true;
        }

        private void WhoopsWarning(bool prodOnlyCheck)
        {
            if (prodOnlyCheck)
            {
                if (FileTabs.Where(x => x.hasChanged).Count() != 0)
                {
                    DialogResult result = MessageBox.Show("Whoops! You forgot to save your changes.", "Gone",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (masterHasChanged || (FileTabs.Where(x => x.hasChanged).Count() != 0))
            {
                DialogResult result = MessageBox.Show("Whoops! You forgot to save your changes.", "Gone",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(UnSavedFileWarning("refreshing"))
               ResetUI();
        }

        private void btnMissingFiles_Click(object sender, EventArgs e)
        {
            if (Config.FileType == FileType.XMLUI || !UnSavedFileWarning("viewing missing files"))
            {
                return;
            }

            MissingFiles mf = new MissingFiles();
            //mf.BackColor = this.BackColor;
            mf.Text = "Missing Files - " + cbFileType.Text;
            mf.ShowDialog(this);
            ResetUI(false);
        }

        private void cbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
               WhoopsWarning(false);

            btnMasterNotepad.Text = "Notepad++";
            btnProdNotepad.Text = "Notepad++";
            if (cbFileType.Text == "Reports")
            {
                Config.FileType = FileType.Reports;
                BackColor = Config.REPORTS_COLOUR;
            }
            else if (cbFileType.Text == "Customisations")
            {
                Config.FileType = FileType.Customisations;
                BackColor = Config.CUSTOMISATIONS_COLOUR;
            }
            else if (cbFileType.Text == "Templates")
            {
                Config.FileType = FileType.Templates;
                BackColor = Config.TEMPLATES_COLOUR;
                btnMasterNotepad.Text = "Word";
                btnProdNotepad.Text = "Word";
            }
            else if (cbFileType.Text == "XMLUI")
            {
                Config.FileType = FileType.XMLUI;
                BackColor = Config.XMLUI_COLOUR;
            }
            ResetUI();
        }

        private void Deploy()
        {
            if(Config.FileType == FileType.XMLUI || !UnSavedFileWarning("deploying"))
            {
                return;
            }

            if (Config.ValidateAllFolderSettings() && Config.ValidateAllBackupSettings())
            {
                string fileName = lbMaster.SelectedItem.ToString();
                if (fileName != "")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to deploy " + fileName + " to all environments?", "Deploy File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        string successMoves = "";
                        int selectedIndex = lbMaster.SelectedIndex;
                        foreach (FileDifferenceTab fdt in FileTabs)
                        {
                            //Backup file in all prods
                            bool fileBackedup = false;
                            if (MoveFileToBackup(fileName, fdt.label, selectedIndex, ref fileBackedup, true))
                            {
                                if(fileBackedup)
                                {
                                    successMoves += "\n Backed up " + fileName + " in " + fdt.label;
                                }                                
                                //Copy file
                                if (CopyFileToProd(fileName, fdt.label, lbMaster.SelectedIndex))
                                {
                                    successMoves += "\n Copied " + fileName + " to " + fdt.label;
                                }
                                else //error has occured
                                    break;
                            }
                            else //error has occured
                                break;
                        }

                        if (successMoves != "")
                        {
                            lbMaster_SelectedIndexChanged(this, EventArgs.Empty);
                            MessageBox.Show(fileName + " deployed to environments:\n" + successMoves);                            
                        }
                    }
                }
            }
        }

        private FileDifferenceTab GetSelectedTab()
        {
            if ((tcFiles.SelectedIndex < 0) || (FileTabs.Count == 0))
                return null;
            else 
                return FileTabs[tcFiles.SelectedIndex];
        }

        private void btnSaveMaster_Click(object sender, EventArgs e)
        {
            if (Config.ValidateAllFolderSettings() && Config.ValidateAllBackupSettings())
            {
                string fileName = lbMaster.SelectedItem.ToString();
                if (fileName != "")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to save " + fileName + " in Master?", "Save File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        masterHasChanged = false;
                        SaveFile(fileName, Config.MasterLabel, rbMaster);
                        
                        EnableMasterSaveAndUndo();
                        EnableMasterHistory(fileName);
                    }
                }
            }
        }

        private void btnSaveProd_Click(object sender, EventArgs e)
        {
            if (Config.ValidateAllFolderSettings() && Config.ValidateAllBackupSettings())
            {
                string fileName = lbMaster.SelectedItem.ToString();
                if (fileName != "")
                {
                    //Get selected tab
                    FileDifferenceTab ft = GetSelectedTab();
                    if (ft == null)
                        return;

                    DialogResult result = MessageBox.Show("Are you sure you want to save " + fileName + " in " + ft.label + "? ", "Save File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        ft.hasChanged = false;
                        SaveFile(fileName, ft.label, ft.rb);
                    }
                }
            }
        }

        private void EnableMasterSaveAndUndo()
        {
            if((Config.FileType == FileType.Customisations) || (Config.FileType == FileType.Templates))
            {
                btnSaveMaster.Enabled = false;
            }
            else
            {
                btnSaveMaster.Enabled = masterHasChanged;
            }
        }

        private void EnableProdSaveAndUndo(FileDifferenceTab ft)
        {
            if (ft.fds == FileDifferenceState.Missing || (Config.FileType == FileType.Customisations) || (Config.FileType == FileType.Templates))
            {
                btnSaveProd.Enabled = false;
            }
            else
            {
                btnSaveProd.Enabled = ft.hasChanged;
            }
        }

        private void EnableProdHistory(string label, string fileName)
        {
            btnProdHistory.Enabled = false;
            if (Config.FileType != FileType.XMLUI)
            {
                FileDifferenceTab fdt = GetSelectedTab();
                if (fdt == null)
                    return;
                string folder = Config.ReadSetting(Config.FileTypePrefix() + label);
                FileInfo file = new FileInfo(folder + "\\" + fileName);

                string extension = file.Extension;
                string withoutExtension = file.Name.Substring(0, file.Name.Length - extension.Length);

                string backupFolder = Config.ReadSetting(Config.FileTypePrefix() + label + Config.BackupSuffix);

                if (file != null && file.Exists)
                {
                    DirectoryInfo di = new DirectoryInfo(backupFolder);
                    FileInfo[] files = di.GetFiles(withoutExtension + "-20" + "*" + extension);

                    btnProdHistory.Enabled = (files.Count() > 0);
                }                    
            }
        }

        private void EnableMasterHistory(string fileName)
        {
            btnMasterHistory.Enabled = false;
            if (Config.FileType != FileType.XMLUI)
            {
                string folder = Config.ReadSetting(Config.FileTypePrefix() + Config.MasterLabel);
                FileInfo file = new FileInfo(folder + "\\" + fileName);

                string extension = file.Extension;
                string withoutExtension = file.Name.Substring(0, file.Name.Length - extension.Length);

                string backupFolder = Config.ReadSetting(Config.FileTypePrefix() + Config.MasterLabel + Config.BackupSuffix);

                if (file != null && file.Exists)
                {
                    DirectoryInfo di = new DirectoryInfo(backupFolder);
                    FileInfo[] files = di.GetFiles(withoutExtension + "-20" + "*" + extension);

                    btnMasterHistory.Enabled = (files.Count() > 0);
                }
            }
        }

        private void rbMaster_TextChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                masterHasChanged = true;
                EnableMasterSaveAndUndo();

                if (selectedRB != null)
                {
                    int selectedStart = selectedRB.SelectionStart;
                    int selectedLength = selectedRB.SelectionLength;
                    RBHelper.HighlightText(selectedRB, true);
                    selectedRB.SelectionStart = selectedStart;
                    selectedRB.SelectionLength = selectedLength;
                }

                if (rbMaster.Text == "")
                {
                    AddLineNumbers(true);
                }

                //textChangedTimer.Interval = 2000; //2 seconds
                //textChangedTimer.Start(); //Trying to update diff highlighting if silent for 2 seconds
            }
        }

        private void rbProd_TextChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                FileDifferenceTab ft = GetSelectedTab();
                if (ft != null)
                {
                    ft.hasChanged = true;
                    EnableProdSaveAndUndo(ft);
                    if (ft.rb.Text == "")
                    {
                        AddLineNumbers(false);
                    }
                }                   
            }
        }

        private void tcFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileDifferenceTab ft = GetSelectedTab();
            if (ft != null)
            {
                loading = true;
                EnableProdSaveAndUndo(ft);

                string fileName = lbMaster.SelectedItem.ToString();
                EnableProdHistory(ft.label, fileName);

                btnProdNotepad.Enabled = ft.fds != FileDifferenceState.Missing;

                if (ft.fds == FileDifferenceState.Same && !ft.hasChanged)
                {
                    RBHelper.HighlightText(ft.rb);
                }
                else if (ft.fds == FileDifferenceState.Missing)
                {
                    //Need to unhighlight master
                    rbMaster.Select(0, rbMaster.Text.Length);
                    rbMaster.SelectionBackColor = Config.UNCHANGED_COLOUR;
                }
                else
                {
                    RBHelper.HighlightText(ft.rb);
                    RBHelper.DiffHighlighting(diffBuilder, rbMaster, ft.rb);
                }

                lbMaster.Select();

                //ModDate and Size Text
                int selectedIndex = lbMaster.SelectedIndex;
                if (selectedIndex != -1)
                {
                    string filename = masterFiles[selectedIndex].fileName;
                    string fullProdFileName = ft.fullProdFileName;
                    FileInfo fi = new FileInfo(fullProdFileName);
                    if (fi != null && fi.Exists)
                    {
                        lblProdMod.Text = fi.LastWriteTime.ToString();
                        lblProdSize.Text = fi.Length.ToString() + " bytes";
                    }
                    else
                    {
                        lblProdMod.Text = "";
                        lblProdSize.Text = "";
                    }
                }
                loading = false;
                ft.rb.Select(0, 0);
            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int buttonTop = lblSharepoint.Top - 13;
            int labelTop = lblSharepoint.Top - 5;
            int buttonGap = 10;

            //Master buttons
            btnSaveMaster.Left = (rbMaster.Width - btnSaveMaster.Width) / 2;
            btnSaveMaster.Top = buttonTop;

            btnMasterInfo.Left = btnSaveMaster.Width + btnSaveMaster.Left + buttonGap;
            btnMasterInfo.Top = buttonTop;

            btnMasterHistory.Left = btnMasterInfo.Width + btnMasterInfo.Left + buttonGap;
            btnMasterHistory.Top = buttonTop;

            lblMasterMod.Top = labelTop;
            lblMasterSize.Top = labelTop;

            btnMasterNotepad.Top = btnMasterHistory.Top;
            btnMasterNotepad.Left = btnMasterHistory.Left + btnMasterHistory.Width + buttonGap;

            AddLineNumbers(true);

            //Prod buttons
            btnSaveProd.Left = (tcFiles.Width - btnSaveProd.Width) / 2;
            btnSaveProd.Top = buttonTop;

            btnProdInfo.Left = btnSaveProd.Width + btnSaveProd.Left + buttonGap;
            btnProdInfo.Top = buttonTop;

            btnProdHistory.Left = btnProdInfo.Width + btnProdInfo.Left + buttonGap;
            btnProdHistory.Top = buttonTop;

            lblProdMod.Top    = labelTop;
            lblProdSize.Top   = labelTop;
            
            btnProdNotepad.Top = btnProdHistory.Top;            
            btnProdNotepad.Left = btnProdHistory.Left + btnProdHistory.Width + buttonGap;
            
            AddLineNumbers(false);
        }

        public void AddLineNumbers(bool forMaster)
        {
            RichTextBox rb;
            RichTextBox LineNumberTextBox;
            if (forMaster)
            {
                rb = rbMaster;
                LineNumberTextBox = MasterLineNumberTextBox;
            }
            else
            {
                FileDifferenceTab ft = GetSelectedTab();
                if (ft == null)
                    return;
                rb = ft.rb;
                LineNumberTextBox = ProdLineNumberTextBox;
            }
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Right;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";

            if (forMaster)
                LineNumberTextBox.Top = rb.Top;
            else
                LineNumberTextBox.Top = tcFiles.Top + rb.Top + 25;
            LineNumberTextBox.Width = RBHelper.getWidth(LineNumberTextBox);
            LineNumberTextBox.Height = rb.Height;
            LineNumberTextBox.Visible = Config.FileType != FileType.Templates;

            if(LineNumberTextBox.Visible)
            {
                //Move richtextbox to the right of LineNumberTextBox if visible
                if (forMaster)
                    rb.Left = LineNumberTextBox.Left + LineNumberTextBox.Width;
                else
                    rb.Left = LineNumberTextBox.Width - 4;
                rb.Width = rb.Parent.Width - LineNumberTextBox.Width;
            }
            else
            {
                if (forMaster)
                    rb.Left = LineNumberTextBox.Left;
                else
                    rb.Left = -4;
                rb.Width = rb.Parent.Width;
            }

            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from rb    
            int First_Index = rb.GetCharIndexFromPosition(pt);
            int First_Line = rb.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to rb Width & Height respectively    
            pt.X = rb.Width;
            pt.Y = rb.Height;
            // get Last Index & Last Line from rb    
            int Last_Index = rb.GetCharIndexFromPosition(pt);
            int Last_Line = rb.GetLineFromCharIndex(Last_Index);

            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        private void rbMaster_SelectionChanged(object sender, EventArgs e)
        {
            if(!loading)
            {
                Point pt = rbMaster.GetPositionFromCharIndex(rbMaster.SelectionStart);
                if (pt.X == 1)
                {
                    AddLineNumbers(true);
                }
            }
        }

        private void rbMaster_FontChanged(object sender, EventArgs e)
        {
            MasterLineNumberTextBox.Font = rbMaster.Font;
            rbMaster.Select();
            AddLineNumbers(true);
        }

        private void MasterLineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            rbMaster.Select();
            MasterLineNumberTextBox.DeselectAll();
        }

        private void rbProd_SelectionChanged(object sender, EventArgs e)
        {
            if(!loading)
            {
                FileDifferenceTab ft = GetSelectedTab();
                if (ft != null)
                {
                    Point pt = ft.rb.GetPositionFromCharIndex(ft.rb.SelectionStart);
                    if (pt.X == 1)
                    {
                        AddLineNumbers(false);
                    }
                }
            }          
        }

        private void rbProd_FontChanged(object sender, EventArgs e)
        {
            FileDifferenceTab ft = GetSelectedTab();
            if (ft != null)
            {
                ProdLineNumberTextBox.Font = ft.rb.Font;
                ft.rb.Select();
                AddLineNumbers(false);
            }
        }

        private void ProdLineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            FileDifferenceTab ft = GetSelectedTab();
            if(ft != null)
            {
                ft.rb.Select();
                ProdLineNumberTextBox.DeselectAll();
            }
        }

        private void lbMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lbMaster.Items.Count != 0)
            {
                for(int i = 0; i < lbMaster.Items.Count; i++)
                {
                    if(lbMaster.Items[i].ToString().StartsWith(e.KeyChar.ToString()))
                    {
                        lbMaster.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnMasterNotepad_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbMaster.SelectedIndex;
            if (selectedIndex != -1)
            {
                string fullMasterFileName = masterFiles[selectedIndex].fullFileName;
                if (Config.FileType != FileType.Templates)
                    OpenInNotePad(fullMasterFileName, rbMaster);
                else
                    OpenInWord(fullMasterFileName);
            }
        }

        private void btnProdNotepad_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbMaster.SelectedIndex;
            string filename = lbMaster.SelectedItem.ToString();
            FileDifferenceTab fdt = GetSelectedTab();
            if ((selectedIndex != -1) && (fdt != null))
            {
                string fullProdFileName = fdt.fullProdFileName;
                if (Config.FileType != FileType.Templates)
                    OpenInNotePad(fullProdFileName, fdt.rb);
                else
                    OpenInWord(fullProdFileName);
            }
        }

        private void OpenInNotePad(string fullFileName, RichTextBox rb)
        {
            //var nppDir = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Notepad++", null, null);
            //var nppExePath = Path.Combine(nppDir, "Notepad++.exe");
            int line = rb.SelectionStart;
            var nppExePath = "C:\\Program Files\\Notepad++\\notepad++.exe";
            var sb = new StringBuilder();
            sb.AppendFormat("\"{0}\" -n{1}", fullFileName, line);
            Process.Start(nppExePath, sb.ToString());
        }

        private void OpenInWord(string fullFileName)
        {
            //Word.Application ap = new Word.Application();
            //Document document = ap.Documents.Open(fullFileName);
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            if (Config.FileType != FileType.XMLUI)
            {
                MessageBox.Show("Total Files: " + masterFiles.Count().ToString() + "\n\nSame: " + stats.sameFiles.ToString() + "\nDifferent: " + stats.differentFiles.ToString() + "\nMissing: " + stats.missingFiles.ToString() + "\n\nTime: " + stats.elapsedTime.ToString() + "\n\nPress refresh for new statistics.",
                        "Statistics", //title
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
            }
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            string searchText = tbFind.Text;
            FileDifferenceTab ft = GetSelectedTab();
            int endOfWordSelected = selectedRB.SelectionStart + selectedRB.SelectionLength;
            string theRest = selectedRB.Text.Substring(endOfWordSelected);
            int startOfNextWordInTheRest = theRest.IndexOf(searchText);
            int startOfNextWord;

            //A faster way to do this is to remember the back and fore color of the selectedText
            //But it has to be maintained in selectionChanged event. Hard to do, instead will
            //just rediff and highlight the text

            if (startOfNextWordInTheRest == -1)
            {
                //We haven't found it. So try to wrap around to the start of the file
                startOfNextWord = selectedRB.Text.IndexOf(searchText);
                if (startOfNextWord != -1)
                {
                    RBHelper.DiffHighlighting(diffBuilder, rbMaster, ft.rb);
                    RBHelper.HighlightText(selectedRB, true);

                    selectedRB.Select(startOfNextWord, searchText.Length);
                    selectedRB.SelectionBackColor = Color.Blue;
                    selectedRB.SelectionColor = Color.White;                    
                }
            }
            else
            {
                //We found it without having to wrap around the text
                startOfNextWord = endOfWordSelected + startOfNextWordInTheRest;

                RBHelper.DiffHighlighting(diffBuilder, rbMaster, ft.rb);
                RBHelper.HighlightText(selectedRB, true);

                selectedRB.Select(startOfNextWord, searchText.Length);
                selectedRB.SelectionBackColor = Color.Blue;
                selectedRB.SelectionColor = Color.White;                
            }            
        }

        private void btnMasterUndo_Click(object sender, EventArgs e)
        {
            //Send Ctrl-Z to rbMaster
            rbMaster.Focus();
            SendKeys.Send("^(z)");
        }

        private void btnProdUndo_Click(object sender, EventArgs e)
        {
            FileDifferenceTab ft = GetSelectedTab();
            if(ft != null)
            {
                ft.rb.Focus();
                SendKeys.Send("^(z)");
            }
        }

        private void rbProd_KeyDown(object sender, KeyEventArgs e)
        {
            selectedRB = sender as RichTextBox;
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (!gbSearch.Visible)
                {
                    tbFind.Text = selectedRB.SelectedText;
                    gbSearch.Left = lblProdMod.Left;
                    gbSearch.Top = tcFiles.Top + selectedRB.Top + selectedRB.Height - gbSearch.Height - 1;
                    gbSearch.Visible = true;
                    tbFind.Focus();
                }
                else
                {
                    gbSearch.Visible = false;
                    selectedRB.Focus();
                }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                btnSaveProd.PerformClick();
            }
        }

        private void rbMaster_KeyDown(object sender, KeyEventArgs e)
        {
            selectedRB = sender as RichTextBox;
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (!gbSearch.Visible)
                {
                    tbFind.Text = selectedRB.SelectedText;
                    gbSearch.Left = selectedRB.Left;
                    gbSearch.Top = selectedRB.Top + selectedRB.Height - gbSearch.Height - 1;
                    gbSearch.Visible = true;
                    //tbFind.Focus();
                }
                else
                {
                    gbSearch.Visible = false;
                    selectedRB.Focus();
                }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                btnSaveMaster.PerformClick();
            }
        }

        private void textChangedTimer_Tick(object sender, EventArgs e)
        {
            //This is extra slow if the selectedRB has focus
            if(selectedRB != null)
            {
                bool rbHadFocus = false;
                int selectionStart = 0;
                int selectionLength = 0;
                if (selectedRB.Focused)
                {
                    selectionStart = selectedRB.SelectionStart;
                    selectionLength = selectedRB.SelectionLength;
                    lbMaster.Focus();
                    rbHadFocus = true;
                }

                RBHelper.HighlightText(selectedRB);

                if(rbHadFocus)
                {
                    selectedRB.Focus();
                    selectedRB.Select(selectionStart, selectionLength);
                }
            }               
            textChangedTimer.Stop();
        }

        private void lblSharepoint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void lblSSKB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void lblIress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void tbFont_Scroll(object sender, EventArgs e)
        {
            float fontSize = tbFont.Value;
            rbMaster.Font = new Font(new FontFamily("Courier New"), fontSize);
            foreach(FileDifferenceTab ft in FileTabs)
            {
                ft.rb.Font = new Font(new FontFamily("Courier New"), fontSize);
            }
        }

        private void rbModified_CheckedChanged(object sender, EventArgs e)
        {
            if(masterFiles.Count != 0)
            {
                string selectedFileName = masterFiles[lbMaster.SelectedIndex].fileName;

                OrderBy();
                lbMaster.Invalidate();

                int index = lbMaster.FindString(selectedFileName);
                // Determine if a valid index is returned. Select the item if it is valid.
                if (index != -1)
                    lbMaster.SetSelected(index, true);
            }
        }

        private void rbName_CheckedChanged(object sender, EventArgs e)
        {
            if (masterFiles.Count != 0)
            {
                string selectedFileName = masterFiles[lbMaster.SelectedIndex].fileName;

                OrderBy();
                lbMaster.Invalidate();

                int index = lbMaster.FindString(selectedFileName);
                // Determine if a valid index is returned. Select the item if it is valid.
                if (index != -1)
                    lbMaster.SetSelected(index, true);
            }
        }

        private void OrderBy()
        {
            lbMaster.BeginUpdate();
            lbMaster.Items.Clear();

            if(rbName.Checked)
                masterFiles = masterFiles.OrderBy(x => x.fileName).ToList();
            else
                masterFiles = masterFiles.OrderByDescending(x => x.modifiedDate).ToList();

            foreach(MasterFile mf in masterFiles)
            {
                lbMaster.Items.Add(mf.fileName);
            }
            lbMaster.EndUpdate();
        }

        //Make right click select the item
        private void lbMaster_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListBox lb = sender as ListBox;
                int index = lb.IndexFromPoint(e.X, e.Y);
                if (index != ListBox.NoMatches)
                {
                    lb.SelectedIndex = index;
                }                    
            }
        }

        private void cmsFile_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Name == miDeploy.Name)
            {
                Deploy();
            }
            else if (e.ClickedItem.Name == miBackup.Name)
            {
                BackupFileEverywhere();
            }
        }

        private void btnProdHistory_Click(object sender, EventArgs e)
        {
            if (Config.FileType != FileType.XMLUI)
            {
                int selectedIndex = lbMaster.SelectedIndex;
                FileDifferenceTab fdt = GetSelectedTab();
                if ((selectedIndex != -1) && (fdt != null))
                {
                    History lcForm = new History();
                    lcForm.fileName = lbMaster.SelectedItem.ToString();
                    lcForm.label = fdt.label;
                    lcForm.ShowDialog(this);
                }
            }
        }

        private void btnMasterHistory_Click(object sender, EventArgs e)
        {
            if (Config.FileType != FileType.XMLUI)
            {
                int selectedIndex = lbMaster.SelectedIndex;
                if (selectedIndex != -1)
                {
                    History lcForm = new History();
                    lcForm.fileName = lbMaster.SelectedItem.ToString();
                    lcForm.label = Config.MasterLabel;
                    lcForm.ShowDialog(this);
                }
            }
        }

        private void Main_BackColorChanged(object sender, EventArgs e)
        {
            scFiles.Panel1.BackColor = this.BackColor;
            scFiles.Panel2.BackColor = this.BackColor;
        }

        private void btnMasterInfo_Click(object sender, EventArgs e)
        {
            if(Config.FileType == FileType.Reports)
            {
                int selectedIndex = lbMaster.SelectedIndex;
                if(selectedIndex != -1)
                {
                    MasterFile mf = masterFiles[selectedIndex];

                    FInfo fi = new FInfo();
                    fi.label = Config.MasterLabel;
                    fi.fileName = mf.fileName;
                    fi.fullFileName = mf.fullFileName;
                    fi.ShowDialog();
                }
            }            
        }

        private void btnProdInfo_Click(object sender, EventArgs e)
        {
            if (Config.FileType == FileType.Reports)
            {
                int selectedIndex = lbMaster.SelectedIndex;
                if (selectedIndex != -1)
                {
                    FileDifferenceTab ft = GetSelectedTab();
                    if(ft != null)
                    {
                        MasterFile mf = masterFiles[selectedIndex];

                        FInfo fi = new FInfo();
                        fi.label = ft.label;
                        fi.fileName = mf.fileName;
                        fi.fullFileName = ft.fullProdFileName;
                        fi.ShowDialog();
                    }
                }
            }
        }

    }

    public class MasterFile
    {
        public FileDifferenceState fds = FileDifferenceState.Unknown;
        public string fullFileName = "";
        public string fileName = "";
        public DateTime modifiedDate = DateTime.MinValue;

        public MasterFile(string fullFileName, string fileName, DateTime modifiedDate)
        {
            this.fullFileName = fullFileName;
            this.fileName = fileName;
            this.modifiedDate = modifiedDate;
        }
    }

}
