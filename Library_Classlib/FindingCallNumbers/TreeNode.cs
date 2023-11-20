using System.Collections.Generic;

namespace Library_Classlib.FindingCallNumbers
{
    /// <summary>
    /// Represents a node in a Dewey Decimal Classification tree.
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// Gets or sets the call number of this tree node.
        /// </summary>
        /// <remarks>
        /// The call number is a unique identifier for each classification entry in the Dewey Decimal System.
        /// </remarks>
        public string CallNumber { get; set; }

        /// <summary>
        /// Gets or sets the description of this tree node.
        /// </summary>
        /// <remarks>
        /// The description provides a textual representation of what this node/classification represents.
        /// </remarks>
        public string Description { get; set; }

        /// <summary>
        /// Gets the children of this tree node.
        /// </summary>
        /// <remarks>
        /// Each child represents a further subdivision in the Dewey Decimal Classification hierarchy.
        /// </remarks>
        public Dictionary<string, TreeNode> Children { get; set; }

        /// <summary>
        /// Gets or sets the parent of this tree node.
        /// </summary>
        /// <remarks>
        /// The parent node represents the broader classification in which this node falls.
        /// </remarks>
        public TreeNode Parent { get; set; }

        /// <summary>
        /// Initializes a new instance of the TreeNode class.
        /// </summary>
        /// <remarks>
        /// The constructor initializes the Children dictionary to ensure that the TreeNode can have children nodes.
        /// </remarks>
        public TreeNode()
        {
            Children = new Dictionary<string, TreeNode>();
        }
    }
}
