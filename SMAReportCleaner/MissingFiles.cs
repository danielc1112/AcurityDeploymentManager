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

namespace SMAReportCleaner
{
    public partial class MissingFiles : Form
    {
        private List<MissingFileFrame> FolderFrames = new List<MissingFileFrame>();

        public MissingFiles()
        {
            InitializeComponent();
        }

        private void MissingFiles_Shown(object sender, EventArgs e)
        {
            LoadScreen();
        }

        private void LoadScreen()
        {
            flpMissing.Controls.Clear();
            FolderFrames.Clear();
            LoadMaster();

            //If a file is copied from prod to master, the whole screen needs to be reloaded.
            //So need to pass this function inside the MissingFileFrame, so it can be called there.
            Action ls = delegate { LoadScreen(); };

            bool allTheSame = true;
            string[] allKeys = Config.AllSettings();
            for (int i = 0; i < allKeys.Length; i++)
            {
                string key = allKeys[i];
                string label = Config.LabelFromKey(key);
                if (!label.StartsWith(Config.MasterLabel) && key.StartsWith(Config.FileTypePrefix()) && !key.EndsWith(Config.BackupSuffix))
                {                    
                    MissingFileFrame mff = new MissingFileFrame(flpMissing, ls, lbMaster, gbMaster, label);
                    mff.LoadUp();
                    FolderFrames.Add(mff);
                    flpMissing.Controls.Add(mff.gb);
                    if (mff.MissingFiles.Count() != 0)
                        allTheSame = false;
                }
            }

            if(allTheSame)
            {
                MessageBox.Show("No missing files found",
                "Information", //title
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
            }

        }

        private void LoadMaster()
        {
            lbMaster.Items.Clear();
            string folder = Config.ReadSetting(Config.FileTypePrefix() + Config.MasterLabel);
            DirectoryInfo di = new DirectoryInfo(folder);
            FileInfo[] files = di.GetFiles();
            lbMaster.BeginUpdate();
            foreach (FileInfo f in files)
            {
                lbMaster.Items.Add(f.Name.ToUpper());
            }
            lbMaster.EndUpdate();
            if (lbMaster.Items.Count > 0)
                lbMaster.SelectedIndex = 0;
        }

        //https://stackoverflow.com/questions/30714980/flowlayoutpanel-autosize
        private void MissingFiles_SizeChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            //this.Height
            foreach (MissingFileFrame mff in FolderFrames)
            {
                mff.gb.Height = gbMaster.Height - 14;
                mff.lb.Height = -10 + lbMaster.Height - 3 * MissingFileFrame.BUTTON_HEIGHT + 4 * MissingFileFrame.BUTTON_SPACING;
                mff.btnMasterToProd.Top = mff.lb.Top + mff.lb.Height + MissingFileFrame.BUTTON_SPACING;
                mff.btnProdToMaster.Top = mff.lb.Top + mff.lb.Height + MissingFileFrame.BUTTON_HEIGHT + (2 * MissingFileFrame.BUTTON_SPACING);
                mff.btnBackup.Top = mff.lb.Top + mff.lb.Height + (2 * MissingFileFrame.BUTTON_HEIGHT) + (3 * MissingFileFrame.BUTTON_SPACING);
            }
            this.ResumeLayout();
        }

        private void lbMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fileName = lbMaster.SelectedItem.ToString();
            foreach (MissingFileFrame mff in FolderFrames)
            {
                int foundIndex = mff.lb.FindStringExact(fileName);
                if(foundIndex >= 0)
                   mff.lb.SelectedIndex = mff.lb.FindString(fileName);
            }
        }

        private void lbMaster_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            e.ItemHeight = listBox.Font.Height;
        }

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

            e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            e.Graphics.DrawString(lbMaster.Items[e.Index].ToString(), myFont, myBrush, e.Bounds);
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
