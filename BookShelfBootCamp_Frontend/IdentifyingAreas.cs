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
                CallNumbersListView.Items.Add(new ListViewItem(data));
            }

            // Adding descriptions to the right ListView
            foreach (var data in questionData.Item2)
            {
                DescriptionsListView.Items.Add(new ListViewItem(data));
            }

            isCallNumberToDescription = !isCallNumberToDescription; // Step 2: Toggle the flag
        }

        private void CheckAllCorrectMatches()
        {
            // Check if all matches are correct
            if (correctMatches.Count == 4) // Assuming you start with 4 call numbers
            {
                MessageBox.Show("Well Done!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GenerateNewQuestion();
            }
        }


        private void btnGenerateCallNumbers_Click(object sender, EventArgs e)
        {
            GenerateNewQuestion();
        }
    }
}
