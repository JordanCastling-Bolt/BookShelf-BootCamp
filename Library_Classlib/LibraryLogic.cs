using System;
using System.Collections.Generic;
using System.Linq;

namespace Library_Classlib
{
    public class LibraryLogic
    {
        public Dictionary<string, string> CallNumberDescriptions { get; private set; }

        private Random rand = new Random();

        public LibraryLogic()
        {
            CallNumberDescriptions = new Dictionary<string, string>
    {
        { "100", "Philosophy and psychology" },
        { "200", "Religion" },
        { "300", "Social sciences" },
        { "000", "Computer science, general works, and information" },
        { "400", "Language" },
        { "500", "Natural sciences and mathematics" },
        { "600", "Technology" },
        { "700", "Arts and recreation" },
        { "800", "Literature" },
        { "900", "History and geography" },
    };
        }

        public bool IsCorrectMatch(string callNumber, string description)
        {
            if (CallNumberDescriptions.TryGetValue(callNumber, out string value))
            {
                return value == description;
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

            var selectedKeys = new List<string>();
            var selectedValues = new List<string>();

            while (selectedKeys.Count < 4)
            {
                var randomEntry = GetRandomEntry();
                if (!selectedKeys.Contains(randomEntry.Key))
                {
                    selectedKeys.Add(randomEntry.Key);
                    selectedValues.Add(randomEntry.Value);
                }
            }

            while (selectedValues.Count < 7)
            {
                var randomDescription = allValues[rand.Next(allValues.Count)];
                if (!selectedValues.Contains(randomDescription))
                {
                    selectedValues.Add(randomDescription);
                }
            }

            return isCallNumberToDescription
                ? Tuple.Create(selectedKeys, selectedValues.OrderBy(_ => rand.Next()).ToList())
                : Tuple.Create(selectedValues, selectedKeys.OrderBy(_ => rand.Next()).ToList());
        }
    }
}
