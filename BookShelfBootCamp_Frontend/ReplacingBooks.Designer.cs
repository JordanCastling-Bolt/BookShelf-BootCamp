namespace PROG7132
{
    partial class ReplacingBooks
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GeneratedListView = new System.Windows.Forms.ListView();
            this.lblProgressInfo = new System.Windows.Forms.Label();
            this.replaceBooksBar = new System.Windows.Forms.ProgressBar();
            this.btnGenerateCallNumbers = new System.Windows.Forms.Button();
            this.btnCheckOrder = new System.Windows.Forms.Button();
            this.achievementBadge = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.achievementBadge)).BeginInit();
            this.SuspendLayout();
            // 
            // GeneratedListView
            // 
            this.GeneratedListView.HideSelection = false;
            this.GeneratedListView.Location = new System.Drawing.Point(339, 3);
            this.GeneratedListView.Name = "GeneratedListView";
            this.GeneratedListView.Size = new System.Drawing.Size(142, 676);
            this.GeneratedListView.TabIndex = 14;
            this.GeneratedListView.UseCompatibleStateImageBehavior = false;
            // 
            // lblProgressInfo
            // 
            this.lblProgressInfo.AutoSize = true;
            this.lblProgressInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressInfo.Location = new System.Drawing.Point(3, 769);
            this.lblProgressInfo.Name = "lblProgressInfo";
            this.lblProgressInfo.Size = new System.Drawing.Size(0, 13);
            this.lblProgressInfo.TabIndex = 13;
            // 
            // replaceBooksBar
            // 
            this.replaceBooksBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.replaceBooksBar.Location = new System.Drawing.Point(0, 775);
            this.replaceBooksBar.Maximum = 10;
            this.replaceBooksBar.Name = "replaceBooksBar";
            this.replaceBooksBar.Size = new System.Drawing.Size(1080, 23);
            this.replaceBooksBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.replaceBooksBar.TabIndex = 12;
            // 
            // btnGenerateCallNumbers
            // 
            this.btnGenerateCallNumbers.AutoSize = true;
            this.btnGenerateCallNumbers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGenerateCallNumbers.Location = new System.Drawing.Point(3, 694);
            this.btnGenerateCallNumbers.Name = "btnGenerateCallNumbers";
            this.btnGenerateCallNumbers.Size = new System.Drawing.Size(126, 23);
            this.btnGenerateCallNumbers.TabIndex = 11;
            this.btnGenerateCallNumbers.Text = "Generate Call Numbers";
            this.btnGenerateCallNumbers.UseVisualStyleBackColor = true;
            this.btnGenerateCallNumbers.Click += new System.EventHandler(this.btnGenerateCallNumbers_Click);
            // 
            // btnCheckOrder
            // 
            this.btnCheckOrder.AutoSize = true;
            this.btnCheckOrder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCheckOrder.Location = new System.Drawing.Point(135, 694);
            this.btnCheckOrder.Name = "btnCheckOrder";
            this.btnCheckOrder.Size = new System.Drawing.Size(77, 23);
            this.btnCheckOrder.TabIndex = 10;
            this.btnCheckOrder.Text = "Check Order";
            this.btnCheckOrder.UseVisualStyleBackColor = true;
            this.btnCheckOrder.Click += new System.EventHandler(this.btnCheckOrder_Click);
            // 
            // achievementBadge
            // 
            this.achievementBadge.Location = new System.Drawing.Point(487, 3);
            this.achievementBadge.Name = "achievementBadge";
            this.achievementBadge.Size = new System.Drawing.Size(196, 154);
            this.achievementBadge.TabIndex = 15;
            this.achievementBadge.TabStop = false;
            this.achievementBadge.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 37);
            this.label1.TabIndex = 16;
            this.label1.Text = "Replacing Books";
            // 
            // ReplacingBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.achievementBadge);
            this.Controls.Add(this.GeneratedListView);
            this.Controls.Add(this.lblProgressInfo);
            this.Controls.Add(this.replaceBooksBar);
            this.Controls.Add(this.btnGenerateCallNumbers);
            this.Controls.Add(this.btnCheckOrder);
            this.Name = "ReplacingBooks";
            this.Size = new System.Drawing.Size(1080, 798);
            ((System.ComponentModel.ISupportInitialize)(this.achievementBadge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox achievementBadge;
        private System.Windows.Forms.ListView GeneratedListView;
        private System.Windows.Forms.Label lblProgressInfo;
        private System.Windows.Forms.ProgressBar replaceBooksBar;
        private System.Windows.Forms.Button btnGenerateCallNumbers;
        private System.Windows.Forms.Button btnCheckOrder;
        private System.Windows.Forms.Label label1;
    }
}
