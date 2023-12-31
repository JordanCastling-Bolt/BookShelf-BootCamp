﻿using System.Windows.Forms;

namespace PROG7132
{
    partial class IdentifyingAreas
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
            this.components = new System.ComponentModel.Container();
            this.CallNumbersListView = new System.Windows.Forms.ListView();
            this.DescriptionsListView = new System.Windows.Forms.ListView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.badgePictureBox = new System.Windows.Forms.PictureBox();
            this.btnShuffleMatches = new System.Windows.Forms.Button();
            this.btnShuffleClues = new System.Windows.Forms.Button();
            this.btnShuffleBoth = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHint = new System.Windows.Forms.Button();
            this.colorResetTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.badgePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CallNumbersListView
            // 
            this.CallNumbersListView.AllowDrop = true;
            this.CallNumbersListView.HideSelection = false;
            this.CallNumbersListView.Location = new System.Drawing.Point(3, 121);
            this.CallNumbersListView.Name = "CallNumbersListView";
            this.CallNumbersListView.Size = new System.Drawing.Size(535, 253);
            this.CallNumbersListView.TabIndex = 3;
            this.CallNumbersListView.UseCompatibleStateImageBehavior = false;
            this.CallNumbersListView.View = System.Windows.Forms.View.List;
            // 
            // DescriptionsListView
            // 
            this.DescriptionsListView.AllowDrop = true;
            this.DescriptionsListView.HideSelection = false;
            this.DescriptionsListView.Location = new System.Drawing.Point(544, 121);
            this.DescriptionsListView.Name = "DescriptionsListView";
            this.DescriptionsListView.Size = new System.Drawing.Size(533, 253);
            this.DescriptionsListView.TabIndex = 4;
            this.DescriptionsListView.UseCompatibleStateImageBehavior = false;
            this.DescriptionsListView.View = System.Windows.Forms.View.List;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 800);
            this.progressBar1.Maximum = 4;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1080, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 5;
            // 
            // badgePictureBox
            // 
            this.badgePictureBox.Location = new System.Drawing.Point(416, 380);
            this.badgePictureBox.Name = "badgePictureBox";
            this.badgePictureBox.Padding = new System.Windows.Forms.Padding(5);
            this.badgePictureBox.Size = new System.Drawing.Size(250, 228);
            this.badgePictureBox.TabIndex = 6;
            this.badgePictureBox.TabStop = false;
            // 
            // btnShuffleMatches
            // 
            this.btnShuffleMatches.Location = new System.Drawing.Point(4, 381);
            this.btnShuffleMatches.Name = "btnShuffleMatches";
            this.btnShuffleMatches.Padding = new System.Windows.Forms.Padding(5);
            this.btnShuffleMatches.Size = new System.Drawing.Size(114, 30);
            this.btnShuffleMatches.TabIndex = 7;
            this.btnShuffleMatches.Text = "Shuffle Clues";
            this.btnShuffleMatches.UseVisualStyleBackColor = true;
            this.btnShuffleMatches.Click += new System.EventHandler(this.btnShuffleMatches_Click);
            // 
            // btnShuffleClues
            // 
            this.btnShuffleClues.Location = new System.Drawing.Point(4, 417);
            this.btnShuffleClues.Name = "btnShuffleClues";
            this.btnShuffleClues.Padding = new System.Windows.Forms.Padding(5);
            this.btnShuffleClues.Size = new System.Drawing.Size(114, 29);
            this.btnShuffleClues.TabIndex = 8;
            this.btnShuffleClues.Text = "Shuffle Matches";
            this.btnShuffleClues.UseVisualStyleBackColor = true;
            this.btnShuffleClues.Click += new System.EventHandler(this.btnShuffleClues_Click);
            // 
            // btnShuffleBoth
            // 
            this.btnShuffleBoth.Location = new System.Drawing.Point(4, 452);
            this.btnShuffleBoth.Name = "btnShuffleBoth";
            this.btnShuffleBoth.Padding = new System.Windows.Forms.Padding(5);
            this.btnShuffleBoth.Size = new System.Drawing.Size(114, 29);
            this.btnShuffleBoth.TabIndex = 9;
            this.btnShuffleBoth.Text = "Shuffle Both";
            this.btnShuffleBoth.UseVisualStyleBackColor = true;
            this.btnShuffleBoth.Click += new System.EventHandler(this.btnShuffleBoth_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(358, 47);
            this.label1.TabIndex = 10;
            this.label1.Text = "Matching the Columns";
            // 
            // btnHint
            // 
            this.btnHint.Location = new System.Drawing.Point(370, 47);
            this.btnHint.Name = "btnHint";
            this.btnHint.Padding = new System.Windows.Forms.Padding(5);
            this.btnHint.Size = new System.Drawing.Size(86, 36);
            this.btnHint.TabIndex = 11;
            this.btnHint.Text = "How to Play";
            this.btnHint.UseVisualStyleBackColor = true;
            this.btnHint.Click += new System.EventHandler(this.btnHint_Click);
            // 
            // colorResetTimer
            // 
            this.colorResetTimer.Interval = 1500;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 90);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(52, 28);
            this.label2.TabIndex = 12;
            this.label2.Text = "Clues";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(541, 90);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5);
            this.label3.Size = new System.Drawing.Size(71, 28);
            this.label3.TabIndex = 13;
            this.label3.Text = "Matches";
            // 
            // IdentifyingAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShuffleBoth);
            this.Controls.Add(this.btnShuffleClues);
            this.Controls.Add(this.btnShuffleMatches);
            this.Controls.Add(this.badgePictureBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.DescriptionsListView);
            this.Controls.Add(this.CallNumbersListView);
            this.Name = "IdentifyingAreas";
            this.Size = new System.Drawing.Size(1080, 823);
            ((System.ComponentModel.ISupportInitialize)(this.badgePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView CallNumbersListView;
        private ListView DescriptionsListView;
        private ProgressBar progressBar1;
        private PictureBox badgePictureBox;
        private Button btnShuffleMatches;
        private Button btnShuffleClues;
        private Button btnShuffleBoth;
        private Label label1;
        private Button btnHint;
        private Timer colorResetTimer;
        private Label label2;
        private Label label3;
    }
}
