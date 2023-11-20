using Library_Classlib;
using Library_Classlib.FindingCallNumbers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG7132
{
    public partial class FindingCallNumbers : UserControl
    {
        // Database handler for leaderboard operations
        private DatabaseHandler databaseHandler;

        // Manages the quiz logic
        private QuizManager quizManager;

        // Stores current options for the quiz question
        private List<string> currentOptions;

        // Game state variables: lives, score, and streak
        private int lives;
        private int score;
        private int streak;

        public FindingCallNumbers()
        {
            // Initialize component designs and controls
            InitializeComponent();

            // Set initial game state
            lives = 3;
            score = 0;
            streak = 0;

            // Initialize UI elements for displaying lives
            InitializeLivesUI();

            // Initialize database handler
            databaseHandler = new DatabaseHandler();

            // Setup event handlers for option buttons
            optionButton1.Click += optionButton_Click;
            optionButton2.Click += optionButton_Click;
            optionButton3.Click += optionButton_Click;
            optionButton4.Click += optionButton_Click;

            // Perform asynchronous initialization tasks
            InitializeAsync();
        }

        //Updates life icons
        private void UpdateLivesDisplay()
        {
            pictureBox1.Visible = lives >= 1;
            pictureBox2.Visible = lives >= 2;
            pictureBox3.Visible = lives >= 3;
        }

        // Asynchronously initialize quiz and leaderboard list view
        private async Task InitializeAsync()
        {
            await InitializeQuiz();
            await InitializeLeaderboardListView();
        }

        // Initialize the quiz with data and set up the first question
        private async Task InitializeQuiz()
        {
            try
            {
                // Load and parse the Dewey Decimal Classification data
                var deweyTree = new DeweyDecimalTree();
                string jsonContent = Encoding.UTF8.GetString(Properties.Resources.DeweyClassificationComplete);
                JObject jsonData = JObject.Parse(jsonContent);

                // Build the Dewey tree and initialize the quiz
                deweyTree.BuildTree(jsonData);
                quizManager = new QuizManager(deweyTree);
                LoadNewQuestion();
            }
            catch (Exception ex)
            {
                // Display error message if initialization fails
                MessageBox.Show($"Error initializing quiz: {ex.Message}");
            }
            UpdateScoreDisplay();
        }

        // Setup the UI elements for displaying the player's lives
        private void InitializeLivesUI()
        {
            Image lifeIcon = Properties.Resources.Life;

            // Set images and properties for life icons
            pictureBox1.Image = lifeIcon;
            pictureBox2.Image = lifeIcon;
            pictureBox3.Image = lifeIcon;

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;

            // Update the display of lives on the UI
            UpdateLivesDisplay();
        }

        // Asynchronously initialize the leaderboard view
        private async Task InitializeLeaderboardListView()
        {
            // Define columns for the leaderboard
            leaderboardListView.Columns.Add("User Tag", -2, HorizontalAlignment.Left);
            leaderboardListView.Columns.Add("Score", -2, HorizontalAlignment.Left);

            // Set the view to show details
            leaderboardListView.View = View.Details;

            // Update the leaderboard display asynchronously
            await UpdateLeaderboardDisplay();
        }

        // Load a new quiz question and update the options
        private void LoadNewQuestion()
        {
            string questionDescription = quizManager.NewQuestion();

            // Set the question text and update the option buttons
            questionLabel.Text = EscapeAmpersand(questionDescription);
            currentOptions = quizManager.GetOptions();

            // Update text for each option button
            optionButton1.Text = EscapeAmpersand(currentOptions[0]);
            optionButton2.Text = EscapeAmpersand(currentOptions[1]);
            optionButton3.Text = EscapeAmpersand(currentOptions[2]);
            optionButton4.Text = EscapeAmpersand(currentOptions[3]);

            // Update the score display
            UpdateScoreDisplay();
        }

        // Event handler for option button clicks
        private void optionButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Check if the selected answer is correct
                string selectedCallNumber = clickedButton.Text.Split(' ')[0];
                bool isCorrect = quizManager.CheckAnswer(selectedCallNumber);

                if (isCorrect)
                {
                    // Update score and streak for correct answers
                    streak++;
                    score += 10;
                    if (streak > 1) score += (streak - 1) * 5;

                    MessageBox.Show("Correct answer!");

                    // Load new options based on the quiz step
                    if (quizManager.IsQuizAtThirdStep())
                    {
                        currentOptions = quizManager.GetOptions();
                        UpdateOptionButtons(currentOptions);
                    }
                    else if (quizManager.IsQuizAtSecondStep())
                    {
                        currentOptions = quizManager.GetOptions();
                        UpdateOptionButtons(currentOptions);
                    }
                    else
                    {
                        // Loads a new question
                        LoadNewQuestion();
                    }
                }
                else
                {
                    // Handle incorrect answers
                    streak = 0;
                    score -= 2;
                    MessageBox.Show("Incorrect answer. Try again.");
                    lives--;
                    UpdateLivesDisplay();

                    // Check for game over condition
                    if (lives <= 0)
                    {
                        MessageBox.Show("You've run out of lives!");
                        ResetGame();
                    }
                }

                // Update the score display
                UpdateScoreDisplay();
            }
        }

        // Updates the display of the player's score
        private void UpdateScoreDisplay()
        {
            scoreLabel.Text = $"Score: {score}";
        }

        // Updates the text of option buttons with the current options
        private void UpdateOptionButtons(List<string> options)
        {
            optionButton1.Text = options.Count > 0 ? EscapeAmpersand(options[0]) : string.Empty;
            optionButton2.Text = options.Count > 1 ? EscapeAmpersand(options[1]) : string.Empty;
            optionButton3.Text = options.Count > 2 ? EscapeAmpersand(options[2]) : string.Empty;
            optionButton4.Text = options.Count > 3 ? EscapeAmpersand(options[3]) : string.Empty;
        }

        // Updates the leaderboard display asynchronously
        private async Task UpdateLeaderboardDisplay()
        {
            var leaderboardData = await databaseHandler.GetLeaderboardData();

            leaderboardListView.Invoke((MethodInvoker)(() =>
            {
                leaderboardListView.Items.Clear();

                // Populate leaderboard with entries from the database
                foreach (var entry in leaderboardData)
                {
                    var item = new ListViewItem(entry.UserTag);
                    item.SubItems.Add(entry.Score.ToString());
                    leaderboardListView.Items.Add(item);
                }
            }));
        }

        // Resets the game state and updates the leaderboard
        private async void ResetGame()
        {
            // Reset game state variables
            lives = 3;
            streak = 0;

            // Prompt the user for their tag and save their score
            string userTag = GetUserTag();
            if (!string.IsNullOrWhiteSpace(userTag))
            {
                await databaseHandler.SaveLeaderboardEntry(userTag, score);
                await UpdateLeaderboardDisplay();
            }

            // Reset the score and update UI elements
            score = 0;
            UpdateLivesDisplay();
            UpdateScoreDisplay();
            InitializeQuiz();
        }

        // Escapes ampersands in text to display correctly in labels
        private string EscapeAmpersand(string text)
        {
            return text.Replace("&", "&&");
        }

        // Prompts the user to enter their tag for the leaderboard
        private string GetUserTag()
        {
            return Microsoft.VisualBasic.Interaction.InputBox("Enter your user tag for the leaderboard:", "User Tag", "DefaultTag");
        }

        // Displays a how-to-play message to the user
        private void ShowHowToPlayAlert()
        {
            string howToPlayMessage = "How to Play:\n\n" +
                                      "1. A question related to library call numbers will be displayed.\n" +
                                      "2. You will be given four options. Choose the correct one.\n" +
                                      "3. Each correct answer increases your score by 10 points and the next level of options will show until you get the call number for the description.\n" +
                                      "4. For each consecutive correct answer (a streak), you earn additional bonus points.\n" +
                                      "5. An incorrect answer breaks the streak and decreases your score by 2 points.\n" +
                                      "6. You have 3 lives. An incorrect answer costs you one life. The game ends when you run out of lives.\n" +
                                      "7. When you lose all lives, you can enter your tag to record your score on the leaderboard.\n\n" +
                                      "Good luck and have fun!";

            MessageBox.Show(howToPlayMessage, "How to Play Finding Call Numbers");
        }

        // Event handler for clicking the 'How To Play' button
        private void howToPlay_Click(object sender, EventArgs e)
        {
            ShowHowToPlayAlert();
        }
    }
}

