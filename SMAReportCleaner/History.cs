using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SMAReportCleaner
{
    public partial class History : Form
    {
        public string fileName;
        public string label;
        private SideBySideDiffBuilder diffBuilder = new SideBySideDiffBuilder(new Differ());
        private bool loading = false;
        private string backupFolder;
        private string searchString;
        private string currentHistoryFileName;

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public History()
        {
            InitializeComponent();
        }

        private void LastChange_Load(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;

            backupFolder = Config.ReadSetting(Config.FileTypePrefix() + label + Config.BackupSuffix);
            loading = true;

            string folder = Config.ReadSetting(Config.FileTypePrefix() + label);
            FileInfo newFile = new FileInfo(folder + "\\" + fileName);
                                   
            if (newFile != null && newFile.Exists)
            {
                lblFullNewFileName.Text = newFile.FullName;
                lblNewMod.Text = newFile.LastWriteTime.ToString();
                lblNewSize.Text = newFile.Length.ToString() + " bytes";
                rbNew.ForeColor = Color.Black;
                if ((newFile.Extension.ToUpper() == ".RTF") || (newFile.Extension.ToUpper() == ".DOC") || (newFile.Extension.ToUpper() == ".DOCX"))
                {
                    rbNew.LoadFile(newFile.FullName, RichTextBoxStreamType.RichText);
                }
                else
                {
                    rbNew.LoadFile(newFile.FullName, RichTextBoxStreamType.PlainText);
                }

                RBHelper.HighlightText(rbNew);

                string extension = newFile.Extension;
                string withoutExtension = newFile.Name.Substring(0, newFile.Name.Length - extension.Length);
                searchString = withoutExtension + "-20" + "*" + extension;

                DirectoryInfo di = new DirectoryInfo(backupFolder);               
                FileInfo[] files = di.GetFiles(searchString);
                //These will be ordered by name by default, so we want the last one.
                FileInfo oldFile = files.LastOrDefault();
                btnPrevious.Enabled = files.Count() > 1;
                LoadHistoryFile(oldFile);                
            }
            loading = false;
            AddLineNumbers(true);
            rbNew.Select(0, 0);
        }

        private void LoadHistoryFile(FileInfo oldFile)
        {
            if (oldFile != null && oldFile.Exists)
            {                
                lblFullOldFileName.Text = oldFile.FullName;
                lblOldMod.Text = oldFile.LastWriteTime.ToString();
                lblOldSize.Text = oldFile.Length.ToString() + " bytes";
                rbOld.ForeColor = Color.Black;

                if ((oldFile.Extension.ToUpper() == ".RTF") || (oldFile.Extension.ToUpper() == ".DOC") || (oldFile.Extension.ToUpper() == ".DOCX"))
                {
                    rbOld.LoadFile(oldFile.FullName, RichTextBoxStreamType.RichText);
                }
                else
                {
                    rbOld.LoadFile(oldFile.FullName, RichTextBoxStreamType.PlainText);
                }

                currentHistoryFileName = oldFile.Name;

                RBHelper.DiffHighlighting(diffBuilder, rbNew, rbOld);
                RBHelper.HighlightText(rbOld);

                rbOld.Select();
                AddLineNumbers(false);
                rbOld.Select(0, 0);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(backupFolder);
            FileInfo[] files = di.GetFiles(searchString);
            //These will be ordered by name.
            for(int i = files.Count() - 1; i >= 0; i--)
            {
                FileInfo file = files[i];
                if(String.Compare(file.Name, currentHistoryFileName, true) < 0)
                {
                    loading = true;
                    LoadHistoryFile(file);
                    loading = false;

                    if (i == 0)
                        btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    break;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(backupFolder);
            FileInfo[] files = di.GetFiles(searchString);
            //These will be ordered by name.
            for (int i = 0; i < files.Count(); i++)
            {
                FileInfo file = files[i];
                if (String.Compare(file.Name, currentHistoryFileName, true) > 0)
                {
                    loading = true;
                    LoadHistoryFile(file);
                    loading = false;

                    if (i == files.Count() - 1)
                        btnNext.Enabled = false;
                    btnPrevious.Enabled = true;
                    break;
                }
            }
        }

        private int getWidth(RichTextBox LineNumberTextBox)
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = LineNumberTextBox.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)LineNumberTextBox.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)LineNumberTextBox.Font.Size;
            }
            else
            {
                w = 50 + (int)LineNumberTextBox.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers(bool forNew)
        {
            RichTextBox rb;
            RichTextBox LineNumberTextBox;
            if (forNew)
            {
                rb = rbNew;
                LineNumberTextBox = NewLineNumberTextBox;
            }
            else
            {
                rb = rbOld;
                LineNumberTextBox = OldLineNumberTextBox;
            }
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Right;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";

            LineNumberTextBox.Top = rb.Top;
            LineNumberTextBox.Width = getWidth(LineNumberTextBox);
            LineNumberTextBox.Height = rb.Height;
            LineNumberTextBox.Visible = Config.FileType != FileType.Templates;

            if (LineNumberTextBox.Visible)
            {
                //Move richtextbox to the right of LineNumberTextBox
                rb.Left = LineNumberTextBox.Left + LineNumberTextBox.Width;
                rb.Width = rb.Parent.Width - LineNumberTextBox.Width;
            }
            else
            {
                rb.Left = LineNumberTextBox.Left;
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

        private void rbOld_MouseDown(object sender, MouseEventArgs e)
        {
            rbOld.Select();
            OldLineNumberTextBox.DeselectAll();

            rbOld.AutoWordSelection = true;
            rbOld.AutoWordSelection = false;
        }

        private void rbNew_MouseDown(object sender, MouseEventArgs e)
        {
            rbNew.Select();
            NewLineNumberTextBox.DeselectAll();

            rbNew.AutoWordSelection = true;
            rbNew.AutoWordSelection = false;
        }

        private void rbNew_SelectionChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                Point pt = rbNew.GetPositionFromCharIndex(rbNew.SelectionStart);
                if (pt.X == 1)
                {
                    AddLineNumbers(true);
                }
            }
        }

        private void rbOld_SelectionChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                Point pt = rbOld.GetPositionFromCharIndex(rbOld.SelectionStart);
                if (pt.X == 1)
                {
                    AddLineNumbers(false);
                }
            }
        }

        private void rbOld_FontChanged(object sender, EventArgs e)
        {
            OldLineNumberTextBox.Font = rbOld.Font;
            rbOld.Select();
            AddLineNumbers(false);
        }

        private void rbNew_FontChanged(object sender, EventArgs e)
        {
            NewLineNumberTextBox.Font = rbNew.Font;
            rbNew.Select();
            AddLineNumbers(true);
        }

        private void LastChange_SizeChanged(object sender, EventArgs e)
        {
            AddLineNumbers(true);
            AddLineNumbers(false);

            lblFullNewFileName.Left = scFiles.Left + scFiles.Panel1.Width + 5;

            lblNewMod.Left = lblFullNewFileName.Left;
            lblNewSize.Left = lblNewMod.Left + 140;

            lblNewMod.Top = lblOldMod.Top;
            lblNewSize.Top = lblOldMod.Top;
        }

        private void rbOld_VScroll(object sender, EventArgs e)
        {
            if (!loading)
            {
                OldLineNumberTextBox.Text = "";
                AddLineNumbers(false);
                OldLineNumberTextBox.Invalidate();

                int nPos = GetScrollPos(rbOld.Handle, (int)ScrollBarType.SbVert);
                nPos <<= 16;
                uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;

                SendMessage(rbNew.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
            }
        }

        private void rbNew_VScroll(object sender, EventArgs e)
        {
            if (!loading)
            {
                NewLineNumberTextBox.Text = "";
                AddLineNumbers(true);
                NewLineNumberTextBox.Invalidate();
            }
        }

        private void splitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
        {
            lblFullNewFileName.Left = scFiles.Left + scFiles.Panel1.Width + 5;

            lblNewMod.Left = lblFullNewFileName.Left;
            lblNewSize.Left = lblNewMod.Left + 140;

            lblNewMod.Top = lblOldMod.Top;
            lblNewSize.Top = lblOldMod.Top;
        }
        
    }
}
