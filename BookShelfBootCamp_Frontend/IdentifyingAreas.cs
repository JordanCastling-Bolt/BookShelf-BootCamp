using Library_Classlib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PROG7132
{
    public partial class IdentifyingAreas : UserControl
    {
        private LibraryLogic libraryLogic = new LibraryLogic();
        private bool isCallNumberToDescription = true;
        private Dictionary<string, string> correctMatches = new Dictionary<string, string>();

        public IdentifyingAreas()
        {
            InitializeComponent();
            GenerateNewQuestion();

            // Assuming the ListViews have been created, set up the drag-drop events for them
            CallNumbersListView.ItemDrag += CallNumbersListView_ItemDrag;
            CallNumbersListView.DragEnter += ListView_DragEnter;
            CallNumbersListView.DragDrop += ListView_DragDrop;

            DescriptionsListView.ItemDrag += DescriptionsListView_ItemDrag;
            DescriptionsListView.DragEnter += ListView_DragEnter;
            DescriptionsListView.DragDrop += ListView_DragDrop;
        }

        private void CallNumbersListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            CallNumbersListView.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void DescriptionsListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DescriptionsListView.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Move;
        }

        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            ListView sourceListView = draggedItem.ListView;
            ListView targetListView = (ListView)sender;

            Point point = targetListView.PointToClient(new Point(e.X, e.Y));
            ListViewItem targetItem = targetListView.GetItemAt(point.X, point.Y);

            // If dropped on another item in the target ListView
            if (targetItem != null)
            {
                int index = targetItem.Index;
                targetListView.Items.Insert(index, (ListViewItem)draggedItem.Clone());
            }
            else // If dropped in an empty space in the target ListView
            {
                targetListView.Items.Add((ListViewItem)draggedItem.Clone());
            }

            // Remove the item from the source ListView
            sourceListView.Items.Remove(draggedItem);

            // Check the match (this depends on your game logic)
            if (libraryLogic.IsCorrectMatch(draggedItem.Text, targetItem?.Text))
            {
                correctMatches.Add(draggedItem.Text, targetItem?.Text);
                // ... rest of your logic
            }
            else
            {
                // ... rest of your logic
            }

            CheckAllCorrectMatches();
        }


        private void GenerateNewQuestion()
        {
            var questionData = libraryLogic.GenerateQuestion(isCallNumberToDescription);

            CallNumbersListView.Items.Clear();
            DescriptionsListView.Items.Clear();

            foreach (var data in questionData.Item1)
            {
                CallNumbersListView.Items.Add(new ListViewItem(data));
            }

            foreach (var data in questionData.Item2)
            {
                DescriptionsListView.Items.Add(new ListViewItem(data));
            }

            // Toggle the question type for the next time
            isCallNumberToDescription = !isCallNumberToDescription;
        }

        private void CheckAllCorrectMatches()
        {
            // Check if all matches are correct
            if (correctMatches.Count == CallNumbersListView.Items.Count)
            {
                MessageBox.Show("Well Done!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnGenerateCallNumbers_Click(object sender, EventArgs e)
        {
            GenerateNewQuestion();
        }
    }
}
