using Library_Classlib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PROG7132
{
    public partial class IdentifyingAreas : UserControl
    {
        private MTC_Logic libraryLogic = new MTC_Logic();
        private readonly Dictionary<string, string> correctMatches = new Dictionary<string, string>();
        private bool isCallNumberToDescription = true; // Step 1: Initialize the flag


        public IdentifyingAreas()
        {
            InitializeComponent();
            StyleListView(CallNumbersListView); // Apply styles to CallNumbersListView
            StyleListView(DescriptionsListView); // Apply styles to DescriptionsListView
            GenerateNewQuestion();

            CallNumbersListView.ItemDrag += ListView_ItemDrag;
            DescriptionsListView.DragEnter += ListView_DragEnter;
            DescriptionsListView.DragDrop += ListView_DragDrop;
        }

        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = DescriptionsListView.PointToClient(new Point(e.X, e.Y));
            ListViewItem destinationItem = DescriptionsListView.GetItemAt(pt.X, pt.Y);

            if (destinationItem != null && e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                bool isMatch;
                if (isCallNumberToDescription)
                {
                    isMatch = libraryLogic.IsCorrectMatch(draggedItem.Text, destinationItem.Text);
                }
                else
                {
                    isMatch = libraryLogic.IsCorrectMatch(destinationItem.Text, draggedItem.Text);
                }

                if (isMatch)
                {
                    progressBar1.Value += 1;
                    correctMatches.Add(draggedItem.Text, destinationItem.Text);
                    CallNumbersListView.Items.Remove(draggedItem);
                    DescriptionsListView.Items.Remove(destinationItem);
                    CheckAllCorrectMatches();
                }
            }
        }


        private void GenerateNewQuestion()
        {
            var questionData = libraryLogic.GenerateQuestion(isCallNumberToDescription);

            CallNumbersListView.Items.Clear();
            DescriptionsListView.Items.Clear();
            correctMatches.Clear();

            // Adding call numbers to the left ListView
            foreach (var data in questionData.Item1)
            {
                ListViewItem item = new ListViewItem(data)
                {
                    ForeColor = Color.Blue,
                    Font = new Font("Arial", 14, FontStyle.Bold)
                };
                CallNumbersListView.Items.Add(item);
            }

            // Adding descriptions to the right ListView
            foreach (var data in questionData.Item2)
            {
                ListViewItem item = new ListViewItem(data)
                {
                    ForeColor = Color.Green,
                    Font = new Font("Arial", 14, FontStyle.Italic)
                };
                DescriptionsListView.Items.Add(item);
            }

            if (isCallNumberToDescription)
            {
                CallNumbersListView.Columns[0].Text = "Call Numbers";
                DescriptionsListView.Columns[0].Text = "Categories";
            }
            else
            {
                CallNumbersListView.Columns[0].Text = "Categories";
                DescriptionsListView.Columns[0].Text = "Call Numbers";
            }
            progressBar1.Value = 0;

            isCallNumberToDescription = !isCallNumberToDescription; // Step 2: Toggle the flag
        }


        private void StyleListView(ListView listView)
        {
            // Common styles for any ListView
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;

            listView.Font = new Font("Verdana", 12);
            listView.ForeColor = Color.Black;
            listView.BackColor = Color.LightGray;

            // Add column header if not added from designer
            listView.Columns.Add("Column Header", -2, HorizontalAlignment.Left);

            // Add border
            listView.BorderStyle = BorderStyle.Fixed3D;
        }

        private void CheckAllCorrectMatches()
        {
            // Check if all matches are correct
            if (correctMatches.Count == 4) // Assuming you start with 4 call numbers
            {
                progressBar1.Value = 0;
                MessageBox.Show("Well Done!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GenerateNewQuestion();
            }
        }

    }
}
