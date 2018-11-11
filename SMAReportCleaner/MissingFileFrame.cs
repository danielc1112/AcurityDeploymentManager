using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMAReportCleaner
{
    public class MissingFileFrame
    {
        public GroupBox gb;
        public Label lbl;
        public ListBox lb;
        public Button btnMasterToProd;
        public Button btnProdToMaster;
        public Button btnBackup;
        public ListBox lbMaster;

        public const int BUTTON_HEIGHT = 30;
        public const int BUTTON_SPACING = 2;

        public string masterFolder;
        public string masterBackupFolder;
        public string prodFolder;
        public string backupFolder;

        public List<bool> MissingFiles = new List<bool>();
        private Action LoadScreen;

        public MissingFileFrame(Control parent, Action loadScreen, ListBox lbMaster, GroupBox gbMaster, string label)
        {
            masterFolder = Config.ReadSetting(Config.FileTypePrefix() + Config.MasterLabel);
            masterBackupFolder = Config.ReadSetting(Config.FileTypePrefix() + Config.MasterLabel + Config.BackupSuffix);
            prodFolder = Config.ReadSetting(Config.FileTypePrefix() + label);
            backupFolder = Config.ReadSetting(Config.FileTypePrefix() + label + Config.BackupSuffix);

            this.lbMaster = lbMaster;

            this.LoadScreen = loadScreen;

            gb = new GroupBox();
            gb.Text = label;
            gb.Parent = parent;
            gb.Width = gbMaster.Width;
            gb.Left = 0;
            gb.Top = 0;
            gb.Height = gbMaster.Height - 14;
            gb.Font = new Font(gb.Font.FontFamily, 10.0f);
            //gb.AutoSize = true;
            //gb.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;

            lb = new ListBox();
            lb.Sorted = true;
            lb.Parent = gb;
            lb.Left = 5;
            lb.Width = lbMaster.Width;
            lb.Top = lbMaster.Top;
            lb.Height = lbMaster.Height - 3 * BUTTON_HEIGHT + 4 * BUTTON_SPACING;
            lb.Font = new Font(lb.Font.FontFamily, 10.0f);

            lb.MeasureItem += lb_MeasureItem;
            lb.DrawItem += lb_DrawItem;
            lb.SelectedIndexChanged += Lb_SelectedIndexChanged;

            btnMasterToProd = new Button();
            btnMasterToProd.Text = "Master To Prod";
            btnMasterToProd.Parent = gb;
            btnMasterToProd.Left = lb.Left;
            btnMasterToProd.Width = lbMaster.Width;
            btnMasterToProd.Height = BUTTON_HEIGHT;
            btnMasterToProd.Top = lb.Top + lb.Height + BUTTON_SPACING;
            //btnMasterToProd.Font = new Font(lb.Font.FontFamily, 10.0f);

            btnMasterToProd.Click += btnMasterToProd_Click;

            btnProdToMaster = new Button();
            btnProdToMaster.Text = "Prod To Master";
            btnProdToMaster.Parent = gb;
            btnProdToMaster.Left = lb.Left;
            btnProdToMaster.Width = lbMaster.Width;
            btnProdToMaster.Height = BUTTON_HEIGHT;
            btnProdToMaster.Top = lb.Top + lb.Height + BUTTON_HEIGHT + (2 * BUTTON_SPACING);
            //btnProdToMaster.Font = new Font(lb.Font.FontFamily, 10.0f);

            btnProdToMaster.Click += btnProdToMaster_Click;

            btnBackup = new Button();
            btnBackup.Text = "Backup";
            btnBackup.Parent = gb;
            btnBackup.Left = lb.Left;
            btnBackup.Width = lbMaster.Width;
            btnBackup.Height = BUTTON_HEIGHT;
            btnBackup.Top = lb.Top + lb.Height + (2 * BUTTON_HEIGHT) + (3 * BUTTON_SPACING);
            //btnBackup.Font = new Font(lb.Font.FontFamily, 10.0f);

            btnBackup.Click += btnBackup_Click;
        }

        public void LoadUp()
        {
            lb.DrawMode = DrawMode.Normal;
            lb.BeginUpdate();
            lb.Text = "";
            MissingFiles.Clear();

            LoadFiles();
            RemoveFilesInMaster();
            ColorRemainingFiles();
            lb.EndUpdate();
            //Only start drawing after Missingfiles is populated
            lb.DrawMode = DrawMode.OwnerDrawFixed;

            if (lb.Items.Count > 0)
                lb.SelectedIndex = 0;
            EnableCopyButtons();
        }

        public void LoadFiles()
        {
            lb.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(prodFolder);
            FileInfo[] files = di.GetFiles();
            lb.BeginUpdate();
            foreach (FileInfo f in files)
            {
                lb.Items.Add(f.Name.ToUpper());
            }
            lb.EndUpdate();
        }

        public void RemoveFilesInMaster()
        {
            foreach (string filename in lbMaster.Items)
            {
                if (lb.Items.Contains(filename))
                {
                    //The file is in both master and environment, so remove it
                    lb.Items.Remove(filename);
                }
                else
                {
                    //Add it to the listbox and color grey.
                    //This means the file is in master but missing from this environment
                    lb.Items.Add(filename);
                }
            }
        }

        public void ColorRemainingFiles()
        {
            foreach (string filename in lb.Items)
            {
                //If contains=true, this file is in the master, but we've already removed matched files. 
                //Therefore, we must have added here before (in RemoveFilesInMaster()) to say that it was missing.
                //So mark as true to color it grey
                MissingFiles.Add(lbMaster.Items.Contains(filename));
            }
        }

        public void EnableCopyButtons()
        {
            bool Missingfile = false;
            if (lb.SelectedItem != null)
            {
                string filename = lb.SelectedItem.ToString();
                if (filename != "")
                {
                    Missingfile = lbMaster.Items.Contains(filename);
                    btnMasterToProd.Enabled = Missingfile;
                    btnProdToMaster.Enabled = !Missingfile;
                    btnBackup.Enabled       = !Missingfile;
                }
                else
                {
                    btnMasterToProd.Enabled = false;
                    btnProdToMaster.Enabled = false;
                    btnBackup.Enabled       = false;
                }
            }
            else
            {
                btnMasterToProd.Enabled = false;
                btnProdToMaster.Enabled = false;
                btnBackup.Enabled       = false;
            }
        }

        private void btnMasterToProd_Click(object sender, EventArgs e)
        {
            btnCopy_Click(sender, e, true);
        }

        private void btnProdToMaster_Click(object sender, EventArgs e)
        {
            btnCopy_Click(sender, e, false);
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (Config.ValidateAllBackupSettings())
            {
                if (lb.SelectedItem == null)
                    return;
                string fileName = lb.SelectedItem.ToString();
                if (fileName != "")
                {
                    string fromFolder = "";
                    string toFolder = "";

                    fromFolder = prodFolder;
                    toFolder = backupFolder;

                    DialogResult result = MessageBox.Show("Are you sure you want to backup " + fileName + " from " + fromFolder + " to " + toFolder + "? ", "Backup File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        string from = fromFolder + "\\" + fileName;
                        string to = toFolder + "\\" + Config.ToBackupFileName(fileName);

                        if (File.Exists(from))
                        {
                            try
                            {
                                File.Move(from, to); // Try to move
                                //MessageBox.Show(fileName + " successfully backed up from " + fromFolder + " to " + toFolder);

                                LoadUp();
                            }
                            catch (IOException ex)
                            {
                                MessageBox.Show("Error backing up " + fileName + " from " + fromFolder + " to " + toFolder + ": " + ex.Message,
                                "Error Backing Up File", //title
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                            }
                        }
                        else
                        {
                            MessageBox.Show(from + " does not exist",
                            "Error Backing Up File", //title
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e, bool MasterToProd)
        {
            if (Config.ValidateAllFolderSettings() && Config.ValidateAllBackupSettings())
            {
                if (lb.SelectedItem == null)
                    return;
                string fileName = lb.SelectedItem.ToString();
                if (fileName != "")
                {
                    string fromFolder = "";
                    string toFolder = "";

                    if (MasterToProd)
                    {
                        fromFolder = masterFolder;
                        toFolder = prodFolder;
                    }
                    else
                    {
                        fromFolder = prodFolder;
                        toFolder = masterFolder;
                    }

                    DialogResult result = MessageBox.Show("Are you sure you want to copy " + fileName + " from " + fromFolder + " to " + toFolder + "? ", "Copy File",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        string from = fromFolder + "\\" + fileName;
                        string to = toFolder + "\\" + fileName;

                        if (File.Exists(from))
                        {
                            try
                            {
                                //Backup master/prod file if it exists first
                                if (File.Exists(to))
                                {
                                    string fromBackup = to;
                                    string toBackup = "";

                                    if (MasterToProd)
                                        toBackup = backupFolder + "\\" + Config.ToBackupFileName(fileName);
                                    else
                                        toBackup = masterBackupFolder + "\\" + Config.ToBackupFileName(fileName);

                                    try
                                    {
                                        File.Move(fromBackup, toBackup); // Try to backup
                                    }
                                    catch (IOException ex)
                                    {
                                        MessageBox.Show("Error backing up " + fileName + " from " + fromBackup + " to " + toBackup + ": " + ex.Message,
                                        "Error Backing Up File", //title
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                        );
                                        return;
                                    }
                                }

                                File.Copy(from, to); // Try to copy
                                //MessageBox.Show(fileName + " successfully copied from " + fromFolder + " to " + toFolder);
                                if (MasterToProd)
                                    LoadUp();
                                else
                                    LoadScreen(); //If master changes, must reload all others
                            }
                            catch (IOException ex)
                            {
                                MessageBox.Show("Error copying " + fileName + " from " + fromFolder + " to " + toFolder + ": " + ex.Message,
                                "Error Copying File", //title
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                            }
                        }
                        else
                        {
                            MessageBox.Show(from + " does not exist",
                            "Error Copying File", //title
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
        }

        private void Lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableCopyButtons();
        }

        private void lb_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            e.ItemHeight = listBox.Font.Height;
        }

        private void lb_DrawItem(object sender, DrawItemEventArgs e)
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

            if (MissingFiles.Count > e.Index)
            {
                if (MissingFiles[e.Index])
                {
                    //true - contained in master but not prod
                    myBrush = Brushes.LightGray; //pen color
                }
                else
                {
                    //false - Contained in prod but not master
                    myBrush = Brushes.LightGreen; //pen color
                }
            }

            e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), myFont, myBrush, e.Bounds);
            e.DrawFocusRectangle();

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                Pen pen = new Pen(Color.Black);
                int y = e.Index * listBox.ItemHeight;
                e.Graphics.DrawRectangle(pen, 0, y, listBox.Width - 5, listBox.ItemHeight - 1);
            }
        }
    }
}
