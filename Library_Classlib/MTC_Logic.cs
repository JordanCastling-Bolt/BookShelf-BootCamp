using System;
using System.Collections.Generic;
using System.Linq;

namespace Library_Classlib
{
    /// <summary>
    /// Provides logic for the Match-The-Category game.
    /// </summary>
    public class MTC_Logic
    {
        // Dictionary to hold the mappings between call numbers and their descriptions.
        public Dictionary<string, string> CallNumberDescriptions { get; private set; }

        // Random number generator.
        private readonly Random rand = new Random();

        /// <summary>
        /// Initializes a new instance of the MTC_Logic class.
        /// </summary>
        public MTC_Logic()
        {
            CallNumberDescriptions = new Dictionary<string, string>
            {
                { "000", "Computer science, general works, and information" },
                { "100", "Philosophy and psychology" },
                { "200", "Religion" },
                { "300", "Social sciences" },
                { "400", "Language" },
                { "500", "Natural sciences and mathematics" },
                { "600", "Technology" },
                { "700", "Arts and recreation" },
                { "800", "Literature" },
                { "900", "History and geography" },
            };
        }

        /// <summary>
        /// Checks if the provided pair of call number and description is a correct match.
        /// </summary>
        /// <param name="left">The left item (either a call number or a description).</param>
        /// <param name="right">The right item (either a call number or a description).</param>
        /// <returns>True if they match correctly; otherwise, false.</returns>
        public bool IsCorrectMatch(string left, string right)
        {
            string baseKey;

            // Check if the left item is a call number.
            if (int.TryParse(left, out int numericKey))
            {
                baseKey = (numericKey / 100 * 100).ToString("D3");
                return CallNumberDescriptions.ContainsKey(baseKey) && CallNumberDescriptions[baseKey] == right;
            }
            // Check if the right item is a call number.
            else if (int.TryParse(right, out int numericValue))
            {
                var descriptionMatchKey = CallNumberDescriptions.FirstOrDefault(x => x.Value == left).Key;
                baseKey = (numericValue / 100 * 100).ToString("D3");
                return descriptionMatchKey == baseKey;
            }
            // Both left and right are descriptions.
            else
            {
                return CallNumberDescriptions.ContainsValue(left) && CallNumberDescriptions.ContainsValue(right) && left == right;
            }
        }

        /// <summary>
        /// Generates a set of call numbers and descriptions for a round of the game.
        /// </summary>
        /// <param name="isCallNumberToDescription">Indicates if the left column should contain call numbers.</param>
        /// <returns>A tuple where the first item is the list for the left column and the second item is the list for the right column.</returns>
        public Tuple<List<string>, List<string>> GenerateQuestion(bool isCallNumberToDescription)
        {
            if (isCallNumberToDescription)
            {
                return GenerateQuestionCallNumberToDescription();
            }
            else
            {
                return GenerateQuestionDescriptionToCallNumber();
            }
        }

        //Helper methods created instead of deep nesting
        private Tuple<List<string>, List<string>> GenerateQuestionCallNumberToDescription()
        {
            var leftColumnItems = GetRandomCallNumbers(4);
            var rightColumnItems = leftColumnItems.Select(item => CallNumberDescriptions[GetBaseNumberStr(item)]).ToList();

            AddRandomDescriptions(rightColumnItems, 3); 
            return Tuple.Create(leftColumnItems, rightColumnItems.OrderBy(_ => rand.Next()).ToList());
        }

        private Tuple<List<string>, List<string>> GenerateQuestionDescriptionToCallNumber()
        {
            var leftColumnItems = GetRandomDescriptions(4);
            var rightColumnItems = leftColumnItems.Select(description => GetRandomCallNumberForDescription(description)).ToList();

            AddRandomCallNumbers(rightColumnItems, 3); 
            return Tuple.Create(leftColumnItems, rightColumnItems.OrderBy(_ => rand.Next()).ToList());
        }

        // Helper method to generate a list of random call numbers.
        // It randomly orders the call numbers and takes the specified count.
        private List<string> GetRandomCallNumbers(int count)
        {
            var allKeys = CallNumberDescriptions.Keys.ToList();
            return allKeys.OrderBy(x => rand.Next()).Take(count).ToList();
        }

        // Helper method to generate a list of random descriptions.
        // It randomly orders the descriptions and takes the specified count.
        private List<string> GetRandomDescriptions(int count)
        {
            return CallNumberDescriptions.Values.OrderBy(x => rand.Next()).Take(count).ToList();
        }

        // Helper method to add a specified number of additional random descriptions to a list.
        // It ensures that the added descriptions are not already present in the list.
        private void AddRandomDescriptions(List<string> descriptions, int additionalCount)
        {
            var allDescriptions = new HashSet<string>(CallNumberDescriptions.Values);
            foreach (var existing in descriptions)
            {
                allDescriptions.Remove(existing); 
            }

            var additionalDescriptions = allDescriptions.OrderBy(x => rand.Next()).Take(additionalCount).ToList();
            descriptions.AddRange(additionalDescriptions); 
        }

        // Helper method to add a specified number of additional random call numbers to a list.
        // It ensures that the added call numbers are not already present in the list.
        private void AddRandomCallNumbers(List<string> callNumbers, int additionalCount)
        {
            var allCallNumbers = new HashSet<string>(CallNumberDescriptions.Keys);
            foreach (var existing in callNumbers)
            {
                allCallNumbers.Remove(existing); 
            }

            var additionalCallNumbers = allCallNumbers.OrderBy(x => rand.Next()).Take(additionalCount).ToList();
            callNumbers.AddRange(additionalCallNumbers); 
        }

        // Helper method to get a random call number for a given description.
        // It finds the base call number for the description and adds a random offset.
        private string GetRandomCallNumberForDescription(string description)
        {
            var baseKey = CallNumberDescriptions.FirstOrDefault(x => x.Value == description).Key;
            return (int.Parse(baseKey) + rand.Next(0, 100)).ToString("D3");
        }

        // Helper method to get the base call number string from a given call number.
        // It calculates the base (hundreds) of the call number and returns it as a string.
        private string GetBaseNumberStr(string callNumber)
        {
            return (int.Parse(callNumber) / 100 * 100).ToString("D3");
        }
    }
}

