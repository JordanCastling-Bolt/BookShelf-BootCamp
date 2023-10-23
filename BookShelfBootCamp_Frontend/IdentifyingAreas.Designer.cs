using System.Windows.Forms;

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
            this.CallNumbersListView = new System.Windows.Forms.ListView();
            this.DescriptionsListView = new System.Windows.Forms.ListView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
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
            this.progressBar1.Location = new System.Drawing.Point(4, 774);
            this.progressBar1.Maximum = 4;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1073, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 5;
            // 
            // IdentifyingAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.DescriptionsListView);
            this.Controls.Add(this.CallNumbersListView);
            this.Name = "IdentifyingAreas";
            this.Size = new System.Drawing.Size(1080, 798);
            this.ResumeLayout(false);

        }

        #endregion

        private ListView CallNumbersListView;
        private ListView DescriptionsListView;
        private ProgressBar progressBar1;
    }
}
