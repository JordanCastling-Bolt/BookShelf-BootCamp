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
            var leftColumnItems = new List<string>();
            var rightColumnItems = new List<string>();

            var baseNumbers = Enumerable.Range(0, 10).Select(x => x * 100).ToList();
            var poolForLeftColumn = new List<string>();

            if (isCallNumberToDescription)
            {
                // Populate the left column with call numbers.
                foreach (var baseNumber in baseNumbers)
                {
                    var randomOffset = rand.Next(0, 100);
                    var newNumber = baseNumber + randomOffset;
                    var newNumberStr = newNumber.ToString("D3");
                    poolForLeftColumn.Add(newNumberStr);
                }

                while (leftColumnItems.Count < 4)
                {
                    var randomItem = poolForLeftColumn[rand.Next(poolForLeftColumn.Count)];
                    if (!leftColumnItems.Contains(randomItem))
                    {
                        leftColumnItems.Add(randomItem);
                    }
                }

                foreach (var item in leftColumnItems)
                {
                    var baseNumberStr = (int.Parse(item) / 100 * 100).ToString("D3");
                    rightColumnItems.Add(CallNumberDescriptions[baseNumberStr]);
                }

                // Add three more random descriptions for variety.
                while (rightColumnItems.Count < 7)
                {
                    var randomDescription = CallNumberDescriptions.Values.ElementAt(rand.Next(CallNumberDescriptions.Values.Count));
                    if (!rightColumnItems.Contains(randomDescription))
                    {
                        rightColumnItems.Add(randomDescription);
                    }
                }
            }
            else
            {
                // When descriptions are in the left column.
                while (leftColumnItems.Count < 4)
                {
                    var randomDescription = CallNumberDescriptions.Values.ElementAt(rand.Next(CallNumberDescriptions.Values.Count));
                    if (!leftColumnItems.Contains(randomDescription))
                    {
                        leftColumnItems.Add(randomDescription);
                    }
                }

                foreach (var description in leftColumnItems)
                {
                    var baseKey = CallNumberDescriptions.FirstOrDefault(x => x.Value == description).Key;
                    var randomOffset = rand.Next(0, 100);
                    var randomCallNumber = int.Parse(baseKey) + randomOffset;
                    var randomCallNumberStr = randomCallNumber.ToString("D3");
                    rightColumnItems.Add(randomCallNumberStr);
                }

                // Add three incorrect call numbers for variety.
                var incorrectCount = 0;
                while (incorrectCount < 3)
                {
                    var randomBase = baseNumbers[rand.Next(baseNumbers.Count)];
                    var randomOffset = rand.Next(0, 100);
                    var randomCallNumber = randomBase + randomOffset;
                    var randomCallNumberStr = randomCallNumber.ToString("D3");

                    var baseKeyForRandom = (randomCallNumber / 100 * 100).ToString("D3");
                    var descriptionForRandom = CallNumberDescriptions[baseKeyForRandom];
                    if (!leftColumnItems.Contains(descriptionForRandom))
                    {
                        if (!rightColumnItems.Contains(randomCallNumberStr))
                        {
                            rightColumnItems.Add(randomCallNumberStr);
                            incorrectCount++;
                        }
                    }
                }
            }

            // Return shuffled right column items.
            return Tuple.Create(leftColumnItems, rightColumnItems.OrderBy(_ => rand.Next()).ToList());
        }
    }
}
