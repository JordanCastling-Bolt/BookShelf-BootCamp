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
    /// The main form responsible for managing the interaction with the Call Number Manager and the GUI.
    /// </summary>
    public partial class Form1 : Form
    {
        readonly CallNumberManager callNumberManager = new CallNumberManager();
        private int currentProgress = 0;
        private int currentCorrectOrders = 0;
        private ImageList bookImageList = new ImageList();

        /// <summary>
        /// Initializes a new instance of the Form1 class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // Initialize the ImageList before assigning it to the ListView
            bookImageList = new ImageList();
            bookImageList.ImageSize = new Size(130, 60);
            bookImageList.ColorDepth = ColorDepth.Depth32Bit;
            bookImageList.Images.Add(Image.FromFile("C:\\Users\\Jordan\\Documents\\GitHub\\BookShelf-BootCamp\\BookShelfBootCamp_Frontend\\Resources\\book.jpg"));

            // Setting up drag and drop functionality and registering progress update event
            GeneratedListView.AllowDrop = true;
            GeneratedListView.MouseDown += ListBox_MouseDown;
            GeneratedListView.DragOver += ListBox_DragOver;
            GeneratedListView.DragDrop += ListBox_DragDrop;
            GeneratedListView.View = View.Details; // Set the view to show details
            GeneratedListView.Columns.Add("Library Books", 135); // Add a column
            GeneratedListView.LargeImageList = bookImageList; // Attach the image list
            GeneratedListView.SmallImageList = bookImageList;

            callNumberManager.OnProgressUpdate += UpdateProgressLabel;
            this.Controls.Add(this.lblProgressInfo);
        }

        /// <summary>
        /// Updates the Progress label and Progress bar.
        /// </summary>
        private void UpdateProgressLabel(int progress, int remainingOrders, string message)
        {
            lblProgressInfo.Text = $"{message} {progress} out of 10 correct. {remainingOrders} remaining.";
            replaceBooksBar.Value = progress;
        }

        /// <summary>
        /// Handles mouse and drag events for drag-and-drop functionality in the list view.
        /// </summary>
        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (GeneratedListView.SelectedItems.Count == 0) return; // Check if any items are selected
            ListViewItem item = GeneratedListView.SelectedItems[0];
            GeneratedListView.DoDragDrop(item, DragDropEffects.Move);
        }

        /// <summary>
        /// Handles mouse and drag events for drag-and-drop functionality in the list view.
        /// </summary>
        private void ListBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Handles mouse and drag events for drag-and-drop functionality in the list view.
        /// </summary>
        private void ListBox_DragDrop(object sender, DragEventArgs e)
        {
            Point point = GeneratedListView.PointToClient(new Point(e.X, e.Y));
            ListViewHitTestInfo hitTest = GeneratedListView.HitTest(point);

            // Null check
            if (hitTest == null || hitTest.Item == null)
                return;

            ListViewItem data = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;

            // Null check
            if (data == null)
                return;

            // Store the index before removing the item
            int index = hitTest.Item.Index;

            // Check if index is valid
            if (index >= 0 && index < GeneratedListView.Items.Count)
            {
                // Remove the item
                GeneratedListView.Items.Remove(data);

                // Insert at the new index
                GeneratedListView.Items.Insert(index, data);
            }
        }

        /// <summary>
        /// Displays the call numbers in the GeneratedListView.
        /// </summary>
        /// <param name="callNumbers">The list of call numbers to display.</param>
        private void DisplayCallNumbers(List<string> callNumbers)
        {
            GeneratedListView.Items.Clear();
            bookImageList.Images.Clear(); // Also clear the image list

            try
            {
                // Book images
                bookImageList.Images.Add(Image.FromFile("Resources/book.jpg"));
                bookImageList.Images.Add(Image.FromFile("Resources/book2.jpg"));
                bookImageList.Images.Add(Image.FromFile("Resources/book3.jpg"));
                bookImageList.Images.Add(Image.FromFile("Resources/book4.jpg"));
                bookImageList.Images.Add(Image.FromFile("Resources/book5.jpg"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Random random = new Random(); // Initialize a random number generator

            foreach (string callNumber in callNumbers)
            {
                int randomIndex = random.Next(0, 5); // Randomly choose between 0 and 1
                Image image = new Bitmap(bookImageList.Images[randomIndex]); // Choose a random image


                // Draw call number on the image
                using (Graphics g = Graphics.FromImage(image))
                {
                    // Customize these to control how the text looks
                    Brush textBrush = new SolidBrush(Color.AntiqueWhite);

                    // Choose a smaller font size to fit text in the image
                    Font textFont = new Font("Arial", 8, FontStyle.Bold);  // Adjust the size as needed

                    // Calculate text position
                    SizeF textSize = g.MeasureString(callNumber, textFont);
                    PointF position = new PointF((image.Width - textSize.Width) / 2, (image.Height - textSize.Height) / 2);

                    g.DrawString(callNumber, textFont, textBrush, position);

                    //Creating 3D tile effect
                    using (Pen borderPen = new Pen(Color.Black, 3))
                    {
                        g.DrawRectangle(borderPen, 0, 0, image.Width - 1, image.Height - 1);
                    }
                    using (Pen lighterPen = new Pen(Color.LightGray, 2))
                    {
                        g.DrawLine(lighterPen, 1, 1, image.Width - 2, 1);
                        g.DrawLine(lighterPen, 1, 1, 1, image.Height - 2);
                    }
                    using (Pen darkerPen = new Pen(Color.DarkGray, 2))
                    {
                        g.DrawLine(darkerPen, image.Width - 2, 1, image.Width - 2, image.Height - 2);
                        g.DrawLine(darkerPen, 1, image.Height - 2, image.Width - 2, image.Height - 2);
                    }
                }


                // Add this new image to your image list
                bookImageList.Images.Add(image);

                // Create a new ListViewItem
                ListViewItem item = new ListViewItem("")
                {
                    ImageIndex = bookImageList.Images.Count - 1, // Last image added
                    Tag = callNumber  // Store the call number in the Tag property
                };

                GeneratedListView.Items.Add(item);
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
            List<string> userOrder = GeneratedListView.Items.Cast<ListViewItem>()
                .Select(item => item.Tag as string) // Get the call number from the Tag property
                .ToList();
            if (!callNumberManager.CheckAndProcessOrdering(userOrder, ref currentProgress))
            {
                // Don't do anything if the list wasn't generated
                return;
            }

            bool result = CallNumberManager.CheckOrdering(userOrder);

            // Only update the display if the ordering was correct
            if (result)
            {
                // Increment the number of correct orders
                currentCorrectOrders++;

                // Generate and display new call numbers only once
                var newCallNumbers = callNumberManager.GenerateCallNumbers();
                DisplayCallNumbers(newCallNumbers);
            }
        }
    }
}
