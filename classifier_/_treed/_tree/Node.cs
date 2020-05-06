using nilnul.data.mining.classifier_._treed._tree._node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.data.mining.classifier_._treed._tree
{

	public class TreeNode
	{
		public TreeNode(string name, int tableIndex, MyAttribute nodeAttribute, string inEdge)
		{
			Name = name;
			TableIndex = tableIndex;
			NodeAttribute = nodeAttribute;
			ChildNodes = new List<TreeNode>();
			Edge = inEdge;
		}

		public TreeNode(bool isleaf, string name, string edge)
		{
			IsLeaf = isleaf;
			Name = name;
			Edge = edge;
			ChildNodes = new List<TreeNode>();
		}
		public override string ToString()
		{
			return this.Name;
		}
		public string Name { get; }

		/// <summary>
		/// 
		/// </summary>
		public string Edge { get; }

		public MyAttribute NodeAttribute { get; }

		public List<TreeNode> ChildNodes { get; }

		public int TableIndex { get; }

		public bool IsLeaf { get; }
	}
}