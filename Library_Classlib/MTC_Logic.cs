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

        public bool IsCorrectMatch(string key, string value)
        {
            // If it's a call number
            if (CallNumberDescriptions.ContainsKey(key))
            {
                return CallNumberDescriptions[key] == value;
            }
            // If it's a description
            else if (CallNumberDescriptions.ContainsValue(key))
            {
                return CallNumberDescriptions.FirstOrDefault(x => x.Value == key).Key == value;
            }
            return false;
        }



        public KeyValuePair<string, string> GetRandomEntry()
        {
            return CallNumberDescriptions.ElementAt(rand.Next(0, CallNumberDescriptions.Count));
        }

        public void AddCallNumberAndDescription(string callNumber, string description)
        {
            if (!CallNumberDescriptions.ContainsKey(callNumber))
            {
                CallNumberDescriptions[callNumber] = description;
            }
            else
            {
                throw new ArgumentException("Call number already exists in the library.");
            }
        }

        public Tuple<List<string>, List<string>> GenerateQuestion(bool isCallNumberToDescription)
        {
            var allKeys = CallNumberDescriptions.Keys.ToList();
            var allValues = CallNumberDescriptions.Values.ToList();

            var leftColumnItems = new List<string>();
            var rightColumnItems = new List<string>();

            // If the flag is set to true, the left column will have call numbers, otherwise descriptions.
            var poolForLeftColumn = isCallNumberToDescription ? allKeys : allValues;

            // Randomly select 4 unique items for the left column
            while (leftColumnItems.Count < 4)
            {
                var randomItem = poolForLeftColumn[rand.Next(poolForLeftColumn.Count)];
                if (!leftColumnItems.Contains(randomItem))
                {
                    leftColumnItems.Add(randomItem);
                }
            }

            // Add the corresponding items from the dictionary to the right column
            foreach (var item in leftColumnItems)
            {
                if (isCallNumberToDescription)
                    rightColumnItems.Add(CallNumberDescriptions[item]);
                else
                    rightColumnItems.Add(CallNumberDescriptions.FirstOrDefault(x => x.Value == item).Key);
            }

            var remainingItemsForRightColumn = isCallNumberToDescription ? allValues.Except(rightColumnItems).ToList() : allKeys.Except(leftColumnItems).ToList();

            // Add 3 random incorrect items to the right column from the remaining items
            while (rightColumnItems.Count < 7)
            {
                var randomItem = remainingItemsForRightColumn[rand.Next(remainingItemsForRightColumn.Count)];
                if (!rightColumnItems.Contains(randomItem))
                {
                    rightColumnItems.Add(randomItem);
                    remainingItemsForRightColumn.Remove(randomItem);
                }
            }

            return Tuple.Create(leftColumnItems, rightColumnItems.OrderBy(_ => rand.Next()).ToList());
        }
    }
}
