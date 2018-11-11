using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMAReportCleaner
{
    public class SettingFrame
    {
        public GroupBox gb;

        public string label;
        public Label lblSetting;
        public TextBox tbSetting;

        public Label lblFolder;
        public TextBox tbFolder;        
        public Button btnFolder;

        public Label lblBackupFolder;
        public TextBox tbBackupFolder;
        public Button btnBackupFolder;

        public FolderBrowserDialog fbd;
        private static string lastChosenFolder = "";

        private Action<SettingFrame> SetSelectedFrame;

        public SettingFrame(FileType ft, Control parent, string label, int left, bool brandNew, Action<SettingFrame> setSelectedFrame)
        {
            this.label = label;
            this.SetSelectedFrame = setSelectedFrame;
            fbd = new FolderBrowserDialog();

            gb = new GroupBox();
            gb.Text = label;
            gb.Parent = parent;
            gb.Width = 1040;
            gb.Left = left;
            gb.Top = 1;
            gb.Height = 125;
            //gb.AutoSize = true;
            //gb.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);

            const int TEXTBOX_LEFT = 150;
            const int TEXTBOX_WIDTH = 850;
            const int LABEL_TOP = 30;
            const int FOLDER_TOP = 60;
            const int BACKUP_TOP = 90;
            const int BUTTON_WIDTH = 26;

            //-----------LABEL------------
            lblSetting = new Label();
            lblSetting.Parent = gb;
            lblSetting.Text = "Label:";
            lblSetting.Left = 2;
            lblSetting.Top = LABEL_TOP;
            lblSetting.Width = TEXTBOX_LEFT - lblSetting.Left;

            tbSetting = new TextBox();
            tbSetting.Parent = gb;
            tbSetting.Text = label;
            tbSetting.Parent = gb;
            tbSetting.Left = TEXTBOX_LEFT;
            tbSetting.Width = 150;
            tbSetting.Top = LABEL_TOP;

            tbSetting.Enabled = brandNew;

            tbSetting.Enter += tb_Enter;
            tbSetting.TextChanged += tbSetting_TextChanged;

            //----------FOLDER-------------
            lblFolder = new Label();
            lblFolder.Parent = gb;
            lblFolder.Text = label + " Folder:";
            lblFolder.Left = 2;
            lblFolder.Top = FOLDER_TOP;
            lblFolder.Width = TEXTBOX_LEFT - lblFolder.Left;

            tbFolder = new TextBox();
            tbFolder.Parent = gb;
            tbFolder.Left = TEXTBOX_LEFT;
            tbFolder.Width = TEXTBOX_WIDTH;
            tbFolder.Top = FOLDER_TOP;

            if(label != "")
                tbFolder.Text = Config.ReadSetting(Config.FileTypePrefix(ft) + label);

            tbFolder.Enter += tb_Enter;

            btnFolder = new Button();
            btnFolder.Parent = gb;
            btnFolder.Width = BUTTON_WIDTH;
            btnFolder.Left = tbFolder.Left + tbFolder.Width - 1;
            btnFolder.Top = FOLDER_TOP - 1;
            btnFolder.Height = tbFolder.Height + 2;
            btnFolder.Text = "..";

            btnFolder.Click += btnFolder_Click;

            //---------BACK UP---------------
            lblBackupFolder = new Label();
            lblBackupFolder.Parent = gb;
            lblBackupFolder.Text = label + " Backup:";
            lblBackupFolder.Left = 2;
            lblBackupFolder.Top = BACKUP_TOP;
            lblBackupFolder.Width = TEXTBOX_LEFT - lblBackupFolder.Left;

            tbBackupFolder = new TextBox();
            tbBackupFolder.Parent = gb;
            tbBackupFolder.Left = TEXTBOX_LEFT;
            tbBackupFolder.Width = TEXTBOX_WIDTH;
            tbBackupFolder.Top = BACKUP_TOP;

            if (label != "")
                tbBackupFolder.Text = Config.ReadSetting(Config.FileTypePrefix(ft) + label + Config.BackupSuffix);

            tbBackupFolder.Enter += tb_Enter;

            btnBackupFolder = new Button();
            btnBackupFolder.Parent = gb;
            btnBackupFolder.Width = BUTTON_WIDTH;
            btnBackupFolder.Left = tbBackupFolder.Left + tbBackupFolder.Width - 1;
            btnBackupFolder.Top = BACKUP_TOP - 1;
            btnBackupFolder.Height = tbFolder.Height + 2;
            btnBackupFolder.Text = "..";

            btnBackupFolder.Click += btnBackupFolder_Click;

            //----------------------------------------------------

            if(brandNew)
                tbSetting.Focus();
        }

        private void tb_Enter(object sender, EventArgs e)
        {
            SetSelectedFrame(this);
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if ((tbFolder.Text.Trim() != "") && Directory.Exists(tbFolder.Text.Trim()))
                fbd.SelectedPath = tbFolder.Text.Trim();
            else
                fbd.SelectedPath = lastChosenFolder;

            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                tbFolder.Text = fbd.SelectedPath;
                lastChosenFolder = fbd.SelectedPath;
            }
            SetSelectedFrame(this);
        }

        private void btnBackupFolder_Click(object sender, EventArgs e)
        {
            if ((tbBackupFolder.Text.Trim() != "") && Directory.Exists(tbBackupFolder.Text.Trim()))
                fbd.SelectedPath = tbBackupFolder.Text.Trim();
            else
                fbd.SelectedPath = lastChosenFolder;

            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                tbBackupFolder.Text = fbd.SelectedPath;
                lastChosenFolder = fbd.SelectedPath;
            }
            SetSelectedFrame(this);
        }

        private void tbSetting_TextChanged(object sender, EventArgs e)
        {
            label = tbSetting.Text;
            gb.Text = tbSetting.Text;
            lblFolder.Text = tbSetting.Text + " Folder:";
            lblBackupFolder.Text = tbSetting.Text + " Backup:";
        }

    }
}
