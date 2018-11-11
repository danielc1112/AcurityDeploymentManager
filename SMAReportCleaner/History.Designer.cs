namespace SMAReportCleaner
{
    partial class History
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFullOldFileName = new System.Windows.Forms.Label();
            this.lblFullNewFileName = new System.Windows.Forms.Label();
            this.scFiles = new System.Windows.Forms.SplitContainer();
            this.OldLineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.rbOld = new System.Windows.Forms.RichTextBox();
            this.NewLineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.rbNew = new System.Windows.Forms.RichTextBox();
            this.lblOldSize = new System.Windows.Forms.Label();
            this.lblOldMod = new System.Windows.Forms.Label();
            this.lblNewSize = new System.Windows.Forms.Label();
            this.lblNewMod = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scFiles)).BeginInit();
            this.scFiles.Panel1.SuspendLayout();
            this.scFiles.Panel2.SuspendLayout();
            this.scFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFullOldFileName
            // 
            this.lblFullOldFileName.AutoSize = true;
            this.lblFullOldFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullOldFileName.Location = new System.Drawing.Point(12, 9);
            this.lblFullOldFileName.Name = "lblFullOldFileName";
            this.lblFullOldFileName.Size = new System.Drawing.Size(111, 17);
            this.lblFullOldFileName.TabIndex = 22;
            this.lblFullOldFileName.Text = "FullOldFileName";
            // 
            // lblFullNewFileName
            // 
            this.lblFullNewFileName.AutoSize = true;
            this.lblFullNewFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullNewFileName.Location = new System.Drawing.Point(463, 9);
            this.lblFullNewFileName.Name = "lblFullNewFileName";
            this.lblFullNewFileName.Size = new System.Drawing.Size(116, 17);
            this.lblFullNewFileName.TabIndex = 23;
            this.lblFullNewFileName.Text = "FullNewFileName";
            // 
            // scFiles
            // 
            this.scFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scFiles.Location = new System.Drawing.Point(12, 29);
            this.scFiles.Name = "scFiles";
            // 
            // scFiles.Panel1
            // 
            this.scFiles.Panel1.Controls.Add(this.OldLineNumberTextBox);
            this.scFiles.Panel1.Controls.Add(this.rbOld);
            // 
            // scFiles.Panel2
            // 
            this.scFiles.Panel2.Controls.Add(this.NewLineNumberTextBox);
            this.scFiles.Panel2.Controls.Add(this.rbNew);
            this.scFiles.Panel2.SizeChanged += new System.EventHandler(this.splitContainer1_Panel2_SizeChanged);
            this.scFiles.Size = new System.Drawing.Size(895, 463);
            this.scFiles.SplitterDistance = 447;
            this.scFiles.TabIndex = 24;
            // 
            // OldLineNumberTextBox
            // 
            this.OldLineNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OldLineNumberTextBox.Font = new System.Drawing.Font("Courier New", 10F);
            this.OldLineNumberTextBox.Location = new System.Drawing.Point(3, 3);
            this.OldLineNumberTextBox.Name = "OldLineNumberTextBox";
            this.OldLineNumberTextBox.ReadOnly = true;
            this.OldLineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.OldLineNumberTextBox.Size = new System.Drawing.Size(30, 457);
            this.OldLineNumberTextBox.TabIndex = 21;
            this.OldLineNumberTextBox.Text = "";
            this.OldLineNumberTextBox.Visible = false;
            // 
            // rbOld
            // 
            this.rbOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOld.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOld.Location = new System.Drawing.Point(39, 3);
            this.rbOld.Name = "rbOld";
            this.rbOld.ReadOnly = true;
            this.rbOld.Size = new System.Drawing.Size(405, 457);
            this.rbOld.TabIndex = 20;
            this.rbOld.Text = "";
            this.rbOld.WordWrap = false;
            this.rbOld.SelectionChanged += new System.EventHandler(this.rbOld_SelectionChanged);
            this.rbOld.VScroll += new System.EventHandler(this.rbOld_VScroll);
            this.rbOld.FontChanged += new System.EventHandler(this.rbOld_FontChanged);
            this.rbOld.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rbOld_MouseDown);
            // 
            // NewLineNumberTextBox
            // 
            this.NewLineNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NewLineNumberTextBox.Font = new System.Drawing.Font("Courier New", 10F);
            this.NewLineNumberTextBox.Location = new System.Drawing.Point(3, 3);
            this.NewLineNumberTextBox.Name = "NewLineNumberTextBox";
            this.NewLineNumberTextBox.ReadOnly = true;
            this.NewLineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.NewLineNumberTextBox.Size = new System.Drawing.Size(30, 457);
            this.NewLineNumberTextBox.TabIndex = 22;
            this.NewLineNumberTextBox.Text = "";
            this.NewLineNumberTextBox.Visible = false;
            // 
            // rbNew
            // 
            this.rbNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbNew.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNew.Location = new System.Drawing.Point(39, 3);
            this.rbNew.Name = "rbNew";
            this.rbNew.ReadOnly = true;
            this.rbNew.Size = new System.Drawing.Size(395, 457);
            this.rbNew.TabIndex = 21;
            this.rbNew.Text = "";
            this.rbNew.WordWrap = false;
            this.rbNew.SelectionChanged += new System.EventHandler(this.rbNew_SelectionChanged);
            this.rbNew.VScroll += new System.EventHandler(this.rbNew_VScroll);
            this.rbNew.FontChanged += new System.EventHandler(this.rbNew_FontChanged);
            this.rbNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rbNew_MouseDown);
            // 
            // lblOldSize
            // 
            this.lblOldSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOldSize.AutoSize = true;
            this.lblOldSize.Location = new System.Drawing.Point(154, 496);
            this.lblOldSize.Name = "lblOldSize";
            this.lblOldSize.Size = new System.Drawing.Size(53, 13);
            this.lblOldSize.TabIndex = 26;
            this.lblOldSize.Text = "lblOldSize";
            // 
            // lblOldMod
            // 
            this.lblOldMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOldMod.AutoSize = true;
            this.lblOldMod.Location = new System.Drawing.Point(12, 496);
            this.lblOldMod.Name = "lblOldMod";
            this.lblOldMod.Size = new System.Drawing.Size(54, 13);
            this.lblOldMod.TabIndex = 25;
            this.lblOldMod.Text = "lblOldMod";
            // 
            // lblNewSize
            // 
            this.lblNewSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNewSize.AutoSize = true;
            this.lblNewSize.Location = new System.Drawing.Point(605, 496);
            this.lblNewSize.Name = "lblNewSize";
            this.lblNewSize.Size = new System.Drawing.Size(59, 13);
            this.lblNewSize.TabIndex = 28;
            this.lblNewSize.Text = "lblNewSize";
            // 
            // lblNewMod
            // 
            this.lblNewMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNewMod.AutoSize = true;
            this.lblNewMod.Location = new System.Drawing.Point(463, 496);
            this.lblNewMod.Name = "lblNewMod";
            this.lblNewMod.Size = new System.Drawing.Size(60, 13);
            this.lblNewMod.TabIndex = 27;
            this.lblNewMod.Text = "lblNewMod";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.Location = new System.Drawing.Point(334, 495);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(58, 21);
            this.btnPrevious.TabIndex = 29;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(398, 495);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 21);
            this.btnNext.TabIndex = 30;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 518);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.lblNewSize);
            this.Controls.Add(this.lblNewMod);
            this.Controls.Add(this.lblOldSize);
            this.Controls.Add(this.lblOldMod);
            this.Controls.Add(this.scFiles);
            this.Controls.Add(this.lblFullNewFileName);
            this.Controls.Add(this.lblFullOldFileName);
            this.Name = "History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LastChange_Load);
            this.SizeChanged += new System.EventHandler(this.LastChange_SizeChanged);
            this.scFiles.Panel1.ResumeLayout(false);
            this.scFiles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scFiles)).EndInit();
            this.scFiles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblFullOldFileName;
        private System.Windows.Forms.Label lblFullNewFileName;
        private System.Windows.Forms.SplitContainer scFiles;
        private System.Windows.Forms.RichTextBox OldLineNumberTextBox;
        private System.Windows.Forms.RichTextBox rbOld;
        private System.Windows.Forms.RichTextBox NewLineNumberTextBox;
        private System.Windows.Forms.RichTextBox rbNew;
        private System.Windows.Forms.Label lblOldSize;
        private System.Windows.Forms.Label lblOldMod;
        private System.Windows.Forms.Label lblNewSize;
        private System.Windows.Forms.Label lblNewMod;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
    }
}