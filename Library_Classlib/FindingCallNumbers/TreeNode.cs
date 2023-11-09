using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Classlib.FindingCallNumbers
{
    public class TreeNode
    {
        public string CallNumber { get; set; }
        public string Description { get; set; }
        public Dictionary<string, TreeNode> Children { get; set; }
        public TreeNode Parent { get; set; } 


        public TreeNode()
        {
            Children = new Dictionary<string, TreeNode>();
        }
    }
}