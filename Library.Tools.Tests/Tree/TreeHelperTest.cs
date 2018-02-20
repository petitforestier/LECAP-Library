using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Library.Tools.Tests
{
	[TestClass]
	public class TreeHelperTest
	{
		[TestMethod]
        public void PrintTreeNodeText()
		{
			string separator = "//";

			var list = new List<string>();
			list.Add("1//2");
			list.Add("1//3");
			list.Add("1//2");
			list.Add("1//2//5");
			list.Add("1//2");
			list.Add("11//6");
			list.Add("11//6//8//9");

			TreeNode tree = Library.Tools.Tree.TreeHelper.GetCatalogTree(list, separator, "root");

			Library.Tools.Debug.MyDebug.PrintTreeNodeText(tree);

			if (tree.Text != "root") throw new Exception();
			if (tree.Nodes[0].Text != "1") throw new Exception();
			if (tree.Nodes[1].Text != "11") throw new Exception();
			if (tree.Nodes.Count != 2) throw new Exception();

			if (tree.Nodes[0].Nodes.Count != 2) throw new Exception();
			if (tree.Nodes[0].Nodes[0].Text != "2") throw new Exception();
			if (tree.Nodes[0].Nodes[1].Text != "3") throw new Exception();
			
		}
	}
}
