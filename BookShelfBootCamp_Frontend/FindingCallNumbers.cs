using Library_Classlib.FindingCallNumbers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library_Classlib;

namespace PROG7132
{
    public partial class FindingCallNumbers : UserControl
    {
        private QuizManager quizManager;
        private List<string> currentOptions;
        private int lives;
        private int score;
        private int streak;

        public FindingCallNumbers()
        {
            InitializeComponent();
            InitializeQuiz();
            InitializeLeaderboardListView();
            lives = 3;
            score = 0; 
            streak = 0; 
            InitializeLivesUI();

            optionButton1.Click += optionButton_Click;
            optionButton2.Click += optionButton_Click;
            optionButton3.Click += optionButton_Click;
            optionButton4.Click += optionButton_Click;
        }

        private void InitializeQuiz()
        {
            try
            {
                var deweyTree = new DeweyDecimalTree();

                // Assume your JSON file is named DeweyDecimal.json and it's added as a resource
                // Properties.Resources.DeweyDecimal would be the way to access it
                string jsonContent = Encoding.UTF8.GetString(Properties.Resources.DeweyClassificationComplete); 
                JObject jsonData = JObject.Parse(jsonContent);

                deweyTree.BuildTree(jsonData);
                quizManager = new QuizManager(deweyTree);
                LoadNewQuestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing quiz: {ex.Message}");
            }
            UpdateScoreDisplay();
        }

        private void InitializeLivesUI()
        {
            // Set the Image property of PictureBoxes to the life icon from resources
            Image lifeIcon = Properties.Resources.Life; 

            // Assuming life1, life2, and life3 are the names of your PictureBox controls
            pictureBox1.Image = lifeIcon;
            pictureBox2.Image = lifeIcon;
            pictureBox3.Image = lifeIcon;

            // You can also set other properties like SizeMode if needed
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox1.BackColor = Color.Transparent; 
            pictureBox2.BackColor = Color.Transparent; 
            pictureBox3.BackColor = Color.Transparent;

            UpdateLivesDisplay();
        }

        private void InitializeLeaderboardListView()
        {
            // Assuming your ListView is named leaderboardListView
            // Add columns for user tag and score
            leaderboardListView.Columns.Add("User Tag", -2, HorizontalAlignment.Left);
            leaderboardListView.Columns.Add("Score", -2, HorizontalAlignment.Left);

            // Set the view to show details
            leaderboardListView.View = View.Details;

            // Update the display
            UpdateLeaderboardDisplay();
        }

        private void UpdateLivesDisplay()
        {
            // Update the visibility of life icons based on the number of lives
            pictureBox1.Visible = lives >= 1;
            pictureBox2.Visible = lives >= 2;
            pictureBox3.Visible = lives >= 3;
        }

        private void LoadNewQuestion()
        {
            // Load the new question's description
            string questionDescription = quizManager.NewQuestion();

            questionLabel.Text = EscapeAmpersand(questionDescription);

            // Get the options as a list of strings
            currentOptions = quizManager.GetOptions(); // This should be List<string>

            // Update UI with new question and options
            optionButton1.Text = EscapeAmpersand(currentOptions[0]);
            optionButton2.Text = EscapeAmpersand(currentOptions[1]);
            optionButton3.Text = EscapeAmpersand(currentOptions[2]);
            optionButton4.Text = EscapeAmpersand(currentOptions[3]);
            UpdateScoreDisplay();
        }

        private void optionButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string selectedCallNumber = clickedButton.Text.Split(' ')[0];
                bool isCorrect = quizManager.CheckAnswer(selectedCallNumber);

                if (isCorrect)
                {
                    streak++;
                    score += 10; // Base points for correct answer
                    if (streak > 1) score += (streak - 1) * 5; // Bonus points for streaks
                    UpdateScoreDisplay();

                    MessageBox.Show("Correct answer!");
                    if (quizManager.IsQuizAtThirdStep())
                    {
                        // The quiz is at the third step, get and present options for the section call numbers
                        currentOptions = quizManager.GetOptions();
                        UpdateOptionButtons(currentOptions);
                    }
                    else if (quizManager.IsQuizAtSecondStep())
                    {
                        // The quiz is at the second step, get and present options for the division
                        currentOptions = quizManager.GetOptions();
                        UpdateOptionButtons(currentOptions);
                    }
                    else // The quiz is finished with the current question
                    {
                        lives = 3; // Reset lives
                        UpdateLivesDisplay();
                        // The user has answered all steps correctly, load a new question
                        LoadNewQuestion();
                    }
                }
                else
                {
                    streak = 0; // Reset streak
                    score -= 2; // Penalty for wrong answer
                    MessageBox.Show("Incorrect answer. Try again.");
                    // The answer was incorrect, you might want to reload the same options or provide feedback
                    lives--; // Decrease life
                    UpdateLivesDisplay();

                    if (lives <= 0)
                    {
                        MessageBox.Show("You've run out of lives!");
                        ResetGame();
                    }
                    UpdateScoreDisplay();
                }
            }
        }

        private void UpdateScoreDisplay()
        {
            // Assuming you have a label on your form to display the score
            scoreLabel.Text = $"Score: {score}";
        }

        // Add this method to update the option buttons with new options
        private void UpdateOptionButtons(List<string> options)
        {
            optionButton1.Text = options.Count > 0 ? EscapeAmpersand(options[0]) : string.Empty;
            optionButton2.Text = options.Count > 1 ? EscapeAmpersand(options[1]) : string.Empty;
            optionButton3.Text = options.Count > 2 ? EscapeAmpersand(options[2]) : string.Empty;
            optionButton4.Text = options.Count > 3 ? EscapeAmpersand(options[3]) : string.Empty;

        }

        private void UpdateLeaderboardDisplay()
        {
            var databaseHandler = new DatabaseHandler();
            var leaderboardData = databaseHandler.GetLeaderboardData();

            leaderboardListView.Items.Clear(); // Clear existing items

            foreach (var entry in leaderboardData)
            {
                var item = new ListViewItem(entry.UserTag);
                item.SubItems.Add(entry.Score.ToString());
                leaderboardListView.Items.Add(item);
            }
        }


        private void ResetGame()
        {
            lives = 3; // Reset lives
            streak = 0; // Reset streak
            string userTag = GetUserTag(); // Prompt for user tag

            if (!string.IsNullOrWhiteSpace(userTag))
            {
                var databaseHandler = new DatabaseHandler();
                databaseHandler.SaveLeaderboardEntry(userTag, score); // Save the score
                UpdateLeaderboardDisplay(); // Update leaderboard display
            }

            score = 0; // Reset score after saving to leaderboard
            UpdateLivesDisplay();
            UpdateScoreDisplay(); // Update score display
            InitializeQuiz(); // Reset the quiz to the beginning
        }


        private string EscapeAmpersand(string text)
        {
            return text.Replace("&", "&&");
        }

        private string GetUserTag()
        {
            // Simple InputBox for user tag
            return Microsoft.VisualBasic.Interaction.InputBox("Enter your user tag for the leaderboard:", "User Tag", "DefaultTag");
        }

        private void ShowHowToPlayAlert()
        {
            string howToPlayMessage = "How to Play:\n\n" +
                                      "1. A question related to library call numbers will be displayed.\n" +
                                      "2. You will be given four options. Choose the correct one.\n" +
                                      "3. Each correct answer increases your score by 10 points.\n" +
                                      "4. For each consecutive correct answer (a streak), you earn additional bonus points. " +
                                      "The first correct answer in a streak earns 10 points, and each subsequent correct answer in the streak earns an extra 5 points. " +
                                      "For example, the second correct answer in a streak gives you 15 points, the third gives you 20 points, and so on.\n" +
                                      "5. An incorrect answer breaks the streak and decreases your score by 2 points.\n" +
                                      "6. You have 3 lives. An incorrect answer costs you one life. The game ends when you run out of lives.\n" +
                                      "7. When you lose all lives, you can enter your tag to record your score on the leaderboard.\n\n" +
                                      "Good luck and have fun!";

            MessageBox.Show(howToPlayMessage, "How to Play Finding Call Numbers");
        }


        private void howToPlay_Click(object sender, EventArgs e)
        {
            ShowHowToPlayAlert();
        }
    }
}
