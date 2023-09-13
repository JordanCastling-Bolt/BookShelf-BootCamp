using Library_Classlib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PROG7132
{
    /// <summary>
    /// Main form to handle the interaction with call number manager and GUI.
    /// </summary>
    public partial class Form1 : Form
    {
        readonly CallNumberManager callNumberManager = new CallNumberManager();
        private int currentProgress = 0;
        private int currentCorrectOrders = 0; 


        public Form1()
        {
            InitializeComponent();
            // Setting up drag and drop functionality and registering progress update event
            Generated_List.AllowDrop = true;
            Generated_List.MouseDown += listBox1_MouseDown;
            Generated_List.DragOver += listBox1_DragOver;
            Generated_List.DragDrop += listBox1_DragDrop;
            callNumberManager.OnProgressUpdate += UpdateProgressLabel;
            this.Controls.Add(this.lblProgressInfo);
        }
        /// <summary>
        /// Updates the Progress label and Progress bar
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="remainingOrders"></param>
        /// <param name="message"></param>
        private void UpdateProgressLabel(int progress, int remainingOrders, string message)
        {
            lblProgressInfo.Text = $"{message} {progress} out of 10 correct. {remainingOrders} remaining.";
            replaceBooksBar.Value = progress;
        }
        /// <summary>
        /// Handles mouse and drag events for drag-and-drop functionality in the list box.
        /// </summary>
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Generated_List.SelectedItem == null) return;
            Generated_List.DoDragDrop(Generated_List.SelectedItem, DragDropEffects.Move);
        }
        /// <summary>
        /// Handles mouse and drag events for drag-and-drop functionality in the list box.
        /// </summary>
        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        /// <summary>
        /// Handles mouse and drag events for drag-and-drop functionality in the list box.
        /// </summary>
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            Point point = Generated_List.PointToClient(new Point(e.X, e.Y));
            int index = Generated_List.IndexFromPoint(point);
            if (index < 0) index = Generated_List.Items.Count - 1;
            object data = e.Data.GetData(typeof(string));
            Generated_List.Items.Remove(data);
            Generated_List.Items.Insert(index, data);
        }

        /// <summary>
        /// Displays the call numbers in the Generated_List listBox.
        /// </summary>
        /// <param name="callNumbers">The list of call numbers to display.</param>
        private void DisplayCallNumbers(List<string> callNumbers)
        {
            Generated_List.Items.Clear();
            foreach (string callNumber in callNumbers)
            {
                Generated_List.Items.Add(callNumber);
            }
        }

        /// <summary>
        /// Event handler for the "Generate Call Numbers" button. Generates and displays new call numbers.
        /// </summary>
        private void btnGenerateCallNumbers_Click(object sender, EventArgs e)
        {
            var callNumbers = callNumberManager.GenerateCallNumbers();
            DisplayCallNumbers(callNumbers);
        }


        /// <summary>
        /// Event handler for the "Check Order" button. Checks the ordering of call numbers and updates progress accordingly.
        /// </summary>
        private void btnCheckOrder_Click(object sender, EventArgs e)
        {
            List<string> userOrder = Generated_List.Items.Cast<string>().ToList();

            if (!callNumberManager.CheckAndProcessOrdering(userOrder, ref currentProgress))
            {
                // Don't do anything if the list wasn't generated
                return;
            }

            bool isOrderCorrect = callNumberManager.CheckOrdering(userOrder);

            // Only update the display if the ordering was correct
            if (isOrderCorrect)
            {
                // Increment the number of correct orders
                currentCorrectOrders++;

                DisplayCallNumbers(callNumberManager.GenerateCallNumbers());
            }
        }

    }
}
