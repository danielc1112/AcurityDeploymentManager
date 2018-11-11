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
    public partial class FInfo : Form
    {
        public string fileName;
        public string fullFileName;
        public string label;
        private string includedInFiles = "";

        public FInfo()
        {
            InitializeComponent();
        }

        private void FInfo_Load(object sender, EventArgs e)
        {
            this.Text = "File Info: " + label + " " + fileName;
            lblScreenNames.Text = GetScreenNames();
            lblIncludedIn.Text = GetIncludedInFiles();
        }

        private string GetScreenNames()
        {
            FileInfo fi = new FileInfo(fileName);
            string extension = fi.Extension;
            string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - extension.Length);
            if (fileNameWithoutExtension.Length != 4)
                return "";

            List<string> screenNames = new List<string>();
            string result = "";
            string XMLUIFolder = Config.ReadSetting(Config.XMLUIPrefix + label);

            DirectoryInfo di = new DirectoryInfo(XMLUIFolder);
            FileInfo[] files;
            files = di.GetFiles("Hierarchy.xml", SearchOption.AllDirectories);

            foreach (FileInfo f in files)
            {
                foreach (var line in File.ReadAllLines(f.FullName))
                {
                    if (line.Contains(fileNameWithoutExtension))
                    {
                        //Find the text on the line
                        int index = line.IndexOf("Text=\"");
                        string restOfLine = line.Substring(index + 6);
                        int nextIndex = restOfLine.IndexOf("\"");
                        screenNames.Add(restOfLine.Substring(0, nextIndex));
                    }                        
                }
            }

            foreach(string screenName in screenNames)
            {
                if (result == "")
                    result = screenName;
                else
                    result += ", " + screenName;
            }
            return result;
        }

        private string GetIncludedInFiles()
        {
            string folder = Config.ReadSetting(Config.FileTypePrefix() + label);
            DirectoryInfo di = new DirectoryInfo(folder);
            FileInfo[] files;
            files = di.GetFiles();
            int fileCount = files.Count();
            pbProgress.Maximum = fileCount;
            pbProgress.Value = 0;
            pbProgress.Visible = true;
            pbProgress.BringToFront();

            Task task1 = null;
            Task task2 = null;
            Task task3 = null;
            Task task4 = null;
            Task task5 = null;
            Task task6 = null;

            
            includedInFiles = "";
            for(int i = 0; i < fileCount; i += 6)
            {
                int index;
                FileInfo file;

                index = i;
                if (index < fileCount)
                {
                    file = files[index];
                    if (task1 != null)
                        task1.Wait();
                    task1 = Task.Factory.StartNew((x) => { IsIncludedInFile((FileInfo)x); }, file);
                }

                index = i + 1;
                if (index < fileCount)
                {
                    file = files[index];
                    if (task2 != null)
                        task2.Wait();
                    task2 = Task.Factory.StartNew((x) => { IsIncludedInFile((FileInfo)x); }, file);
                }

                index = i + 2;
                if (index < fileCount)
                {
                    file = files[index];
                    if (task3 != null)
                        task3.Wait();
                    task3 = Task.Factory.StartNew((x) => { IsIncludedInFile((FileInfo)x); }, file);
                }

                index = i + 3;
                if (index < fileCount)
                {
                    file = files[index];
                    if (task4 != null)
                        task4.Wait();
                    task4 = Task.Factory.StartNew((x) => { IsIncludedInFile((FileInfo)x); }, file);
                }

                index = i + 4;
                if (index < fileCount)
                {
                    file = files[index];
                    if (task5 != null)
                        task5.Wait();
                    task5 = Task.Factory.StartNew((x) => { IsIncludedInFile((FileInfo)x); }, file);
                }

                index = i + 5;
                if (index < fileCount)
                {
                    file = files[index];
                    if (task6 != null)
                        task6.Wait();
                    task6 = Task.Factory.StartNew((x) => { IsIncludedInFile((FileInfo)x); }, file);
                }
                if (pbProgress.Value + 6 <= pbProgress.Maximum)
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

            pbProgress.Visible = false;

            return includedInFiles;
        }

        private void IsIncludedInFile(FileInfo file)
        {
            using (StreamReader reader = new StreamReader(file.OpenRead()))
            {
                string fileContents = reader.ReadToEnd();
                if (fileContents.Contains("INCLUDE " + fileName) || fileContents.Contains("INCLUDEONCE " + fileName))
                {
                    if(includedInFiles == "")
                       includedInFiles = file.Name;
                    else
                       includedInFiles += "," + file.Name;
                }
            }
        }
    }
}
