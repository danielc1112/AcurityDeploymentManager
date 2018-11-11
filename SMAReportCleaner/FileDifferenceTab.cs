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
    public class FileDifferenceTab
    {
        public FileType ft;
        public TabPage tp;
        public RichTextBox rb;
        public WebBrowser wb;

        public string label;
        public FileDifferenceState fds = FileDifferenceState.Unknown;
        public bool hasChanged = false;
        public string fullProdFileName = "";

        public FileDifferenceTab(string label, FileType ft)
        {
            this.label = label;
            this.ft = ft;

            tp = new TabPage();
            tp.Text = label;
            tp.CausesValidation = true;

            rb = new RichTextBox();
            rb.Parent = tp;
            rb.WordWrap = false;
            rb.Width = tp.Width;
            rb.Height = tp.Height;
            rb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rb.Font = new Font(new FontFamily("Courier New"), 10f);
            rb.MouseDown += rb_MouseDown;
        }

        private void rb_MouseDown(object sender, MouseEventArgs e)
        {
            rb.AutoWordSelection = true;
            rb.AutoWordSelection = false;
        }
    }
}
