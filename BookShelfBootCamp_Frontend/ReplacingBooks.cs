using Library_Classlib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace PROG7132
{
    public partial class ReplacingBooks : UserControl
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private List<string> mozartCompositions = new List<string>();
        readonly CallNumberManager callNumberManager = new CallNumberManager();
        private int currentProgress = 0;
        private int currentCorrectOrders = 0;
        private ImageList bookImageList = new ImageList();
        public event EventHandler CorrectOrdering;
        private bool isPaused = false;
        private int currentCompositionIndex = 0;
        private Timer progressTimer;

        /// <summary>
        /// Initializes a new instance of the Form1 class.
        /// </summary>
        public ReplacingBooks()
        {
            InitializeComponent();
            InitializeMozartCompositions();

            cboMozartCompositions.SelectedIndex = 0;
            //Setting up achievement badge feature
            achievementBadge.Visible = false;
            achievementBadge.Image = Properties.Resources.badge;


            // Initialize the ImageList before assigning it to the ListView
            bookImageList = new ImageList();
            bookImageList.ImageSize = new Size(130, 60);
            bookImageList.ColorDepth = ColorDepth.Depth32Bit;
            bookImageList.Images.Add(Image.FromFile("Resources/book.jpg"));

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
            var callNumbers = callNumberManager.GenerateCallNumbers();
            DisplayCallNumbers(callNumbers);

            mediaPlayer.MediaOpened += mediaPlayer_MediaOpened;

            mediaPlayer.MediaEnded += mediaPlayer_MediaEnded;

            if (mozartCompositions.Count > 0)
            {
                PlaySelectedComposition(mozartCompositions[0]);
            }

            progressTimer = new Timer();
            progressTimer.Interval = 1000; // Update every second
            progressTimer.Tick += ProgressTimer_Tick;
            progressTimer.Start();
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                songProgressBar.Value = (int)mediaPlayer.Position.TotalSeconds;
            }
        }

        private void InitializeMozartCompositions()
        {
            // Assuming you have the Mozart compositions in a folder named "Music" within Resources
            // Add paths to the mozartCompositions list
            mozartCompositions.Add("Resources/Brendan_Kinsella_-_Mozart_-_Piano_Sonata_in_B-flat_major_III_Allegretto_Grazioso(chosic.com).mp3");
            mozartCompositions.Add("Resources/Overture-to-The-marriage-of-Figaro-K.-492(chosic.com).mp3");
            mozartCompositions.Add("Resources/Mozart-Serenade-in-G-major(chosic.com).mp3");
            mozartCompositions.Add("Resources/Brendan_Kinsella_-_Mozart_-_Sonata_No_13_In_B_Flat_Major_K333_-_II_Andante_Cantabile(chosic.com).mp3");
            mozartCompositions.Add("Resources/Piano-Concerto-no.-21-in-C-major-K.-467-II.-Andante(chosic.com).mp3");

            foreach (var composition in mozartCompositions)
            {
                cboMozartCompositions.Items.Add(Path.GetFileNameWithoutExtension(composition));
            }

            // Set the first composition as the selected item in the ComboBox
            if (cboMozartCompositions.Items.Count > 0)
            {
                cboMozartCompositions.SelectedIndex = 0;
                currentCompositionIndex = 0;
            }
        }

        private void PlaySelectedComposition(string compositionPath)
        {

            mediaPlayer.Open(new Uri(compositionPath, UriKind.Relative));
            mediaPlayer.Play();
            songProgressBar.Value = 0;
        }

        // Call this method when a user selects a composition to play
        private void OnCompositionSelected()
        {
            currentCompositionIndex = cboMozartCompositions.SelectedIndex;
            if (currentCompositionIndex >= 0 && currentCompositionIndex < mozartCompositions.Count)
            {
                PlaySelectedComposition(mozartCompositions[currentCompositionIndex]);
            }
        }



        // Event handlers for your play, pause, stop buttons (you need to add these buttons to your form)
        private void btnPlay_Click(object sender, EventArgs e)
        {
            OnCompositionSelected();
            btnPause.Text = "Pause"; // Reset button text to "Pause"
            isPaused = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                mediaPlayer.Pause();
                btnPause.Text = "Resume"; // Change button text to "Resume"
                isPaused = true;
            }
            else
            {
                mediaPlayer.Play();
                btnPause.Text = "Pause"; // Change button text back to "Pause"
                isPaused = false;
            }
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
            btnPause.Text = "Pause"; // Reset button text to "Pause"
            isPaused = false;
        }

            /// <summary>
            /// Updates the Progress label and Progress bar.
            /// </summary>
            private void UpdateProgressLabel(int progress, int remainingOrders, string message)
        {
            lblProgressInfo.Text = $"{message} {progress} out of 10 correct. {remainingOrders} remaining.";
            replaceBooksBar.Value = progress;
            if (progress == 10)
            {
                achievementBadge.Visible = true;
            }
            else
            {
                achievementBadge.Visible = false;
            }
        }

        private void mediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                this.Invoke(new Action(() =>
                {
                    songProgressBar.Maximum = (int)mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                    songProgressBar.Value = 0;
                }));
            }
        }

        private void mediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            currentCompositionIndex++;
            if (currentCompositionIndex >= mozartCompositions.Count)
            {
                currentCompositionIndex = 0; // Optionally loop to the start
            }

            if (currentCompositionIndex < mozartCompositions.Count)
            {
                this.Invoke(new Action(() =>
                {
                    cboMozartCompositions.SelectedIndex = currentCompositionIndex; // Update ComboBox selection
                    PlaySelectedComposition(mozartCompositions[currentCompositionIndex]);
                }));
            }
            songProgressBar.Value = 0;
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
        public void DisplayCallNumbers(List<string> callNumbers)
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
                    System.Drawing.Brush textBrush = new SolidBrush(System.Drawing.Color.AntiqueWhite);

                    // Choose a smaller font size to fit text in the image
                    Font textFont = new Font("Arial", 8, FontStyle.Bold);  // Adjust the size as needed

                    // Calculate text position
                    SizeF textSize = g.MeasureString(callNumber, textFont);
                    PointF position = new PointF((image.Width - textSize.Width) / 2, (image.Height - textSize.Height) / 2);

                    g.DrawString(callNumber, textFont, textBrush, position);

                    //Creating 3D tile effect
                    using (System.Drawing.Pen borderPen = new System.Drawing.Pen(System.Drawing.Color.Black, 3))
                    {
                        g.DrawRectangle(borderPen, 0, 0, image.Width - 1, image.Height - 1);
                    }
                    using (System.Drawing.Pen lighterPen = new System.Drawing.Pen(System.Drawing.Color.LightGray, 2))
                    {
                        g.DrawLine(lighterPen, 1, 1, image.Width - 2, 1);
                        g.DrawLine(lighterPen, 1, 1, 1, image.Height - 2);
                    }
                    using  (System.Drawing.Pen darkerPen = new System.Drawing.Pen(System.Drawing.Color.DarkGray, 2))
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

            if (result)
            {
                CorrectOrdering?.Invoke(this, EventArgs.Empty);
            }
        }

        private void ShowHowToPlayAlert()
        {
            string howToPlayMessage = "How to Play 'Replacing Books':\n\n" +
                                      "1. The game generates a list of call numbers that you need to arrange in the correct order.\n" +
                                      "2. Drag and drop the call numbers in the list to arrange them in either Numerical or Alphabetical order.\n" +
                                      "3. Click the 'Check Order' button to submit your arrangement.\n" +
                                      "4. You will receive feedback on your progress. Correct arrangements increase your progress.\n" +
                                      "5. You can listen to Mozart compositions while playing. Use the media controls to Play, Pause/Resume, or Stop the music.\n" +
                                      "Good luck and have fun!";

            MessageBox.Show(howToPlayMessage, "How to Play");
        }

        private void howToPlay_Click(object sender, EventArgs e)
        {
            ShowHowToPlayAlert();
        }
    }
}

