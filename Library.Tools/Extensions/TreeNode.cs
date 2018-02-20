using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Tools.Extensions
{
	public static class MyTreeNode
	{
		public static IEnumerable<TreeNode> FlattenTree(this TreeNode iTreeNode)
		{
			return FlattenTree(iTreeNode.Nodes);
		}

		public static IEnumerable<TreeNode> FlattenTree(this TreeNodeCollection iTreeNodeCollection)
		{
			return iTreeNodeCollection.Cast<TreeNode>()
						.Concat(iTreeNodeCollection.Cast<TreeNode>()
									.SelectMany(x => FlattenTree(x.Nodes)));
		}
	}
}
