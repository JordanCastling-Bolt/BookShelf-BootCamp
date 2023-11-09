using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library_Classlib.FindingCallNumbers
{
    public class DeweyDecimalTree
    {
        public TreeNode Root { get; set; }

        public DeweyDecimalTree()
        {
            Root = new TreeNode { CallNumber = "Root", Description = "Dewey Decimal Classification" };
        }

        public KeyValuePair<string, string> GetRandomSectionDescription()
        {
            var allSections = new List<KeyValuePair<string, string>>();

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

        // Method to find a section by its call number
        public TreeNode FindSectionByCallNumber(string sectionCallNumber)
        {
            // This needs to be adapted to your tree structure.
            // Assuming you have a way to directly find a section based on its call number.
            // This is a placeholder and needs to be implemented.
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


        public void BuildTree(JObject jsonData)
        {
            // Iterate over main classes
            foreach (var mainClassProp in jsonData["main_classes"].Children<JProperty>())
            {
                string mainClassKey = mainClassProp.Name;
                JObject mainClassValue = (JObject)mainClassProp.Value;

                var mainClassNode = new TreeNode
                {
                    CallNumber = mainClassKey,
                    Description = mainClassValue["description"].ToString(),
                    Parent = Root // Set the parent to Root for main classes
                };
                Root.Children.Add(mainClassKey, mainClassNode);

                // Iterate over divisions
                foreach (var divisionProp in mainClassValue["divisions"].Children<JProperty>())
                {
                    string divisionKey = divisionProp.Name;
                    JObject divisionValue = (JObject)divisionProp.Value;

                    var divisionNode = new TreeNode
                    {
                        CallNumber = divisionKey,
                        Description = divisionValue["description"].ToString(),
                        Parent = mainClassNode // Set the parent to the main class node
                    };
                    mainClassNode.Children.Add(divisionKey, divisionNode);

                    // Iterate over sections
                    if (divisionValue["sections"] != null)
                    {
                        foreach (var sectionProp in divisionValue["sections"].Children<JProperty>())
                        {
                            string sectionKey = sectionProp.Name;
                            JToken sectionValue = sectionProp.Value;

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
