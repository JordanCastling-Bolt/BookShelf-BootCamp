namespace PROG7132
{
    partial class ReplacingBooks
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cboMozartCompositions;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboMozartCompositions = new System.Windows.Forms.ComboBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.songProgressBar = new System.Windows.Forms.ProgressBar();
            this.achievementBadge = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.achievementBadge)).BeginInit();
            this.SuspendLayout();
            // 
            // GeneratedListView
            // 
            this.GeneratedListView.HideSelection = false;
            this.GeneratedListView.Location = new System.Drawing.Point(281, 4);
            this.GeneratedListView.Name = "GeneratedListView";
            this.GeneratedListView.Size = new System.Drawing.Size(142, 689);
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
            this.replaceBooksBar.Location = new System.Drawing.Point(0, 719);
            this.replaceBooksBar.Maximum = 10;
            this.replaceBooksBar.Name = "replaceBooksBar";
            this.replaceBooksBar.Size = new System.Drawing.Size(862, 23);
            this.replaceBooksBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.replaceBooksBar.TabIndex = 12;
            // 
            // btnGenerateCallNumbers
            // 
            this.btnGenerateCallNumbers.AutoSize = true;
            this.btnGenerateCallNumbers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGenerateCallNumbers.Location = new System.Drawing.Point(0, 670);
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
            this.btnCheckOrder.Location = new System.Drawing.Point(132, 670);
            this.btnCheckOrder.Name = "btnCheckOrder";
            this.btnCheckOrder.Size = new System.Drawing.Size(77, 23);
            this.btnCheckOrder.TabIndex = 10;
            this.btnCheckOrder.Text = "Check Order";
            this.btnCheckOrder.UseVisualStyleBackColor = true;
            this.btnCheckOrder.Click += new System.EventHandler(this.btnCheckOrder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(272, 47);
            this.label1.TabIndex = 16;
            this.label1.Text = "Replacing Books";
            // 
            // cboMozartCompositions
            // 
            this.cboMozartCompositions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMozartCompositions.FormattingEnabled = true;
            this.cboMozartCompositions.Location = new System.Drawing.Point(636, 42);
            this.cboMozartCompositions.Name = "cboMozartCompositions";
            this.cboMozartCompositions.Size = new System.Drawing.Size(200, 21);
            this.cboMozartCompositions.TabIndex = 17;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(636, 108);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Padding = new System.Windows.Forms.Padding(5);
            this.btnPlay.Size = new System.Drawing.Size(50, 31);
            this.btnPlay.TabIndex = 18;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(692, 108);
            this.btnPause.Name = "btnPause";
            this.btnPause.Padding = new System.Windows.Forms.Padding(5);
            this.btnPause.Size = new System.Drawing.Size(68, 31);
            this.btnPause.TabIndex = 19;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(766, 108);
            this.btnStop.Name = "btnStop";
            this.btnStop.Padding = new System.Windows.Forms.Padding(5);
            this.btnStop.Size = new System.Drawing.Size(50, 31);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(631, 4);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(232, 35);
            this.label2.TabIndex = 21;
            this.label2.Text = "Mozart Media Player";
            // 
            // songProgressBar
            // 
            this.songProgressBar.Location = new System.Drawing.Point(636, 79);
            this.songProgressBar.Name = "songProgressBar";
            this.songProgressBar.Size = new System.Drawing.Size(200, 23);
            this.songProgressBar.TabIndex = 22;
            // 
            // achievementBadge
            // 
            this.achievementBadge.Location = new System.Drawing.Point(429, 4);
            this.achievementBadge.Name = "achievementBadge";
            this.achievementBadge.Padding = new System.Windows.Forms.Padding(5);
            this.achievementBadge.Size = new System.Drawing.Size(196, 154);
            this.achievementBadge.TabIndex = 15;
            this.achievementBadge.TabStop = false;
            this.achievementBadge.Visible = false;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(3, 54);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(5);
            this.button1.Size = new System.Drawing.Size(86, 33);
            this.button1.TabIndex = 23;
            this.button1.Text = "How to Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.howToPlay_Click);
            // 
            // ReplacingBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.songProgressBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.achievementBadge);
            this.Controls.Add(this.GeneratedListView);
            this.Controls.Add(this.lblProgressInfo);
            this.Controls.Add(this.replaceBooksBar);
            this.Controls.Add(this.btnGenerateCallNumbers);
            this.Controls.Add(this.btnCheckOrder);
            this.Controls.Add(this.cboMozartCompositions);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Name = "ReplacingBooks";
            this.Size = new System.Drawing.Size(862, 742);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar songProgressBar;
        private System.Windows.Forms.Button button1;
    }
}
