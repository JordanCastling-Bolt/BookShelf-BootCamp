using System;
using System.Collections.Generic;

namespace Library_Classlib
{
    /// <summary>
    /// Manages the generation, ordering, and processing of call numbers for a library.
    /// </summary>
    public class CallNumberManager
    {
        //Stores the call numbers in a list
        private List<string> callNumbers = new List<string>();
        private readonly Random random = new Random();
        // Added flag
        private bool isListGenerated = false;
        /// <summary>
        /// Delegate to handle progress update events.
        /// </summary>
        /// <param name="progress">Current progress as an integer.</param>
        /// <param name="message">A message to describe the progress.</param>
        public delegate void ProgressHandler(int progress, int remainingOrders, string message);

        /// <summary>
        /// Event triggered when there is a progress update.
        /// </summary>
        public event ProgressHandler OnProgressUpdate;

        /// <summary>
        /// Generates a list of call numbers based on predefined classes and authors.
        /// </summary>
        /// <returns>
        /// List of generated call numbers.
        /// </returns>
        public List<string> GenerateCallNumbers()
        {
            // Clear any existing call numbers
            callNumbers.Clear();

            // DDS main classes as ranges
            int[][] classes = {
        new[] {000, 099}, // Computer Science, Information & General Works
        new[] {100, 199}, // Philosophy & Psychology
        new[] {200, 299}, // Religion
        new[] {300, 399}, // Social Sciences
        new[] {400, 499}, // Language
        new[] {500, 599}, // Science
        new[] {600, 699}, // Technology
        new[] {700, 799}, // Arts & Recreation
        new[] {800, 899}, // Literature
        new[] {900, 999}  // History & Geography
    };
            //List of authors
            string[] authors = { "JAM", "SMI", "LEE", "KIM", "JOH", "DAR", "MAT", "COL", "BRO", "ROW", "ADA", "BRY", "CHE", "DAV", "ELL", "FOX", "GRE", "HAR", "ING", "JEN", "KAY", "LEW", "MAX", "NEL", "OSB", "PAU", "QUA", "RIC", "STE", "TYL", "UPT", "VIN", "WAL", "YAR", "ZAN" };


            for (int i = 0; i < 10; i++)
            {
                // Selecting a random main class range
                int[] mainClassRange = classes[random.Next(classes.Length)];

                // Generating a random number within the selected main class range
                int mainClass = random.Next(mainClassRange[0], mainClassRange[1] + 1);

                // Generating a random number for the subcategory, in the range 00 to 99
                int subcategory = random.Next(100);

                // Getting a random author's surname from the predefined list
                string authorSurname = authors[random.Next(authors.Length)];

                // Concatenating the numbers with a dot and the author's surname
                string callNumber = mainClass.ToString("000") + "." + subcategory.ToString("00") + " " + authorSurname;

                callNumbers.Add(callNumber);
            }
            // Set the flag to true once the list is generated
            isListGenerated = true;
            return callNumbers;
        }

        /// <summary>
        /// Checks if the given list of call numbers is in correct order, either numerically or alphabetically.
        /// </summary>
        /// <param name="userOrder">The list of call numbers to check.</param>
        /// <returns>
        /// True if the ordering is correct, false otherwise.
        /// </returns>
        public static bool CheckOrdering(List<string> userOrder)
        {
            // Initialize flags to track ordering types
            bool isNumericalOrder = true; // Indicates numerical order
            bool isAlphabeticalOrder = true; // Indicates alphabetical order

            // Loop through the user's ordered list
            for (int i = 1; i < userOrder.Count; i++)
            {
                // Split the call numbers into their components
                string[] partsA = userOrder[i - 1].Split(' ');
                string[] partsB = userOrder[i].Split(' ');

                // Extract and compare the main class and subcategory parts
                int mainClassA = int.Parse(partsA[0].Split('.')[0]);
                int subcategoryA = int.Parse(partsA[0].Split('.')[1]);

                int mainClassB = int.Parse(partsB[0].Split('.')[0]);
                int subcategoryB = int.Parse(partsB[0].Split('.')[1]);

                // Check if the main class or subcategory is out of order
                if (mainClassA > mainClassB ||
                    (mainClassA == mainClassB && subcategoryA > subcategoryB))
                {
                    isNumericalOrder = false;
                }

                // Compare the author surnames alphabetically
                if (string.Compare(partsA[1], partsB[1]) > 0)
                {
                    isAlphabeticalOrder = false;
                }
            }

            // The order is correct if it's either numerical or alphabetical
            return isNumericalOrder || isAlphabeticalOrder;
        }


        /// <summary>
        /// Checks and processes the ordering of call numbers. Updates progress if the ordering is correct.
        /// </summary>
        /// <param name="userOrder">The list of call numbers ordered by the user.</param>
        /// <param name="currentProgress">Reference to the current progress value.</param>
        public bool CheckAndProcessOrdering(List<string> userOrder, ref int currentProgress)
        {
            //Checks if a list of call numbers has been generated
            if (!isListGenerated)
            {
                OnProgressUpdate?.Invoke(currentProgress, 10 - currentProgress, "Please generate a list of call numbers first.");
                return false;
            }

            else if (CheckOrdering(userOrder))
            {
                currentProgress += 1;
                int remainingOrders = 10 - currentProgress;

                // Pass remainingOrders as a parameter in OnProgressUpdate
                OnProgressUpdate?.Invoke(currentProgress, remainingOrders, "Congratulations on getting the order correct!");

                // Generate a new set of call numbers.
                GenerateCallNumbers();

                //If the user gets 10 orders correct, progress event updates and displays message to user
                if (currentProgress == 10)
                {
                    OnProgressUpdate?.Invoke(currentProgress, remainingOrders, "Congratulations! You have correctly reordered 10 times!");
                    currentProgress = 0;
                }

                return true;
            }
            else
            {
                OnProgressUpdate?.Invoke(currentProgress, 10 - currentProgress, "Incorrect ordering. Try again!");
                return false;
            }
        }

    }
}
