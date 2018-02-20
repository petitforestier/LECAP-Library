using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Tools.Extensions;

namespace Library.Tools.Tree
{
	public static class TreeHelper
	{
		#region Public METHODS

		/// <summary>
		/// Retourne un tree depuis une liste de hiérarchie
		/// </summary>
		public static TreeNode GetCatalogTree(List<string> iList, string iSeparator, string iRootName)
		{
			if (iList.IsNullOrEmpty()) throw new ArgumentNullException();

			iList = iList.OrderBy(x => x).ToList();
			iList = iList.GetWithoutDuplicates(x => x).ToList();

			//Création des catégories en TreeNode root et leaf

			List<String> fullList = new List<string>();
			//Bouclage sur les chemins
			foreach (string categoryPathItem in iList.Enum())
			{
				if (categoryPathItem == null) continue;

				List<string> categoryList = categoryPathItem.Split(iSeparator).ToList();
				string parentCategory = null;

				//Bouclage sur les catégories du chemin
				foreach (string categoryItem in categoryList.Enum())
				{
					string categoryPath = (parentCategory != null) ? parentCategory + iSeparator + categoryItem : categoryItem;
					if (!fullList.Contains(categoryPath))
					{
						fullList.Add(categoryPath);
					}
					parentCategory = categoryPath;
				}
			}

			TreeNode rootTreeNode = new TreeNode();
			rootTreeNode.Text = iRootName;

			//Bouclage sur la liste
			foreach (string pathItem in fullList.Enum())
			{
				TreeNode parentNode = null;
				if (!pathItem.Contains(iSeparator))
				{
					parentNode = rootTreeNode;
				}
				else
				{
					string parentPath = pathItem.RemoveLastOccurence(iSeparator);
					parentNode = rootTreeNode.FlattenTree().Where(x => x.Tag.ToString() == parentPath).Single();
				}

				var newTreeNode = new TreeNode();
				newTreeNode.Tag = pathItem;
				newTreeNode.Text = pathItem.Split(iSeparator).Last();
				parentNode.Nodes.Add(newTreeNode);
			}
			return rootTreeNode;
		}

		#endregion
	}
}