namespace SMAReportCleaner
{
    partial class MissingFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MissingFiles));
            this.flpMissing = new System.Windows.Forms.FlowLayoutPanel();
            this.gbMaster = new System.Windows.Forms.GroupBox();
            this.lbMaster = new System.Windows.Forms.ListBox();
            this.gbMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMissing
            // 
            this.flpMissing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpMissing.AutoScroll = true;
            this.flpMissing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMissing.Location = new System.Drawing.Point(226, 9);
            this.flpMissing.Name = "flpMissing";
            this.flpMissing.Size = new System.Drawing.Size(881, 564);
            this.flpMissing.TabIndex = 27;
            this.flpMissing.WrapContents = false;
            // 
            // gbMaster
            // 
            this.gbMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbMaster.Controls.Add(this.lbMaster);
            this.gbMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMaster.Location = new System.Drawing.Point(12, 12);
            this.gbMaster.Name = "gbMaster";
            this.gbMaster.Size = new System.Drawing.Size(208, 551);
            this.gbMaster.TabIndex = 28;
            this.gbMaster.TabStop = false;
            this.gbMaster.Text = "Master";
            // 
            // lbMaster
            // 
            this.lbMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMaster.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbMaster.FormattingEnabled = true;
            this.lbMaster.ItemHeight = 16;
            this.lbMaster.Location = new System.Drawing.Point(3, 19);
            this.lbMaster.Name = "lbMaster";
            this.lbMaster.Size = new System.Drawing.Size(199, 516);
            this.lbMaster.Sorted = true;
            this.lbMaster.TabIndex = 0;
            this.lbMaster.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbMaster_DrawItem);
            this.lbMaster.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbMaster_MeasureItem);
            this.lbMaster.SelectedIndexChanged += new System.EventHandler(this.lbMaster_SelectedIndexChanged);
            // 
            // MissingFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 575);
            this.Controls.Add(this.gbMaster);
            this.Controls.Add(this.flpMissing);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MissingFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Missing Files";
            this.Shown += new System.EventHandler(this.MissingFiles_Shown);
            this.SizeChanged += new System.EventHandler(this.MissingFiles_SizeChanged);
            this.gbMaster.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpMissing;
        private System.Windows.Forms.GroupBox gbMaster;
        private System.Windows.Forms.ListBox lbMaster;
    }
}