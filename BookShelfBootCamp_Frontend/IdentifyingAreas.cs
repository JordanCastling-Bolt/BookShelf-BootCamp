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
        private bool isCallNumberToDescription = false;
        private int streakCount = 0;
        private ShuffleMode currentShuffleMode = ShuffleMode.None;

        public IdentifyingAreas()
        {
            InitializeComponent();
            StyleListView(CallNumbersListView);
            StyleListView(DescriptionsListView);
            GenerateNewQuestion();

            CallNumbersListView.ItemDrag += ListView_ItemDrag;
            DescriptionsListView.DragEnter += ListView_DragEnter;
            DescriptionsListView.DragDrop += ListView_DragDrop;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnHint, "Click for game instructions.");
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

            // Clear existing items
            CallNumbersListView.Items.Clear();
            DescriptionsListView.Items.Clear();
            correctMatches.Clear();

            // Populate call numbers
            foreach (var data in questionData.Item1)
            {
                ListViewItem item = new ListViewItem(data)
                {
                    ForeColor = Color.Blue,
                    Font = new Font("Arial", 14, FontStyle.Bold)
                };
                CallNumbersListView.Items.Add(item);
            }

            // Populate descriptions
            foreach (var data in questionData.Item2)
            {
                ListViewItem item = new ListViewItem(data)
                {
                    ForeColor = Color.Green,
                    Font = new Font("Arial", 14, FontStyle.Italic)
                };
                DescriptionsListView.Items.Add(item);
            }

            // Apply shuffling after populating both lists
            switch (currentShuffleMode)
            {
                case ShuffleMode.Matches:
                    ShuffleListViewItems(CallNumbersListView);
                    break;
                case ShuffleMode.Clues:
                    ShuffleListViewItems(DescriptionsListView);
                    break;
                case ShuffleMode.Both:
                    ShuffleListViewItems(CallNumbersListView);
                    ShuffleListViewItems(DescriptionsListView);
                    break;
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

            isCallNumberToDescription = !isCallNumberToDescription;
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
                streakCount++; // Increment the streak count

                Image badgeImage = null;

                // Check for streak badges
                if (streakCount == 3)
                {
                    badgeImage = Properties.Resources._3streak;
                }
                else if (streakCount == 5)
                {
                    badgeImage = Properties.Resources._5streak;
                }
                else if (streakCount == 10)
                {
                    badgeImage = Properties.Resources._10streak;
                }

                if (badgeImage != null)
                {
                    badgePictureBox.SizeMode = PictureBoxSizeMode.Zoom; 
                    badgePictureBox.Image = badgeImage;
                    badgePictureBox.Visible = true;
                }

                MessageBox.Show("Well Done!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GenerateNewQuestion();
            }
        }

        enum ShuffleMode
        {
            None,
            Matches,
            Clues,
            Both
        }

        private void ShuffleListViewItems(ListView listView)
        {
            var rand = new Random();
            var items = new List<ListViewItem>();

            // Copy existing items to a temporary list
            foreach (ListViewItem item in listView.Items)
            {
                items.Add((ListViewItem)item.Clone());
            }

            listView.Items.Clear();

            // Shuffle the temporary list
            int n = items.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                var value = items[k];
                items[k] = items[n];
                items[n] = value;
            }

            // Populate the ListView with shuffled items
            listView.Items.AddRange(items.ToArray());
        }

        // Your button click handlers would now simply call ShuffleListViewItems

        private void btnShuffleMatches_Click(object sender, EventArgs e)
        {
            ShuffleListViewItems(CallNumbersListView);
        }

        private void btnShuffleClues_Click(object sender, EventArgs e)
        {
            ShuffleListViewItems(DescriptionsListView);
        }

        private void btnShuffleBoth_Click(object sender, EventArgs e)
        {
            ShuffleListViewItems(CallNumbersListView);
            ShuffleListViewItems(DescriptionsListView);
        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
               "How to Play:\n" +
               "1. Drag items from the left (Clues) to the right (Matches) to match them.\n" +
               "2. A successful match will remove both items from the lists.\n" +
               "3. Match all items correctly to complete the round.\n" +
               "4. New rounds will swap Categories and Call Numbers and vice versa. \n" +
               "5. Complete 10 rounds to earn all the badges.\n" +
               " Enjoy ",
               "Game Instructions",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information
           );
        }
    }
}
