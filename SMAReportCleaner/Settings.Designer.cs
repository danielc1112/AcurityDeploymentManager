namespace SMAReportCleaner
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.btnSave = new System.Windows.Forms.Button();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tpReports = new System.Windows.Forms.TabPage();
            this.flpReports = new System.Windows.Forms.FlowLayoutPanel();
            this.tpCustomisations = new System.Windows.Forms.TabPage();
            this.flpCustomisations = new System.Windows.Forms.FlowLayoutPanel();
            this.tpTemplates = new System.Windows.Forms.TabPage();
            this.flpTemplates = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSubtract = new System.Windows.Forms.Button();
            this.tpXMLUI = new System.Windows.Forms.TabPage();
            this.flpXMLUI = new System.Windows.Forms.FlowLayoutPanel();
            this.tcSettings.SuspendLayout();
            this.tpReports.SuspendLayout();
            this.tpCustomisations.SuspendLayout();
            this.tpTemplates.SuspendLayout();
            this.tpXMLUI.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(490, 569);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 32);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Save All";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tcSettings
            // 
            this.tcSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSettings.Controls.Add(this.tpReports);
            this.tcSettings.Controls.Add(this.tpCustomisations);
            this.tcSettings.Controls.Add(this.tpTemplates);
            this.tcSettings.Controls.Add(this.tpXMLUI);
            this.tcSettings.Location = new System.Drawing.Point(16, 42);
            this.tcSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(1089, 519);
            this.tcSettings.TabIndex = 97;
            // 
            // tpReports
            // 
            this.tpReports.Controls.Add(this.flpReports);
            this.tpReports.Location = new System.Drawing.Point(4, 25);
            this.tpReports.Margin = new System.Windows.Forms.Padding(4);
            this.tpReports.Name = "tpReports";
            this.tpReports.Padding = new System.Windows.Forms.Padding(4);
            this.tpReports.Size = new System.Drawing.Size(1081, 490);
            this.tpReports.TabIndex = 0;
            this.tpReports.Text = "Reports";
            this.tpReports.UseVisualStyleBackColor = true;
            // 
            // flpReports
            // 
            this.flpReports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpReports.AutoScroll = true;
            this.flpReports.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpReports.Location = new System.Drawing.Point(4, 4);
            this.flpReports.Margin = new System.Windows.Forms.Padding(4);
            this.flpReports.Name = "flpReports";
            this.flpReports.Size = new System.Drawing.Size(1071, 473);
            this.flpReports.TabIndex = 0;
            this.flpReports.WrapContents = false;
            // 
            // tpCustomisations
            // 
            this.tpCustomisations.Controls.Add(this.flpCustomisations);
            this.tpCustomisations.Location = new System.Drawing.Point(4, 25);
            this.tpCustomisations.Margin = new System.Windows.Forms.Padding(4);
            this.tpCustomisations.Name = "tpCustomisations";
            this.tpCustomisations.Padding = new System.Windows.Forms.Padding(4);
            this.tpCustomisations.Size = new System.Drawing.Size(1081, 490);
            this.tpCustomisations.TabIndex = 1;
            this.tpCustomisations.Text = "Customisations";
            this.tpCustomisations.UseVisualStyleBackColor = true;
            // 
            // flpCustomisations
            // 
            this.flpCustomisations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCustomisations.AutoScroll = true;
            this.flpCustomisations.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCustomisations.Location = new System.Drawing.Point(4, 4);
            this.flpCustomisations.Margin = new System.Windows.Forms.Padding(4);
            this.flpCustomisations.Name = "flpCustomisations";
            this.flpCustomisations.Size = new System.Drawing.Size(1071, 473);
            this.flpCustomisations.TabIndex = 1;
            this.flpCustomisations.WrapContents = false;
            // 
            // tpTemplates
            // 
            this.tpTemplates.Controls.Add(this.flpTemplates);
            this.tpTemplates.Location = new System.Drawing.Point(4, 25);
            this.tpTemplates.Margin = new System.Windows.Forms.Padding(4);
            this.tpTemplates.Name = "tpTemplates";
            this.tpTemplates.Size = new System.Drawing.Size(1081, 490);
            this.tpTemplates.TabIndex = 2;
            this.tpTemplates.Text = "Templates";
            this.tpTemplates.UseVisualStyleBackColor = true;
            // 
            // flpTemplates
            // 
            this.flpTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpTemplates.AutoScroll = true;
            this.flpTemplates.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTemplates.Location = new System.Drawing.Point(4, 4);
            this.flpTemplates.Margin = new System.Windows.Forms.Padding(4);
            this.flpTemplates.Name = "flpTemplates";
            this.flpTemplates.Size = new System.Drawing.Size(1071, 473);
            this.flpTemplates.TabIndex = 1;
            this.flpTemplates.WrapContents = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(436, 15);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 28);
            this.btnAdd.TabIndex = 98;
            this.btnAdd.Text = "Add Setting";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSubtract
            // 
            this.btnSubtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubtract.Location = new System.Drawing.Point(564, 15);
            this.btnSubtract.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubtract.Name = "btnSubtract";
            this.btnSubtract.Size = new System.Drawing.Size(121, 28);
            this.btnSubtract.TabIndex = 99;
            this.btnSubtract.Text = "Remove Setting";
            this.btnSubtract.UseVisualStyleBackColor = true;
            this.btnSubtract.Click += new System.EventHandler(this.btnSubtract_Click);
            // 
            // tpXMLUI
            // 
            this.tpXMLUI.Controls.Add(this.flpXMLUI);
            this.tpXMLUI.Location = new System.Drawing.Point(4, 25);
            this.tpXMLUI.Name = "tpXMLUI";
            this.tpXMLUI.Size = new System.Drawing.Size(1081, 490);
            this.tpXMLUI.TabIndex = 3;
            this.tpXMLUI.Text = "XMLUI";
            this.tpXMLUI.UseVisualStyleBackColor = true;
            // 
            // flpXMLUI
            // 
            this.flpXMLUI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpXMLUI.AutoScroll = true;
            this.flpXMLUI.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpXMLUI.Location = new System.Drawing.Point(4, 4);
            this.flpXMLUI.Margin = new System.Windows.Forms.Padding(4);
            this.flpXMLUI.Name = "flpXMLUI";
            this.flpXMLUI.Size = new System.Drawing.Size(1071, 482);
            this.flpXMLUI.TabIndex = 2;
            this.flpXMLUI.WrapContents = false;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 614);
            this.Controls.Add(this.btnSubtract);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tcSettings);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tcSettings.ResumeLayout(false);
            this.tpReports.ResumeLayout(false);
            this.tpCustomisations.ResumeLayout(false);
            this.tpTemplates.ResumeLayout(false);
            this.tpXMLUI.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.TabPage tpReports;
        private System.Windows.Forms.TabPage tpCustomisations;
        private System.Windows.Forms.TabPage tpTemplates;
        private System.Windows.Forms.FlowLayoutPanel flpReports;
        private System.Windows.Forms.FlowLayoutPanel flpCustomisations;
        private System.Windows.Forms.FlowLayoutPanel flpTemplates;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSubtract;
        private System.Windows.Forms.TabPage tpXMLUI;
        private System.Windows.Forms.FlowLayoutPanel flpXMLUI;
    }
}