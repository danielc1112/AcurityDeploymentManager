using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;

namespace SMAReportCleaner
{
    public partial class Settings : Form
    {
        private List<SettingFrame> ReportSettingFrames = new List<SettingFrame>();
        private List<SettingFrame> CustomisationSettingFrames = new List<SettingFrame>();
        private List<SettingFrame> TemplateSettingFrames = new List<SettingFrame>();
        private List<SettingFrame> XMLUISettingFrames = new List<SettingFrame>();

        //Required to know which frame was last selected to remove
        private SettingFrame selectedFrame = null;
        private Action<SettingFrame> SetSelectedFrameAction;

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //If a file is copied from prod to master, the whole screen needs to be reloaded.
            //So need to pass this function inside the MissingFileFrame, so it can be called there.
            SetSelectedFrameAction = SetSelectedFrame;

            string[] allKeys = Config.AllSettings();
            for(int i=0;i< allKeys.Length;i++)
            {
                SettingFrame sf;
                string key = allKeys[i];
                string label = Config.LabelFromKey(key);
                if (key.StartsWith(Config.ReportPrefix) && !key.EndsWith(Config.BackupSuffix))
                {
                    sf = new SettingFrame(FileType.Reports, flpReports, label, 2, false, SetSelectedFrameAction);
                    flpReports.Controls.Add(sf.gb);
                    ReportSettingFrames.Add(sf);
                }
                else if (key.StartsWith(Config.CustomisationPrefix) && !key.EndsWith(Config.BackupSuffix))
                {
                    sf = new SettingFrame(FileType.Customisations, flpCustomisations, label, 2, false, SetSelectedFrameAction);
                    flpCustomisations.Controls.Add(sf.gb);
                    CustomisationSettingFrames.Add(sf);
                }
                else if (key.StartsWith(Config.TemplatePrefix) && !key.EndsWith(Config.BackupSuffix))
                {
                    sf = new SettingFrame(FileType.Templates, flpTemplates, label, 2, false, SetSelectedFrameAction);
                    flpTemplates.Controls.Add(sf.gb);
                    TemplateSettingFrames.Add(sf);
                }
                else if (key.StartsWith(Config.XMLUIPrefix) && !key.EndsWith(Config.BackupSuffix))
                {
                    sf = new SettingFrame(FileType.XMLUI, flpXMLUI, label, 2, false, SetSelectedFrameAction);
                    flpXMLUI.Controls.Add(sf.gb);
                    XMLUISettingFrames.Add(sf);
                }
            }

            //switch tabs depending on file type chosen
            switch(Config.FileType)
            {
                case FileType.Reports:
                    tcSettings.SelectedIndex = 0;
                    break;
                case FileType.Customisations:
                    tcSettings.SelectedIndex = 1;
                    break;
                case FileType.Templates:
                    tcSettings.SelectedIndex = 2;
                    break;
                case FileType.XMLUI:
                    tcSettings.SelectedIndex = 3;
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SettingFrame sf;

            switch (tcSettings.SelectedIndex)
            {
                case 0:
                    sf = new SettingFrame(FileType.Reports, flpReports, "", 2, true, SetSelectedFrameAction);
                    ReportSettingFrames.Add(sf);
                    selectedFrame = sf;
                    break;
                case 1:
                    sf = new SettingFrame(FileType.Customisations, flpCustomisations, "", 2, true, SetSelectedFrameAction);
                    CustomisationSettingFrames.Add(sf);
                    selectedFrame = sf;
                    break;
                case 2:
                    sf = new SettingFrame(FileType.Templates, flpTemplates, "", 2, true, SetSelectedFrameAction);
                    TemplateSettingFrames.Add(sf);
                    selectedFrame = sf;
                    break;
                case 3:
                    sf = new SettingFrame(FileType.XMLUI, flpXMLUI, "", 2, true, SetSelectedFrameAction);
                    XMLUISettingFrames.Add(sf);
                    selectedFrame = sf;
                    break;
            }
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            if((selectedFrame != null) && (selectedFrame.label != Config.MasterLabel))
            {
                switch (tcSettings.SelectedIndex)
                {
                    case 0: //Reports
                        flpReports.Controls.Remove(selectedFrame.gb);
                        ReportSettingFrames.Remove(selectedFrame);                        
                        break;
                    case 1: //Customisations
                        flpCustomisations.Controls.Remove(selectedFrame.gb);
                        CustomisationSettingFrames.Remove(selectedFrame);
                        break;
                    case 2: //Templates
                        flpTemplates.Controls.Remove(selectedFrame.gb);
                        TemplateSettingFrames.Remove(selectedFrame);
                        break;
                    case 3: //XMLUI
                        flpXMLUI.Controls.Remove(selectedFrame.gb);
                        XMLUISettingFrames.Remove(selectedFrame);
                        break;
                }

                selectedFrame = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config.ResetSettings();
            //Save to app.config
            foreach(SettingFrame sf in ReportSettingFrames)
            {
                //Don't save empty ones
                if ((sf.tbSetting.Text.Trim() != ""))
                {
                    Config.AddUpdateAppSettings(Config.ReportPrefix + sf.label, sf.tbFolder.Text.Trim());
                    Config.AddUpdateAppSettings(Config.ReportPrefix + sf.label + Config.BackupSuffix, sf.tbBackupFolder.Text.Trim());
                    sf.tbSetting.Enabled = false;
                }
            }
            foreach (SettingFrame sf in CustomisationSettingFrames)
            {
                //Don't save empty ones
                if ((sf.tbSetting.Text.Trim() != ""))
                {
                    Config.AddUpdateAppSettings(Config.CustomisationPrefix + sf.label, sf.tbFolder.Text.Trim());
                    Config.AddUpdateAppSettings(Config.CustomisationPrefix + sf.label + Config.BackupSuffix, sf.tbBackupFolder.Text.Trim());
                    sf.tbSetting.Enabled = false;
                }
            }
            foreach (SettingFrame sf in TemplateSettingFrames)
            {
                //Don't save empty ones
                if ((sf.tbSetting.Text.Trim() != ""))
                {
                    Config.AddUpdateAppSettings(Config.TemplatePrefix + sf.label, sf.tbFolder.Text.Trim());
                    Config.AddUpdateAppSettings(Config.TemplatePrefix + sf.label + Config.BackupSuffix, sf.tbBackupFolder.Text.Trim());
                    sf.tbSetting.Enabled = false;
                }
            }
            foreach (SettingFrame sf in XMLUISettingFrames)
            {
                //Don't save empty ones
                if ((sf.tbSetting.Text.Trim() != ""))
                {
                    Config.AddUpdateAppSettings(Config.XMLUIPrefix + sf.label, sf.tbFolder.Text.Trim());
                    Config.AddUpdateAppSettings(Config.XMLUIPrefix + sf.label + Config.BackupSuffix, sf.tbBackupFolder.Text.Trim());
                    sf.tbSetting.Enabled = false;
                }
            }

            MessageBox.Show("Settings saved",
                "Settings saved", //title
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void SetSelectedFrame(SettingFrame sf)
        {
            selectedFrame = sf;
        }

    }
}
