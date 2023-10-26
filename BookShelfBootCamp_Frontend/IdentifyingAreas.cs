using Library_Classlib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PROG7132
{
    public partial class IdentifyingAreas : UserControl
    {
        // Class variables
        private readonly MTC_Logic libraryLogic = new MTC_Logic();
        private readonly Dictionary<string, string> correctMatches = new Dictionary<string, string>();
        private bool isCallNumberToDescription = false;
        private int streakCount = 0;
        private readonly ShuffleMode currentShuffleMode = ShuffleMode.None;

        /// <summary>
        /// Constructor for the IdentifyingAreas UserControl.
        /// Initializes components, styles, and event handlers.
        /// </summary>
        public IdentifyingAreas()
        {
            InitializeComponent();
            StyleListView(CallNumbersListView);
            StyleListView(DescriptionsListView);
            GenerateNewQuestion();

            // Assigning drag and drop event handlers for list views
            CallNumbersListView.ItemDrag += ListView_ItemDrag;
            DescriptionsListView.DragEnter += ListView_DragEnter;
            DescriptionsListView.DragDrop += ListView_DragDrop;

            // ToolTip for the hint button
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnHint, "Click for game instructions.");

            // Timer event for resetting color
            colorResetTimer.Tick += ColorResetTimer_Tick;
        }

        /// <summary>
        /// Timer event to reset the color of an incorrect item after a delay.
        /// </summary>
        private void ColorResetTimer_Tick(object sender, EventArgs e)
        {
            if (colorResetTimer.Tag is ListViewItem item)
            {
                item.ForeColor = Color.Blue; // Reset the color back to blue
            }
            colorResetTimer.Stop(); // Stop the timer
        }

        /// <summary>
        /// Handles the start of a drag operation on a ListViewItem.
        /// </summary>
        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        /// <summary>
        /// Handles the drag enter event to set the drag effect.
        /// </summary>
        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Handles the drop event to match items or show an incorrect match.
        /// </summary>
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

                // Successful match
                if (isMatch)
                {
                    progressBar1.Value += 1;
                    correctMatches.Add(draggedItem.Text, destinationItem.Text);
                    CallNumbersListView.Items.Remove(draggedItem);
                    DescriptionsListView.Items.Remove(destinationItem);
                    CheckAllCorrectMatches();
                }
                // Incorrect match
                else
                {
                    draggedItem.ForeColor = Color.Red;  // Highlight the incorrect item in red
                    draggedItem.Selected = false;  // Deselect the dragged item
                    colorResetTimer.Tag = draggedItem;
                    colorResetTimer.Start();  // Start the timer to reset the color after a delay
                }
            }
        }

        /// <summary>
        /// Generates a new set of call numbers and descriptions for the game.
        /// </summary>
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

            // Apply shuffling based on the current mode
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

            // Update column headers based on the current mode
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

            // Toggle mode
            isCallNumberToDescription = !isCallNumberToDescription;
        }

        /// <summary>
        /// Applies a consistent style to the provided ListView.
        /// </summary>
        private void StyleListView(ListView listView)
        {
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Font = new Font("Verdana", 12);
            listView.ForeColor = Color.Black;
            listView.BackColor = Color.LightGray;
            listView.Columns.Add("Column Header", -2, HorizontalAlignment.Left);
            listView.BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// Checks if all items have been matched correctly.
        /// </summary>
        private void CheckAllCorrectMatches()
        {
            if (correctMatches.Count == 4) // Assumes you start with 4 call numbers
            {
                progressBar1.Value = 0;
                streakCount++;

                Image badgeImage = null;

                // Assign badges based on streak count
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

                // Inform the player of the successful finished round
                MessageBox.Show("Well Done on completing a round, lets see how many you can do.", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GenerateNewQuestion();
            }
        }

        /// <summary>
        /// Enumeration to define shuffle modes.
        /// </summary>
        enum ShuffleMode
        {
            None,
            Matches,
            Clues,
            Both
        }

        /// <summary>
        /// Shuffles the items in a ListView.
        /// </summary>
        private void ShuffleListViewItems(ListView listView)
        {
            var rand = new Random();
            var items = new List<ListViewItem>();

            foreach (ListViewItem item in listView.Items)
            {
                items.Add((ListViewItem)item.Clone());
            }

            listView.Items.Clear();

            int n = items.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                var value = items[k];
                items[k] = items[n];
                items[n] = value;
            }

            listView.Items.AddRange(items.ToArray());
        }

        // Event handlers for the shuffle buttons
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

        /// <summary>
        /// Event handler for the hint button.
        /// Displays the game instructions to the player.
        /// </summary>
        private void btnHint_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
               "How to Play:\n" +
               "1. Drag items from the left (Clues) to the right (Matches) to match them.\n" +
               "2. A successful match will remove both items from the lists while an incorrect match will highlight the Clue in red.\n" +
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
