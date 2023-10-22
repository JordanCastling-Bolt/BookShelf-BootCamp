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
            this.btnGenerateCallNumbers = new System.Windows.Forms.Button();
            this.CallNumbersListView = new System.Windows.Forms.ListView();
            this.DescriptionsListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnGenerateCallNumbers
            // 
            this.btnGenerateCallNumbers.Location = new System.Drawing.Point(188, 403);
            this.btnGenerateCallNumbers.Name = "btnGenerateCallNumbers";
            this.btnGenerateCallNumbers.Size = new System.Drawing.Size(151, 23);
            this.btnGenerateCallNumbers.TabIndex = 1;
            this.btnGenerateCallNumbers.Text = "Generate Call Numbers";
            this.btnGenerateCallNumbers.UseVisualStyleBackColor = true;
            this.btnGenerateCallNumbers.Click += new System.EventHandler(this.btnGenerateCallNumbers_Click);
            // 
            // CallNumbersListView
            // 
            this.CallNumbersListView.AllowDrop = true;
            this.CallNumbersListView.HideSelection = false;
            this.CallNumbersListView.Location = new System.Drawing.Point(3, 121);
            this.CallNumbersListView.Name = "CallNumbersListView";
            this.CallNumbersListView.Size = new System.Drawing.Size(265, 253);
            this.CallNumbersListView.TabIndex = 3;
            this.CallNumbersListView.UseCompatibleStateImageBehavior = false;
            this.CallNumbersListView.View = System.Windows.Forms.View.List;
            // 
            // DescriptionsListView
            // 
            this.DescriptionsListView.AllowDrop = true;
            this.DescriptionsListView.HideSelection = false;
            this.DescriptionsListView.Location = new System.Drawing.Point(274, 121);
            this.DescriptionsListView.Name = "DescriptionsListView";
            this.DescriptionsListView.Size = new System.Drawing.Size(280, 253);
            this.DescriptionsListView.TabIndex = 4;
            this.DescriptionsListView.UseCompatibleStateImageBehavior = false;
            this.DescriptionsListView.View = System.Windows.Forms.View.List;
            // 
            // IdentifyingAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DescriptionsListView);
            this.Controls.Add(this.CallNumbersListView);
            this.Controls.Add(this.btnGenerateCallNumbers);
            this.Name = "IdentifyingAreas";
            this.Size = new System.Drawing.Size(763, 798);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnGenerateCallNumbers;
        private ListView CallNumbersListView;
        private ListView DescriptionsListView;
    }
}
