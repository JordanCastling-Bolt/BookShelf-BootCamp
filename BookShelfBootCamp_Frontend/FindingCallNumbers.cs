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

        public FindingCallNumbers()
        {
            InitializeComponent();
            InitializeQuiz();

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
                        // The user has answered all steps correctly, load a new question
                        LoadNewQuestion();
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect answer. Try again.");
                    // The answer was incorrect, you might want to reload the same options or provide feedback
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

    }
}
