using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace SMAReportCleaner
{
    public enum FileType : uint
    {
        Reports,
        Customisations,
        Templates,
        XMLUI
    }

    public static class Config
    {
        public static FileType FileType;

        public const string MasterLabel = "Master";
        public const string BackupSuffix = "_B";
        public const string ReportPrefix = "R_";
        public const string CustomisationPrefix = "C_";
        public const string TemplatePrefix = "T_";
        public const string XMLUIPrefix = "H_";

        public static Color REPORTS_COLOUR;
        public static Color CUSTOMISATIONS_COLOUR;
        public static Color TEMPLATES_COLOUR;
        public static Color XMLUI_COLOUR;

        public static Color UNKNOWN_COLOUR;
        public static Color SAME_COLOUR;
        public static Color DIFFERENT_COLOUR;
        public static Color MISSING_COLOUR;

        public static Color TAB_UNKNOWN_COLOUR;
        public static Color TAB_SAME_COLOUR;
        public static Color TAB_DIFFERENT_COLOUR;
        public static Color TAB_MISSING_COLOUR;

        public static Color INSERTED_COLOUR;
        public static Color MODIFIED_COLOUR;
        public static Color DELETED_COLOUR;
        public static Color UNCHANGED_COLOUR;
        public static Color IMAGINARY_COLOUR;
        public static Color INSERTED_CHAR_COLOUR;
        public static Color DELETED_CHAR_COLOUR;

        public static List<string> keywords = new List<string>();
        public static Color keywordColor;
        public static List<string> inMemoryPrefixes = new List<string>();
        public static Color inMemoryColor;
        public static Color literalColor;
        public static List<string> knownFunctionPrefixes = new List<string>();
        public static Color knownFunctionColor;
        public static Color acurityFunctionColor;
        public static List<string> dataTypes = new List<string>();
        public static Color dataTypeColor;
        public static Color commentColor;
        public static Color includeFileColor;

        public static void InitialiseSettings()
        {
            //Master reports
            if (ReadSetting(ReportPrefix + MasterLabel) == "")
            {
                AddUpdateAppSettings(ReportPrefix + MasterLabel, "");
            }
            //Master reports backup
            if (ReadSetting(ReportPrefix + MasterLabel + BackupSuffix) == "")
            {
                AddUpdateAppSettings(ReportPrefix + MasterLabel + BackupSuffix, "");
            }
            //Master Customisations
            if (ReadSetting(CustomisationPrefix + MasterLabel) == "")
            {
                AddUpdateAppSettings(CustomisationPrefix + MasterLabel, "");
            }
            //Master Customisations backup
            if (ReadSetting(CustomisationPrefix + MasterLabel + BackupSuffix) == "")
            {
                AddUpdateAppSettings(CustomisationPrefix + MasterLabel + BackupSuffix, "");
            }
            //Master Templates
            if (ReadSetting(TemplatePrefix + MasterLabel) == "")
            {
                AddUpdateAppSettings(TemplatePrefix + MasterLabel, "");
            }
            //Master Templates backup
            if (ReadSetting(TemplatePrefix + MasterLabel + BackupSuffix) == "")
            {
                AddUpdateAppSettings(TemplatePrefix + MasterLabel + BackupSuffix, "");
            }
            //Master XMLUI
            if (ReadSetting(XMLUIPrefix + MasterLabel) == "")
            {
                AddUpdateAppSettings(XMLUIPrefix + MasterLabel, "");
            }
            //Master XMLUI backup
            if (ReadSetting(XMLUIPrefix + MasterLabel + BackupSuffix) == "")
            {
                AddUpdateAppSettings(XMLUIPrefix + MasterLabel + BackupSuffix, "");
            }

            REPORTS_COLOUR = ColorWithoutAlpha(0xe1e0ff);
            CUSTOMISATIONS_COLOUR = ColorWithoutAlpha(0xd4f9e0);
            TEMPLATES_COLOUR = ColorWithoutAlpha(0xf6d4f9);
            XMLUI_COLOUR = ColorWithoutAlpha(0xf6f9cf);

            UNKNOWN_COLOUR = ColorWithoutAlpha(0xeaeaea);
            SAME_COLOUR = ColorWithoutAlpha(0x7bef70);
            DIFFERENT_COLOUR = ColorWithoutAlpha(0xffb702);
            MISSING_COLOUR = ColorWithoutAlpha(0xfc8d8d);

            TAB_SAME_COLOUR = ColorWithoutAlpha(0x3da05c);
            TAB_DIFFERENT_COLOUR = ColorWithoutAlpha(0xcc9a04);
            TAB_MISSING_COLOUR = ColorWithoutAlpha(0xc44c4c);
            TAB_UNKNOWN_COLOUR = Color.Black;

            INSERTED_COLOUR = ColorWithoutAlpha(0xFFFF00);
            MODIFIED_COLOUR = ColorWithoutAlpha(0xDCDCFF);
            DELETED_COLOUR = ColorWithoutAlpha(0xFFC864);
            UNCHANGED_COLOUR = ColorWithoutAlpha(0xFFFFFF);
            IMAGINARY_COLOUR = ColorWithoutAlpha(0xC8C8C8);
            INSERTED_CHAR_COLOUR = ColorWithoutAlpha(0xFFFF96);
            DELETED_CHAR_COLOUR = ColorWithoutAlpha(0xC86464);

            keywordColor = Color.Blue;// Config.ColorWithoutAlpha(0xC86464);
            keywords.AddRange(new string[] {
                                    "VAR",
                                    "SET",
                                    "IF",
                                    "AND",
                                    "OR",
                                    "NOT",
                                    "ELSE",
                                    "ENDIF",
                                    "WHILE",
                                    "UNTIL",
                                    "CONTINUE",
                                    "BREAK",
                                    "ENDDO",
                                    "CASE",
                                    "OPTION",
                                    "INCLUDE",
                                    "INCLUDEONCE",
                                    "FUNCTION",
                                    "RETURN",
                                    "ENDFUNCTION",
                                    "EXIT" });

            inMemoryColor = ColorWithoutAlpha(0xFA8072);
            inMemoryPrefixes.AddRange(new string[] {
                                    "hSX","hEC","hFT","hBL","hDA","hAA","hUS","hEI","hCT","hIE","hSS","hPC","hRV","hNZ","hEH","hRG","hDH",
                                    "hMS","hAT","hEB","hFQ","hDU","hNT","hTD","hCM","hPR","hAB","hSB","hCP","hFH","hBC","hIH","hAP","hCH",
                                    "hPD","hEX","hSU","hWS","hAY","hTH","hOF","hDV","hCV","hMH","hFZ","hTS","hCO","hRL","hBJ","hTZ","hAK",
                                    "hSM","hPE","hWH","hFC","hIT","hFW","hAZ","hDY","hRE","hNX","hMD","hSD","hTR","hEN","hBE","hSA","hSP",
                                    "hCQ","hFL","hRO","hUP","hEV","hGV","hSF","hSR","hIN","hFF","hAD","hKE","hFR","hOH","hAW","hDX","hTA",
                                    "hCX","hPS","hEM","hFX","hSC","hCI","hGL","hRH","hBH","hCG","hVA","hFG","hRR","hFU","hBT","hPH","hMG",
                                    "hRC","hPT","hIR","hT4","hBK","hCS","hGR","hTT","hCL","hBA","hUN","hID","hSH","hCB","hTU","hSW","hFD",
                                    "hWU","hBP","hEE","hFV","hIP","hAU","hRF","hPO","hBN","hFP","hAC","hMC","hTC","hBD","hCR","hAQ","hBB",
                                    "hTB","hCK","hSG","hES","hCA","hPG","hAE","hFE","hPA","hCD","hIM","hAX","hRA","hTE","hBS","hED","hFS",
                                    "hOG","hMU","hVH","hAR","hJQ","hAV","hAH","hUL","hCF","hPY","hPB","hFB","hD2","hER","ANR","hZY"});

            literalColor = ColorWithoutAlpha(0xFF0000);

            dataTypes.AddRange(new string[] {"DOUBLE","STRING","SINGLE","BOOLEAN","DATE","TIME","SHORT","INTEGER","LONG" });
            dataTypeColor = ColorWithoutAlpha(0x257867);

            knownFunctionPrefixes.AddRange(new string[] {
                                    "FORMAT_","READ_","INDEX_","PERIOD_","TO_","GET_","PUT_","LENGTH","SANITISE_","DESCRIPTION",
                                    "INDEX","INDEX_OPEN_STANDARD","SUBSTR","INIT_","FREE_","TRIM_","UPPER","STR","LOWER","FORMAT",
                                    "SQL_","XML_TO_PDF", "PDF_","SYS_", "NUM_SORTED_ARRAYS", "CALC_", "UPDATE_", "UPD_", "WRITE_", "FILE_",
                                    "GUI_","REPLACE","YY","MM","DD" });
            acurityFunctionColor = Color.Purple;
            knownFunctionColor = Color.Purple;

            commentColor = Color.Green;
            includeFileColor = Color.SlateGray;
        }

        public static Color ColorWithoutAlpha(Int32 hex)
        {
            Color temp = Color.FromArgb(hex);
            return Color.FromArgb(temp.R, temp.G, temp.B);
        }

        public static string FileTypePrefix()
        {
            switch(FileType)
            {
                case FileType.Reports:
                    return ReportPrefix;
                case FileType.Customisations:
                    return CustomisationPrefix;
                case FileType.Templates:
                    return TemplatePrefix;
                case FileType.XMLUI:
                    return XMLUIPrefix;
                default:
                    return "";
            }
        }

        public static string FileTypePrefix(FileType ft)
        {
            switch (ft)
            {
                case FileType.Reports:
                    return ReportPrefix;
                case FileType.Customisations:
                    return CustomisationPrefix;
                case FileType.Templates:
                    return TemplatePrefix;
                case FileType.XMLUI:
                    return XMLUIPrefix;
                default:
                    return "";
            }
        }

        private static string FileTypeText()
        {
            switch(FileType)
            {
                case FileType.Reports:
                    return "Reports";
                case FileType.Customisations:
                    return "Customisations";
                case FileType.Templates:
                    return "Templates";
                case FileType.XMLUI:
                    return "XMLUI";
                default:
                    return "";
            }
        }

        public static string LabelFromKey(string key)
        {
            string ftp = Config.FileTypePrefix();

            //Remove first 2 characters 'R_','T_','C_','H_'
            string label = key.Substring(ftp.Length);
            //Remove last 2 chars if necessary '_B'
            if (label.EndsWith(Config.BackupSuffix))
                label = label.Substring(label.Length - Config.BackupSuffix.Length);
            return label;
        }

        public static string[] AllSettings()
        {
            return ConfigurationManager.AppSettings.AllKeys;
        }

        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings[key] == null)
                {
                    return "";
                }
                else
                    return appSettings[key];
            }
            catch (ConfigurationErrorsException)
            {
                return "Error";
            }
        }

        public static void ResetSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                appSettings.Clear();
            }
            catch (ConfigurationErrorsException)
            {
                //
            }
        }

        public static bool AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return true;
            }
            catch (ConfigurationErrorsException)
            {
                return false;
            }
        }

        public static bool ValidateAllFolderSettings()
        {
            string errors = "";
            foreach (string key in AllSettings())
            {
                if (key.StartsWith(FileTypePrefix()) && !key.EndsWith(BackupSuffix))
                {
                    string folder = Config.ReadSetting(key);
                    if (folder == "")
                        errors += "\n" + LabelFromKey(key) + ": folder not set.\n";
                    else if (!Directory.Exists(folder))
                        errors += "\n" + LabelFromKey(key) + ": " + folder + " does not exist.\n";
                }
            }

            if (errors != "")
            {
                MessageBox.Show(errors,
                    "Settings Invalid - " + FileTypeText(), //title
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }
            return true;
        }

        public static bool ValidateAllBackupSettings()
        {
            string errors = "";
            foreach (string key in AllSettings())
            {
                if (key.StartsWith(FileTypePrefix()) && key.EndsWith(BackupSuffix))
                {
                    string folder = Config.ReadSetting(key);
                    if (folder == "")
                        errors += "\n" + LabelFromKey(key) + ": backup folder not set.\n";
                    else if (!Directory.Exists(folder))
                        errors += "\n" + LabelFromKey(key) + ": " + folder + " does not exist.\n";
                }
            }

            if (errors != "")
            {
                MessageBox.Show(errors,
                    "Backup Settings Invalid - " + FileTypeText(), //title
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
            return true;
        }

        public static string ToBackupFileName(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            string extension = fi.Extension;
            string withoutExtension = filename.Substring(0, filename.Length - extension.Length);
            return withoutExtension + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
        }
    }
}
