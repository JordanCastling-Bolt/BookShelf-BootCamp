using Library_Classlib;
using System;
using System.Windows.Forms;

namespace PROG7132
{
    public partial class IdentifyingAreas : UserControl
    {
        private ListView GeneratedListView;
        private ListView CallNumbersListView; // To display call numbers or descriptions
        private ListView DescriptionsListView; // To display descriptions or call numbers
        private LibraryLogic libraryLogic = new LibraryLogic();
        private bool isCallNumberToDescription = true;

        public IdentifyingAreas()
        {
            InitializeComponent();
            GenerateNewQuestion();
        }

        private void GeneratedListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            GeneratedListView.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void GeneratedListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Move;
        }

        private void GeneratedListView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ListViewItem)) is ListViewItem item)
            {
                // If desired, you can decide where to drop the item or implement rearrangement logic
                GeneratedListView.Items.Add((ListViewItem)item.Clone());
                GeneratedListView.Items.Remove(item);
            }
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

        private void btnGenerateCallNumbers_Click(object sender, EventArgs e)
        {
            GenerateNewQuestion();
        }
    }
}