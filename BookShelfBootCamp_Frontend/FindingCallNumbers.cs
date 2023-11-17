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

namespace PROG7132
{
    public partial class FindingCallNumbers : UserControl
    {
        private QuizManager quizManager;
        private List<string> currentOptions; // This should be List<string> now
        private int lives;

        private PictureBox life1;
        private PictureBox life2;
        private PictureBox life3;

        public FindingCallNumbers()
        {
            InitializeComponent();
            InitializeQuiz();

            lives = 3;
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

            // Get the options as a list of strings
            currentOptions = quizManager.GetOptions(); // This should be List<string>

            // Update UI with new question and options
            questionLabel.Text = questionDescription;
            optionButton1.Text = currentOptions[0];
            optionButton2.Text = currentOptions[1];
            optionButton3.Text = currentOptions[2];
            optionButton4.Text = currentOptions[3];
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
                    MessageBox.Show("Incorrect answer. Try again.");
                    // The answer was incorrect, you might want to reload the same options or provide feedback
                    lives--; // Decrease life
                    UpdateLivesDisplay();

                    if (lives <= 0)
                    {
                        MessageBox.Show("You've run out of lives!");
                        ResetGame();
                    }
                }
            }
        }

        // Add this method to update the option buttons with new options
        private void UpdateOptionButtons(List<string> options)
        {
            optionButton1.Text = options.Count > 0 ? options[0] : string.Empty;
            optionButton2.Text = options.Count > 1 ? options[1] : string.Empty;
            optionButton3.Text = options.Count > 2 ? options[2] : string.Empty;
            optionButton4.Text = options.Count > 3 ? options[3] : string.Empty;
        }

        private void ResetGame()
        {
            lives = 3; // Reset lives
            UpdateLivesDisplay();
            InitializeQuiz(); // Reset the quiz to the beginning
                              
        }

    }
}
