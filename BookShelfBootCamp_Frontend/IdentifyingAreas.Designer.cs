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
            this.GeneratedListView = new System.Windows.Forms.ListView();
            this.btnGenerateCallNumbers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GeneratedListView
            // 
            this.GeneratedListView.HideSelection = false;
            this.GeneratedListView.Location = new System.Drawing.Point(3, 3);
            this.GeneratedListView.Name = "GeneratedListView";
            this.GeneratedListView.Size = new System.Drawing.Size(300, 400);
            this.GeneratedListView.UseCompatibleStateImageBehavior = false;
            this.GeneratedListView.Columns.Add("Call Number", -2, HorizontalAlignment.Left);
            this.GeneratedListView.Columns.Add("Description", -2, HorizontalAlignment.Left);
            this.GeneratedListView.View = View.Details;
            this.GeneratedListView.AllowDrop = true;  // Enabling drag and drop
            this.GeneratedListView.ItemDrag += new ItemDragEventHandler(GeneratedListView_ItemDrag);
            this.GeneratedListView.DragDrop += new DragEventHandler(GeneratedListView_DragDrop);
            this.GeneratedListView.DragEnter += new DragEventHandler(GeneratedListView_DragEnter);

            this.Controls.Add(this.GeneratedListView);
            this.ResumeLayout(false);
            // 
            // btnGenerateCallNumbers
            // 
            this.btnGenerateCallNumbers.Location = new System.Drawing.Point(3, 406);
            this.btnGenerateCallNumbers.Name = "btnGenerateCallNumbers";
            this.btnGenerateCallNumbers.Size = new System.Drawing.Size(151, 23);
            this.btnGenerateCallNumbers.TabIndex = 1;
            this.btnGenerateCallNumbers.Text = "Generate Call Numbers";
            this.btnGenerateCallNumbers.UseVisualStyleBackColor = true;
            this.btnGenerateCallNumbers.Click += new System.EventHandler(this.btnGenerateCallNumbers_Click);
            // 
            // IdentifyingAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGenerateCallNumbers);
            this.Controls.Add(this.GeneratedListView);
            this.Name = "IdentifyingAreas";
            this.Size = new System.Drawing.Size(359, 448);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnGenerateCallNumbers;
    }
}
