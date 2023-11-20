using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Library_Classlib.FindingCallNumbers
{
    /// <summary>
    /// Represents a tree structure for the Dewey Decimal Classification.
    /// </summary>
    public class DeweyDecimalTree
    {
        /// <summary>
        /// Root of the Dewey Decimal Tree.
        /// </summary>
        public TreeNode Root { get; set; }

        /// <summary>
        /// Constructs a DeweyDecimalTree with an initialized root node.
        /// </summary>
        public DeweyDecimalTree()
        {
            Root = new TreeNode { CallNumber = "Root", Description = "Dewey Decimal Classification" };
        }

        /// <summary>
        /// Gets a random section description from the Dewey Decimal Tree.
        /// </summary>
        /// <returns>A KeyValuePair containing the call number and description of a randomly selected section.</returns>
        public KeyValuePair<string, string> GetRandomSectionDescription()
        {
            var allSections = new List<KeyValuePair<string, string>>();

            // Traverse the tree to collect all section descriptions
            foreach (var mainClass in Root.Children.Values)
            {
                foreach (var division in mainClass.Children.Values)
                {
                    foreach (var section in division.Children.Values)
                    {
                        allSections.Add(new KeyValuePair<string, string>(section.CallNumber, section.Description));
                    }
                }
            }

            // Return a random section description
            Random random = new Random();
            return allSections[random.Next(allSections.Count)];
        }

        /// <summary>
        /// Finds a section in the Dewey Decimal Tree by its call number.
        /// </summary>
        /// <param name="sectionCallNumber">The call number of the section to find.</param>
        /// <returns>The TreeNode corresponding to the section, or null if not found.</returns>
        public TreeNode FindSectionByCallNumber(string sectionCallNumber)
        {
            // Search through each division for the specified section call number
            foreach (var mainClass in Root.Children.Values)
            {
                foreach (var division in mainClass.Children.Values)
                {
                    if (division.Children.TryGetValue(sectionCallNumber, out TreeNode sectionNode))
                    {
                        return sectionNode;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Builds the Dewey Decimal Tree from a given JSON object.
        /// </summary>
        /// <param name="jsonData">JSON object representing the Dewey Decimal Classification.</param>
        public void BuildTree(JObject jsonData)
        {
            // Iterate over main classes in the JSON data
            foreach (var mainClassProp in jsonData["main_classes"].Children<JProperty>())
            {
                string mainClassKey = mainClassProp.Name;
                JObject mainClassValue = (JObject)mainClassProp.Value;

                // Create a node for each main class and add it to the tree
                var mainClassNode = new TreeNode
                {
                    CallNumber = mainClassKey,
                    Description = mainClassValue["description"].ToString(),
                    Parent = Root // Set the parent to Root for main classes
                };
                Root.Children.Add(mainClassKey, mainClassNode);

                // Iterate over divisions within each main class
                foreach (var divisionProp in mainClassValue["divisions"].Children<JProperty>())
                {
                    string divisionKey = divisionProp.Name;
                    JObject divisionValue = (JObject)divisionProp.Value;

                    // Create a node for each division and add it to the main class node
                    var divisionNode = new TreeNode
                    {
                        CallNumber = divisionKey,
                        Description = divisionValue["description"].ToString(),
                        Parent = mainClassNode // Set the parent to the main class node
                    };
                    mainClassNode.Children.Add(divisionKey, divisionNode);

                    // Iterate over sections within each division
                    if (divisionValue["sections"] != null)
                    {
                        foreach (var sectionProp in divisionValue["sections"].Children<JProperty>())
                        {
                            string sectionKey = sectionProp.Name;
                            JToken sectionValue = sectionProp.Value;

                            // Create a node for each section and add it to the division node
                            var sectionNode = new TreeNode
                            {
                                CallNumber = sectionKey,
                                Description = sectionValue.ToString(),
                                Parent = divisionNode // Set the parent to the division node
                            };
                            divisionNode.Children.Add(sectionKey, sectionNode);
                        }
                    }
                }
            }
        }
    }
}
