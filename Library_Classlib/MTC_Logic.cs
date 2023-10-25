using System;
using System.Collections.Generic;
using System.Linq;

namespace Library_Classlib
{
    public class MTC_Logic
    {
        public Dictionary<string, string> CallNumberDescriptions { get; private set; }

        private readonly Random rand = new Random();

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

        public bool IsCorrectMatch(string left, string right)
        {
            string baseKey;

            // If left can be parsed as an integer (indicating that it is a call number)
            if (int.TryParse(left, out int numericKey))
            {
                baseKey = (numericKey / 100 * 100).ToString("D3");
                return CallNumberDescriptions.ContainsKey(baseKey) && CallNumberDescriptions[baseKey] == right;
            }

            // If right can be parsed as an integer (indicating that it is a call number)
            else if (int.TryParse(right, out int numericValue))
            {
                var descriptionMatchKey = CallNumberDescriptions.FirstOrDefault(x => x.Value == left).Key;

                // Check if the call number falls within the same range as the description
                baseKey = (numericValue / 100 * 100).ToString("D3");
                return descriptionMatchKey == baseKey;
            }

            // Both left and right are descriptions (unlikely but possible)
            else
            {
                return CallNumberDescriptions.ContainsValue(left) && CallNumberDescriptions.ContainsValue(right) && left == right;
            }
        }

        public Tuple<List<string>, List<string>> GenerateQuestion(bool isCallNumberToDescription)
        {
            var leftColumnItems = new List<string>();
            var rightColumnItems = new List<string>();

            var baseNumbers = Enumerable.Range(0, 10).Select(x => x * 100).ToList();
            var poolForLeftColumn = new List<string>();

            if (isCallNumberToDescription)
            {
                // When call numbers are in the left column
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
                // When descriptions are in the left column
                while (leftColumnItems.Count < 4)
                {
                    var randomDescription = CallNumberDescriptions.Values.ElementAt(rand.Next(CallNumberDescriptions.Values.Count));
                    if (!leftColumnItems.Contains(randomDescription))
                    {
                        leftColumnItems.Add(randomDescription);
                    }
                }

                // Add 4 corresponding call numbers
                foreach (var description in leftColumnItems)
                {
                    var baseKey = CallNumberDescriptions.FirstOrDefault(x => x.Value == description).Key;
                    var randomOffset = rand.Next(0, 100);
                    var randomCallNumber = int.Parse(baseKey) + randomOffset;
                    var randomCallNumberStr = randomCallNumber.ToString("D3");
                    rightColumnItems.Add(randomCallNumberStr);
                }

                // Add 3 incorrect call numbers
                var incorrectCount = 0;
                while (incorrectCount < 3)
                {
                    var randomBase = baseNumbers[rand.Next(baseNumbers.Count)];
                    var randomOffset = rand.Next(0, 100);
                    var randomCallNumber = randomBase + randomOffset;
                    var randomCallNumberStr = randomCallNumber.ToString("D3");

                    // Check if this number does not have a corresponding description in leftColumnItems
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

            return Tuple.Create(leftColumnItems, rightColumnItems.OrderBy(_ => rand.Next()).ToList());
        }
    }
}
