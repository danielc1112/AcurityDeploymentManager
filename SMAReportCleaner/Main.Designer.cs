namespace SMAReportCleaner
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.lbMaster = new System.Windows.Forms.ListBox();
            this.cmsFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDeploy = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSettings = new System.Windows.Forms.Button();
            this.scFiles = new System.Windows.Forms.SplitContainer();
            this.btnMasterInfo = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.btnMasterNotepad = new System.Windows.Forms.Button();
            this.btnMasterHistory = new System.Windows.Forms.Button();
            this.lblMasterSize = new System.Windows.Forms.Label();
            this.lblMasterMod = new System.Windows.Forms.Label();
            this.MasterLineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.rbMaster = new System.Windows.Forms.RichTextBox();
            this.btnSaveMaster = new System.Windows.Forms.Button();
            this.lblMaster = new System.Windows.Forms.Label();
            this.btnProdInfo = new System.Windows.Forms.Button();
            this.btnProdNotepad = new System.Windows.Forms.Button();
            this.btnProdHistory = new System.Windows.Forms.Button();
            this.lblProdSize = new System.Windows.Forms.Label();
            this.lblProdMod = new System.Windows.Forms.Label();
            this.ProdLineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.btnSaveProd = new System.Windows.Forms.Button();
            this.tcFiles = new System.Windows.Forms.TabControl();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbFileType = new System.Windows.Forms.ComboBox();
            this.btnMissingFiles = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.textChangedTimer = new System.Windows.Forms.Timer(this.components);
            this.lblSharepoint = new System.Windows.Forms.LinkLabel();
            this.lblSSKB = new System.Windows.Forms.LinkLabel();
            this.tbFont = new System.Windows.Forms.TrackBar();
            this.gbOrderBy = new System.Windows.Forms.GroupBox();
            this.rbModified = new System.Windows.Forms.RadioButton();
            this.rbName = new System.Windows.Forms.RadioButton();
            this.lblIress = new System.Windows.Forms.LinkLabel();
            this.cmsFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scFiles)).BeginInit();
            this.scFiles.Panel1.SuspendLayout();
            this.scFiles.Panel2.SuspendLayout();
            this.scFiles.SuspendLayout();
            this.gbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFont)).BeginInit();
            this.gbOrderBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMaster
            // 
            this.lbMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbMaster.ContextMenuStrip = this.cmsFile;
            this.lbMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaster.FormattingEnabled = true;
            this.lbMaster.ItemHeight = 15;
            this.lbMaster.Location = new System.Drawing.Point(15, 47);
            this.lbMaster.Name = "lbMaster";
            this.lbMaster.Size = new System.Drawing.Size(168, 394);
            this.lbMaster.TabIndex = 0;
            this.lbMaster.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbMaster_DrawItem);
            this.lbMaster.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbMaster_MeasureItem);
            this.lbMaster.SelectedIndexChanged += new System.EventHandler(this.lbMaster_SelectedIndexChanged);
            this.lbMaster.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbMaster_KeyPress);
            this.lbMaster.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbMaster_MouseDown);
            // 
            // cmsFile
            // 
            this.cmsFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDeploy,
            this.miBackup});
            this.cmsFile.Name = "cmsFile";
            this.cmsFile.Size = new System.Drawing.Size(218, 48);
            this.cmsFile.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsFile_ItemClicked);
            // 
            // miDeploy
            // 
            this.miDeploy.Name = "miDeploy";
            this.miDeploy.Size = new System.Drawing.Size(217, 22);
            this.miDeploy.Text = "Deploy to all environments";
            // 
            // miBackup
            // 
            this.miBackup.Name = "miBackup";
            this.miBackup.Size = new System.Drawing.Size(217, 22);
            this.miBackup.Text = "Backup in all environments";
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Location = new System.Drawing.Point(14, 560);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(90, 26);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings...";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // scFiles
            // 
            this.scFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scFiles.BackColor = System.Drawing.SystemColors.ControlLight;
            this.scFiles.Location = new System.Drawing.Point(189, 3);
            this.scFiles.Name = "scFiles";
            // 
            // scFiles.Panel1
            // 
            this.scFiles.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.scFiles.Panel1.Controls.Add(this.btnMasterInfo);
            this.scFiles.Panel1.Controls.Add(this.pbProgress);
            this.scFiles.Panel1.Controls.Add(this.gbSearch);
            this.scFiles.Panel1.Controls.Add(this.btnMasterNotepad);
            this.scFiles.Panel1.Controls.Add(this.btnMasterHistory);
            this.scFiles.Panel1.Controls.Add(this.lblMasterSize);
            this.scFiles.Panel1.Controls.Add(this.lblMasterMod);
            this.scFiles.Panel1.Controls.Add(this.MasterLineNumberTextBox);
            this.scFiles.Panel1.Controls.Add(this.rbMaster);
            this.scFiles.Panel1.Controls.Add(this.btnSaveMaster);
            this.scFiles.Panel1.Controls.Add(this.lblMaster);
            // 
            // scFiles.Panel2
            // 
            this.scFiles.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.scFiles.Panel2.Controls.Add(this.btnProdInfo);
            this.scFiles.Panel2.Controls.Add(this.btnProdNotepad);
            this.scFiles.Panel2.Controls.Add(this.btnProdHistory);
            this.scFiles.Panel2.Controls.Add(this.lblProdSize);
            this.scFiles.Panel2.Controls.Add(this.lblProdMod);
            this.scFiles.Panel2.Controls.Add(this.ProdLineNumberTextBox);
            this.scFiles.Panel2.Controls.Add(this.btnSaveProd);
            this.scFiles.Panel2.Controls.Add(this.tcFiles);
            this.scFiles.Size = new System.Drawing.Size(935, 613);
            this.scFiles.SplitterDistance = 419;
            this.scFiles.SplitterWidth = 8;
            this.scFiles.TabIndex = 7;
            // 
            // btnMasterInfo
            // 
            this.btnMasterInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMasterInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMasterInfo.Location = new System.Drawing.Point(236, 583);
            this.btnMasterInfo.Name = "btnMasterInfo";
            this.btnMasterInfo.Size = new System.Drawing.Size(60, 26);
            this.btnMasterInfo.TabIndex = 24;
            this.btnMasterInfo.Text = "Info";
            this.btnMasterInfo.UseVisualStyleBackColor = true;
            this.btnMasterInfo.Click += new System.EventHandler(this.btnMasterInfo_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(6, 7);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(410, 21);
            this.pbProgress.TabIndex = 0;
            // 
            // gbSearch
            // 
            this.gbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbSearch.Controls.Add(this.button2);
            this.gbSearch.Controls.Add(this.button1);
            this.gbSearch.Controls.Add(this.label2);
            this.gbSearch.Controls.Add(this.textBox1);
            this.gbSearch.Controls.Add(this.btnFindNext);
            this.gbSearch.Controls.Add(this.label1);
            this.gbSearch.Controls.Add(this.tbFind);
            this.gbSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbSearch.Location = new System.Drawing.Point(6, 506);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(348, 71);
            this.gbSearch.TabIndex = 23;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Search";
            this.gbSearch.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(243, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Replace";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Replace With:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            this.textBox1.TabIndex = 3;
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(295, 17);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(47, 23);
            this.btnFindNext.TabIndex = 2;
            this.btnFindNext.Text = "Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find what:";
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(87, 19);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(150, 20);
            this.tbFind.TabIndex = 0;
            // 
            // btnMasterNotepad
            // 
            this.btnMasterNotepad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMasterNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMasterNotepad.Location = new System.Drawing.Point(379, 583);
            this.btnMasterNotepad.Name = "btnMasterNotepad";
            this.btnMasterNotepad.Size = new System.Drawing.Size(89, 26);
            this.btnMasterNotepad.TabIndex = 21;
            this.btnMasterNotepad.Text = "Notepad++";
            this.btnMasterNotepad.UseVisualStyleBackColor = true;
            this.btnMasterNotepad.Click += new System.EventHandler(this.btnMasterNotepad_Click);
            // 
            // btnMasterHistory
            // 
            this.btnMasterHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMasterHistory.Enabled = false;
            this.btnMasterHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMasterHistory.Location = new System.Drawing.Point(302, 583);
            this.btnMasterHistory.Name = "btnMasterHistory";
            this.btnMasterHistory.Size = new System.Drawing.Size(71, 26);
            this.btnMasterHistory.TabIndex = 20;
            this.btnMasterHistory.Text = "History";
            this.btnMasterHistory.UseVisualStyleBackColor = true;
            this.btnMasterHistory.Click += new System.EventHandler(this.btnMasterHistory_Click);
            // 
            // lblMasterSize
            // 
            this.lblMasterSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMasterSize.AutoSize = true;
            this.lblMasterSize.Location = new System.Drawing.Point(142, 590);
            this.lblMasterSize.Name = "lblMasterSize";
            this.lblMasterSize.Size = new System.Drawing.Size(69, 13);
            this.lblMasterSize.TabIndex = 19;
            this.lblMasterSize.Text = "lblMasterSize";
            // 
            // lblMasterMod
            // 
            this.lblMasterMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMasterMod.AutoSize = true;
            this.lblMasterMod.Location = new System.Drawing.Point(3, 590);
            this.lblMasterMod.Name = "lblMasterMod";
            this.lblMasterMod.Size = new System.Drawing.Size(70, 13);
            this.lblMasterMod.TabIndex = 18;
            this.lblMasterMod.Text = "lblMasterMod";
            // 
            // MasterLineNumberTextBox
            // 
            this.MasterLineNumberTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.MasterLineNumberTextBox.Font = new System.Drawing.Font("Courier New", 10F);
            this.MasterLineNumberTextBox.Location = new System.Drawing.Point(3, 33);
            this.MasterLineNumberTextBox.Name = "MasterLineNumberTextBox";
            this.MasterLineNumberTextBox.ReadOnly = true;
            this.MasterLineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.MasterLineNumberTextBox.Size = new System.Drawing.Size(30, 547);
            this.MasterLineNumberTextBox.TabIndex = 15;
            this.MasterLineNumberTextBox.Text = "";
            this.MasterLineNumberTextBox.Visible = false;
            this.MasterLineNumberTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MasterLineNumberTextBox_MouseDown);
            // 
            // rbMaster
            // 
            this.rbMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbMaster.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMaster.Location = new System.Drawing.Point(3, 33);
            this.rbMaster.Name = "rbMaster";
            this.rbMaster.Size = new System.Drawing.Size(413, 547);
            this.rbMaster.TabIndex = 17;
            this.rbMaster.Text = "";
            this.rbMaster.WordWrap = false;
            this.rbMaster.SelectionChanged += new System.EventHandler(this.rbMaster_SelectionChanged);
            this.rbMaster.VScroll += new System.EventHandler(this.rbMaster_VScroll);
            this.rbMaster.FontChanged += new System.EventHandler(this.rbMaster_FontChanged);
            this.rbMaster.TextChanged += new System.EventHandler(this.rbMaster_TextChanged);
            this.rbMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbMaster_KeyDown);
            // 
            // btnSaveMaster
            // 
            this.btnSaveMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveMaster.Enabled = false;
            this.btnSaveMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveMaster.Location = new System.Drawing.Point(176, 583);
            this.btnSaveMaster.Name = "btnSaveMaster";
            this.btnSaveMaster.Size = new System.Drawing.Size(75, 26);
            this.btnSaveMaster.TabIndex = 14;
            this.btnSaveMaster.Text = "Save";
            this.btnSaveMaster.UseVisualStyleBackColor = true;
            this.btnSaveMaster.Click += new System.EventHandler(this.btnSaveMaster_Click);
            // 
            // lblMaster
            // 
            this.lblMaster.AutoSize = true;
            this.lblMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaster.Location = new System.Drawing.Point(3, 11);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(51, 17);
            this.lblMaster.TabIndex = 8;
            this.lblMaster.Text = "Master";
            // 
            // btnProdInfo
            // 
            this.btnProdInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProdInfo.Enabled = false;
            this.btnProdInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProdInfo.Location = new System.Drawing.Point(270, 583);
            this.btnProdInfo.Name = "btnProdInfo";
            this.btnProdInfo.Size = new System.Drawing.Size(60, 26);
            this.btnProdInfo.TabIndex = 23;
            this.btnProdInfo.Text = "Info";
            this.btnProdInfo.UseVisualStyleBackColor = true;
            this.btnProdInfo.Click += new System.EventHandler(this.btnProdInfo_Click);
            // 
            // btnProdNotepad
            // 
            this.btnProdNotepad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProdNotepad.Enabled = false;
            this.btnProdNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProdNotepad.Location = new System.Drawing.Point(406, 583);
            this.btnProdNotepad.Name = "btnProdNotepad";
            this.btnProdNotepad.Size = new System.Drawing.Size(88, 26);
            this.btnProdNotepad.TabIndex = 22;
            this.btnProdNotepad.Text = "Notepad++";
            this.btnProdNotepad.UseVisualStyleBackColor = true;
            this.btnProdNotepad.Click += new System.EventHandler(this.btnProdNotepad_Click);
            // 
            // btnProdHistory
            // 
            this.btnProdHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProdHistory.Enabled = false;
            this.btnProdHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProdHistory.Location = new System.Drawing.Point(324, 583);
            this.btnProdHistory.Name = "btnProdHistory";
            this.btnProdHistory.Size = new System.Drawing.Size(76, 26);
            this.btnProdHistory.TabIndex = 19;
            this.btnProdHistory.Text = "History";
            this.btnProdHistory.UseVisualStyleBackColor = true;
            this.btnProdHistory.Click += new System.EventHandler(this.btnProdHistory_Click);
            // 
            // lblProdSize
            // 
            this.lblProdSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProdSize.AutoSize = true;
            this.lblProdSize.Location = new System.Drawing.Point(148, 590);
            this.lblProdSize.Name = "lblProdSize";
            this.lblProdSize.Size = new System.Drawing.Size(59, 13);
            this.lblProdSize.TabIndex = 18;
            this.lblProdSize.Text = "lblProdSize";
            // 
            // lblProdMod
            // 
            this.lblProdMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProdMod.AutoSize = true;
            this.lblProdMod.Location = new System.Drawing.Point(3, 590);
            this.lblProdMod.Name = "lblProdMod";
            this.lblProdMod.Size = new System.Drawing.Size(60, 13);
            this.lblProdMod.TabIndex = 17;
            this.lblProdMod.Text = "lblProdMod";
            // 
            // ProdLineNumberTextBox
            // 
            this.ProdLineNumberTextBox.Font = new System.Drawing.Font("Courier New", 10F);
            this.ProdLineNumberTextBox.Location = new System.Drawing.Point(3, 32);
            this.ProdLineNumberTextBox.Name = "ProdLineNumberTextBox";
            this.ProdLineNumberTextBox.ReadOnly = true;
            this.ProdLineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.ProdLineNumberTextBox.Size = new System.Drawing.Size(28, 548);
            this.ProdLineNumberTextBox.TabIndex = 16;
            this.ProdLineNumberTextBox.Text = "";
            this.ProdLineNumberTextBox.Visible = false;
            this.ProdLineNumberTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProdLineNumberTextBox_MouseDown);
            // 
            // btnSaveProd
            // 
            this.btnSaveProd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveProd.Enabled = false;
            this.btnSaveProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveProd.Location = new System.Drawing.Point(225, 583);
            this.btnSaveProd.Name = "btnSaveProd";
            this.btnSaveProd.Size = new System.Drawing.Size(75, 26);
            this.btnSaveProd.TabIndex = 3;
            this.btnSaveProd.Text = "Save";
            this.btnSaveProd.UseVisualStyleBackColor = true;
            this.btnSaveProd.Click += new System.EventHandler(this.btnSaveProd_Click);
            // 
            // tcFiles
            // 
            this.tcFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcFiles.Location = new System.Drawing.Point(3, 8);
            this.tcFiles.Name = "tcFiles";
            this.tcFiles.SelectedIndex = 0;
            this.tcFiles.Size = new System.Drawing.Size(502, 569);
            this.tcFiles.TabIndex = 2;
            this.tcFiles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tcFiles_DrawItem);
            this.tcFiles.SelectedIndexChanged += new System.EventHandler(this.tcFiles_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(14, 496);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(168, 26);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cbFileType
            // 
            this.cbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbFileType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFileType.FormattingEnabled = true;
            this.cbFileType.Items.AddRange(new object[] {
            "Reports",
            "Customisations",
            "Templates",
            "XMLUI"});
            this.cbFileType.Location = new System.Drawing.Point(15, 11);
            this.cbFileType.Name = "cbFileType";
            this.cbFileType.Size = new System.Drawing.Size(167, 24);
            this.cbFileType.TabIndex = 0;
            this.cbFileType.SelectedIndexChanged += new System.EventHandler(this.cbFileType_SelectedIndexChanged);
            // 
            // btnMissingFiles
            // 
            this.btnMissingFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMissingFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMissingFiles.Location = new System.Drawing.Point(14, 528);
            this.btnMissingFiles.Name = "btnMissingFiles";
            this.btnMissingFiles.Size = new System.Drawing.Size(168, 26);
            this.btnMissingFiles.TabIndex = 11;
            this.btnMissingFiles.Text = "Missing Files...";
            this.btnMissingFiles.UseVisualStyleBackColor = true;
            this.btnMissingFiles.Click += new System.EventHandler(this.btnMissingFiles_Click);
            // 
            // btnStats
            // 
            this.btnStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStats.Location = new System.Drawing.Point(110, 560);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(72, 26);
            this.btnStats.TabIndex = 13;
            this.btnStats.Text = "Stats";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // textChangedTimer
            // 
            this.textChangedTimer.Interval = 2000;
            this.textChangedTimer.Tick += new System.EventHandler(this.textChangedTimer_Tick);
            // 
            // lblSharepoint
            // 
            this.lblSharepoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSharepoint.AutoSize = true;
            this.lblSharepoint.Location = new System.Drawing.Point(64, 597);
            this.lblSharepoint.Name = "lblSharepoint";
            this.lblSharepoint.Size = new System.Drawing.Size(58, 13);
            this.lblSharepoint.TabIndex = 15;
            this.lblSharepoint.TabStop = true;
            this.lblSharepoint.Text = "Sharepoint";
            this.lblSharepoint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSharepoint_LinkClicked);
            // 
            // lblSSKB
            // 
            this.lblSSKB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSSKB.AutoSize = true;
            this.lblSSKB.Location = new System.Drawing.Point(119, 597);
            this.lblSSKB.Name = "lblSSKB";
            this.lblSSKB.Size = new System.Drawing.Size(35, 13);
            this.lblSSKB.TabIndex = 16;
            this.lblSSKB.TabStop = true;
            this.lblSSKB.Text = "SSKB";
            this.lblSSKB.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSSKB_LinkClicked);
            // 
            // tbFont
            // 
            this.tbFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbFont.LargeChange = 2;
            this.tbFont.Location = new System.Drawing.Point(3, 597);
            this.tbFont.Maximum = 16;
            this.tbFont.Minimum = 6;
            this.tbFont.Name = "tbFont";
            this.tbFont.Size = new System.Drawing.Size(63, 45);
            this.tbFont.TabIndex = 17;
            this.tbFont.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbFont.Value = 6;
            this.tbFont.Scroll += new System.EventHandler(this.tbFont_Scroll);
            // 
            // gbOrderBy
            // 
            this.gbOrderBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbOrderBy.Controls.Add(this.rbModified);
            this.gbOrderBy.Controls.Add(this.rbName);
            this.gbOrderBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOrderBy.Location = new System.Drawing.Point(15, 451);
            this.gbOrderBy.Name = "gbOrderBy";
            this.gbOrderBy.Size = new System.Drawing.Size(167, 39);
            this.gbOrderBy.TabIndex = 18;
            this.gbOrderBy.TabStop = false;
            this.gbOrderBy.Text = "Order By";
            // 
            // rbModified
            // 
            this.rbModified.AutoSize = true;
            this.rbModified.Location = new System.Drawing.Point(83, 16);
            this.rbModified.Name = "rbModified";
            this.rbModified.Size = new System.Drawing.Size(65, 17);
            this.rbModified.TabIndex = 1;
            this.rbModified.TabStop = true;
            this.rbModified.Text = "Modified";
            this.rbModified.UseVisualStyleBackColor = true;
            this.rbModified.CheckedChanged += new System.EventHandler(this.rbModified_CheckedChanged);
            // 
            // rbName
            // 
            this.rbName.AutoSize = true;
            this.rbName.Checked = true;
            this.rbName.Location = new System.Drawing.Point(14, 16);
            this.rbName.Name = "rbName";
            this.rbName.Size = new System.Drawing.Size(53, 17);
            this.rbName.TabIndex = 0;
            this.rbName.TabStop = true;
            this.rbName.Text = "Name";
            this.rbName.UseVisualStyleBackColor = true;
            this.rbName.CheckedChanged += new System.EventHandler(this.rbName_CheckedChanged);
            // 
            // lblIress
            // 
            this.lblIress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIress.AutoSize = true;
            this.lblIress.Location = new System.Drawing.Point(154, 597);
            this.lblIress.Name = "lblIress";
            this.lblIress.Size = new System.Drawing.Size(29, 13);
            this.lblIress.TabIndex = 19;
            this.lblIress.TabStop = true;
            this.lblIress.Text = "Iress";
            this.lblIress.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblIress_LinkClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1136, 621);
            this.Controls.Add(this.lblIress);
            this.Controls.Add(this.gbOrderBy);
            this.Controls.Add(this.tbFont);
            this.Controls.Add(this.lblSSKB);
            this.Controls.Add(this.lblSharepoint);
            this.Controls.Add(this.btnStats);
            this.Controls.Add(this.btnMissingFiles);
            this.Controls.Add(this.cbFileType);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.scFiles);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lbMaster);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acurity Deployment Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.BackColorChanged += new System.EventHandler(this.Main_BackColorChanged);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.cmsFile.ResumeLayout(false);
            this.scFiles.Panel1.ResumeLayout(false);
            this.scFiles.Panel1.PerformLayout();
            this.scFiles.Panel2.ResumeLayout(false);
            this.scFiles.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scFiles)).EndInit();
            this.scFiles.ResumeLayout(false);
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFont)).EndInit();
            this.gbOrderBy.ResumeLayout(false);
            this.gbOrderBy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMaster;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.SplitContainer scFiles;
        private System.Windows.Forms.TabControl tcFiles;
        private System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cbFileType;
        private System.Windows.Forms.Button btnMissingFiles;
        private System.Windows.Forms.Button btnSaveProd;
        private System.Windows.Forms.Button btnSaveMaster;
        private System.Windows.Forms.RichTextBox MasterLineNumberTextBox;
        private System.Windows.Forms.RichTextBox ProdLineNumberTextBox;
        private System.Windows.Forms.RichTextBox rbMaster;
        private System.Windows.Forms.Label lblMasterMod;
        private System.Windows.Forms.Label lblMasterSize;
        private System.Windows.Forms.Label lblProdSize;
        private System.Windows.Forms.Label lblProdMod;
        private System.Windows.Forms.Button btnMasterHistory;
        private System.Windows.Forms.Button btnProdHistory;
        private System.Windows.Forms.Button btnMasterNotepad;
        private System.Windows.Forms.Button btnProdNotepad;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.Timer textChangedTimer;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.LinkLabel lblSharepoint;
        private System.Windows.Forms.LinkLabel lblSSKB;
        private System.Windows.Forms.TrackBar tbFont;
        private System.Windows.Forms.GroupBox gbOrderBy;
        private System.Windows.Forms.RadioButton rbModified;
        private System.Windows.Forms.RadioButton rbName;
        private System.Windows.Forms.ContextMenuStrip cmsFile;
        private System.Windows.Forms.ToolStripMenuItem miDeploy;
        private System.Windows.Forms.ToolStripMenuItem miBackup;
        private System.Windows.Forms.Button btnMasterInfo;
        private System.Windows.Forms.Button btnProdInfo;
        private System.Windows.Forms.LinkLabel lblIress;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

