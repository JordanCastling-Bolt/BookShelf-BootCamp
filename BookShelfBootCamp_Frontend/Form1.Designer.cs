namespace PROG7132
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnCheckOrder = new System.Windows.Forms.Button();
            this.btnGenerateCallNumbers = new System.Windows.Forms.Button();
            this.replaceBooksBar = new System.Windows.Forms.ProgressBar();
            this.menuReplacingBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIdentifyingAreas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindingCallNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.lblProgressInfo = new System.Windows.Forms.Label();
            this.GeneratedListView = new System.Windows.Forms.ListView();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCheckOrder
            // 
            this.btnCheckOrder.AutoSize = true;
            this.btnCheckOrder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCheckOrder.Location = new System.Drawing.Point(319, 682);
            this.btnCheckOrder.Name = "btnCheckOrder";
            this.btnCheckOrder.Size = new System.Drawing.Size(77, 23);
            this.btnCheckOrder.TabIndex = 1;
            this.btnCheckOrder.Text = "Check Order";
            this.btnCheckOrder.UseVisualStyleBackColor = true;
            this.btnCheckOrder.Click += new System.EventHandler(this.btnCheckOrder_Click);
            // 
            // btnGenerateCallNumbers
            // 
            this.btnGenerateCallNumbers.AutoSize = true;
            this.btnGenerateCallNumbers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGenerateCallNumbers.Location = new System.Drawing.Point(140, 682);
            this.btnGenerateCallNumbers.Name = "btnGenerateCallNumbers";
            this.btnGenerateCallNumbers.Size = new System.Drawing.Size(126, 23);
            this.btnGenerateCallNumbers.TabIndex = 4;
            this.btnGenerateCallNumbers.Text = "Generate Call Numbers";
            this.btnGenerateCallNumbers.UseVisualStyleBackColor = true;
            this.btnGenerateCallNumbers.Click += new System.EventHandler(this.btnGenerateCallNumbers_Click);
            // 
            // replaceBooksBar
            // 
            this.replaceBooksBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.replaceBooksBar.Location = new System.Drawing.Point(140, 724);
            this.replaceBooksBar.Maximum = 10;
            this.replaceBooksBar.Name = "replaceBooksBar";
            this.replaceBooksBar.Size = new System.Drawing.Size(515, 23);
            this.replaceBooksBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.replaceBooksBar.TabIndex = 5;
            // 
            // menuReplacingBooks
            // 
            this.menuReplacingBooks.Name = "menuReplacingBooks";
            this.menuReplacingBooks.Size = new System.Drawing.Size(127, 19);
            this.menuReplacingBooks.Text = "Replacing Books";
            // 
            // menuIdentifyingAreas
            // 
            this.menuIdentifyingAreas.Enabled = false;
            this.menuIdentifyingAreas.Name = "menuIdentifyingAreas";
            this.menuIdentifyingAreas.Size = new System.Drawing.Size(127, 19);
            this.menuIdentifyingAreas.Text = "Identifying Areas";
            // 
            // menuFindingCallNumbers
            // 
            this.menuFindingCallNumbers.Enabled = false;
            this.menuFindingCallNumbers.Name = "menuFindingCallNumbers";
            this.menuFindingCallNumbers.Size = new System.Drawing.Size(127, 19);
            this.menuFindingCallNumbers.Text = "Finding Call Numbers";
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReplacingBooks,
            this.menuIdentifyingAreas,
            this.menuFindingCallNumbers});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip.Size = new System.Drawing.Size(140, 747);
            this.menuStrip.TabIndex = 0;
            // 
            // lblProgressInfo
            // 
            this.lblProgressInfo.AutoSize = true;
            this.lblProgressInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressInfo.Location = new System.Drawing.Point(143, 708);
            this.lblProgressInfo.Name = "lblProgressInfo";
            this.lblProgressInfo.Size = new System.Drawing.Size(0, 13);
            this.lblProgressInfo.TabIndex = 7;
            // 
            // GeneratedListView
            // 
            this.GeneratedListView.HideSelection = false;
            this.GeneratedListView.Location = new System.Drawing.Point(224, 0);
            this.GeneratedListView.Name = "GeneratedListView";
            this.GeneratedListView.Size = new System.Drawing.Size(141, 676);
            this.GeneratedListView.TabIndex = 8;
            this.GeneratedListView.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(655, 747);
            this.Controls.Add(this.GeneratedListView);
            this.Controls.Add(this.lblProgressInfo);
            this.Controls.Add(this.replaceBooksBar);
            this.Controls.Add(this.btnGenerateCallNumbers);
            this.Controls.Add(this.btnCheckOrder);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bookshelf Bootcamp";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCheckOrder;
        private System.Windows.Forms.Button btnGenerateCallNumbers;
        private System.Windows.Forms.ProgressBar replaceBooksBar;
        private System.Windows.Forms.ToolStripMenuItem menuReplacingBooks;
        private System.Windows.Forms.ToolStripMenuItem menuIdentifyingAreas;
        private System.Windows.Forms.ToolStripMenuItem menuFindingCallNumbers;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Label lblProgressInfo;
        private System.Windows.Forms.ListView GeneratedListView;
    }
}

