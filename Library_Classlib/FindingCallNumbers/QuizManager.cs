using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Classlib.FindingCallNumbers
{
    public class QuizManager
    {
        private DeweyDecimalTree deweyTree;
        private TreeNode currentEntry;
        private TreeNode correctMainClassEntry;
        private TreeNode correctDivisionEntry;
        private Random random = new Random();
        private bool isLastAnswerCorrect;
        private int quizStep = 1;
        private TreeNode correctSectionEntry;

        public QuizManager(DeweyDecimalTree tree)
        {
            deweyTree = tree;
            isLastAnswerCorrect = false;
            correctMainClassEntry = null;
            NewQuestion(); // Initialize the first question
        }

        // Method to start a new question by selecting a random section
        public string NewQuestion()
        {
            quizStep = 1;
            // Randomly select a third-level entry (section)
            KeyValuePair<string, string> randomSection = deweyTree.GetRandomSectionDescription();
            string sectionCallNumber = randomSection.Key;
            currentEntry = deweyTree.FindSectionByCallNumber(sectionCallNumber);

            correctDivisionEntry = currentEntry?.Parent;

            correctSectionEntry = currentEntry;

            // Set the correct main class entry for this question based on the section selected
            correctMainClassEntry = currentEntry?.Parent?.Parent;

            if (correctMainClassEntry == null)
            {
                throw new InvalidOperationException("Failed to select a valid main class entry.");
            }

            Debug.WriteLine($"New Question: {randomSection.Value}");
            Debug.WriteLine($"Correct Main Class Entry: {correctMainClassEntry.CallNumber} - {correctMainClassEntry.Description}");
            Debug.WriteLine($"Correct Division Entry: {correctDivisionEntry?.CallNumber} - {correctDivisionEntry?.Description}");


            // Return only the description for the question
            return randomSection.Value;
        }

        public int QuizStep
        {
            get { return quizStep; } // Assuming quizStep is the name of your private step tracking variable
        }

        // Method to check the answer and progress through the quiz
        public bool CheckAnswer(string selectedCallNumber)
        {
            // Validate currentEntry
            if (currentEntry == null)
            {
                throw new InvalidOperationException("Current entry is not set.");
            }

            string correctAnswer = GetCurrentStepAnswer();

            // Check if the selected call number matches the correct answer
            isLastAnswerCorrect = (selectedCallNumber == correctAnswer);

            // Log the results
            Debug.WriteLine($"User selected: {selectedCallNumber}");
            Debug.WriteLine($"Correct answer: {correctAnswer}");
            Debug.WriteLine($"Is last answer correct: {isLastAnswerCorrect}");

            if (isLastAnswerCorrect)
            {
                // If the answer is correct, advance to the next step or reset if at the end of the quiz
                quizStep = (quizStep == 3) ? 1 : quizStep + 1;
                return true;
            }
            else
            {
                // If the answer is incorrect and we're on the second or third step, stay on that step
                if (quizStep == 2 || quizStep == 3)
                {
                    // The user should retry the current step without changing the quizStep.
                    return false; // Incorrect answer, retry the current step
                }
                else
                {
                    // If the answer is incorrect and we're on the first step, reset to the first step for a new question
                    quizStep = 1;
                    return false; // Incorrect answer, start a new question
                }
            }
        }

        private string GetCurrentStepAnswer()
        {
            // Determine the correct answer based on the current quiz step
            switch (quizStep)
            {
                case 1: return correctMainClassEntry.CallNumber; // Main Class
                case 2: return correctDivisionEntry.CallNumber;  // Division
                case 3: return correctSectionEntry.CallNumber;   // Section
                default: throw new InvalidOperationException("Quiz step is out of range.");
            }
        }


        // Method to get options for the current question
        public List<string> GetOptions()
        {
            if (quizStep == 1)
            {
                var options = new List<TreeNode> { correctMainClassEntry };

                // Get three random main classes as incorrect options
                var incorrectOptions = deweyTree.Root.Children.Values
                    .Where(x => x != correctMainClassEntry)
                    .OrderBy(x => random.Next())
                    .Take(3)
                    .ToList();

                options.AddRange(incorrectOptions);

                // Shuffle the options
                options = options.OrderBy(x => random.Next()).ToList();

                // Ensure exactly four options
                while (options.Count < 4)
                {
                    var additionalOption = deweyTree.Root.Children.Values
                                        .FirstOrDefault(x => !options.Contains(x));
                    if (additionalOption != null)
                    {
                        options.Add(additionalOption);
                    }
                }
                List<string> optionsToDisplay = options.Select(o => $"{o.CallNumber} {o.Description}").ToList();
                Debug.WriteLine("Options provided:");
                foreach (var option in optionsToDisplay)
                {
                    Debug.WriteLine(option);
                }
                // Return the options in numerical order as a list of formatted strings with call number and description
                return options.OrderBy(o => o.CallNumber)
                              .Select(o => $"{o.CallNumber} {o.Description}")
                              .ToList();
            }
            else if (quizStep == 2)
            {
                // ... existing code for the second step ...
                return GetDivisionOptions();
            }
            else if (quizStep == 3)
            {
                // Start with the correct call number
                var sectionCallNumbers = new List<string> { correctSectionEntry.CallNumber };

                // Add other call numbers from the same section if available
                sectionCallNumbers.AddRange(correctSectionEntry.Children.Values
                    .Select(c => c.CallNumber)
                    .Where(c => c != correctSectionEntry.CallNumber));

                // If there are not enough call numbers, add closely related but incorrect call numbers
                while (sectionCallNumbers.Count < 4)
                {
                    string dummyCallNumber = GenerateCloseDummyCallNumber(sectionCallNumbers);
                    sectionCallNumbers.Add(dummyCallNumber);
                }

                // Shuffle the call numbers
                sectionCallNumbers = sectionCallNumbers.OrderBy(x => random.Next()).ToList();

                return sectionCallNumbers;
            }
            else
            {
                throw new InvalidOperationException("Quiz step is not valid.");
            }
        }

        private string GenerateCloseDummyCallNumber(List<string> existingCallNumbers)
        {
            // Take the first two digits of the correct call number as the base
            string baseCallNumber = correctSectionEntry.CallNumber.Substring(0, 2);

            string dummy;
            int dummyNumber;
            do
            {
                // Generate a random third digit that does not exist in the section
                dummyNumber = random.Next(0, 10);
                dummy = $"{baseCallNumber}{dummyNumber:D1}";
            }
            // Ensure the generated number is not already a child of the correct section and not the correct answer
            while (existingCallNumbers.Contains(dummy) || dummy == correctSectionEntry.CallNumber);

            return dummy;
        }


        public bool IsQuizAtSecondStep()
        {
            return this.quizStep == 2;
        }
        public bool IsQuizAtThirdStep()
        {
            return quizStep == 3;
        }

        private List<string> GetDivisionOptions()
        {
            var options = new List<TreeNode> { correctDivisionEntry };

            // Get three random divisions from the correct main class as incorrect options
            var incorrectOptions = correctMainClassEntry.Children.Values
                .Where(x => x != correctDivisionEntry)
                .OrderBy(x => random.Next())
                .Take(3)
                .ToList();

            options.AddRange(incorrectOptions);

            // Shuffle the options
            options = options.OrderBy(x => random.Next()).ToList();

            // Return the options in numerical order as a list of formatted strings with call number and description
            return options.Select(o => $"{o.CallNumber} {o.Description}")
                          .ToList();
        }
    }
}
