namespace PROG7132
{
    partial class Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.menuReplacingBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIdentifyingAreas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindingCallNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuReplacingBooks
            // 
            this.menuReplacingBooks.Name = "menuReplacingBooks";
            this.menuReplacingBooks.Size = new System.Drawing.Size(127, 19);
            this.menuReplacingBooks.Text = "Replacing Books";
            this.menuReplacingBooks.Click += new System.EventHandler(this.menuReplacingBooks_Click);
            // 
            // menuIdentifyingAreas
            // 
            this.menuIdentifyingAreas.Name = "menuIdentifyingAreas";
            this.menuIdentifyingAreas.Size = new System.Drawing.Size(127, 19);
            this.menuIdentifyingAreas.Text = "Identifying Areas";
            this.menuIdentifyingAreas.Click += new System.EventHandler(this.menuIdentifyingAreas_Click);
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
            this.menuStrip.Size = new System.Drawing.Size(140, 819);
            this.menuStrip.TabIndex = 0;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1218, 819);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bookshelf Bootcamp";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem menuReplacingBooks;
        private System.Windows.Forms.ToolStripMenuItem menuIdentifyingAreas;
        private System.Windows.Forms.ToolStripMenuItem menuFindingCallNumbers;
        private System.Windows.Forms.MenuStrip menuStrip;
    }
}

