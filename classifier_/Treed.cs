using nilnul.data.mining.classifier_._treed._tree;
using nilnul.data.mining.classifier_._treed._tree._node;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.data.mining.classifier_
{
	/// <summary>
	/// given a table, return a decision tree
	/// </summary>
	static public class _TreedX
	{

		static public TreeNode GenerateTree(DataTable dataTable)
		{
			return _treed._TrainSetX.Learn(dataTable);
		}

		static public TreeNode GenerateTree_ofAddress(string semicolonAtEnd)
		{
			return _treed._TrainSetX.Learn(
				nilnul.blob_.csv_.cannonized._OnTblX.ImportFromCsvFile(semicolonAtEnd)
			);
		}

		static public TreeNode GenerateTree_ofBlob(string semicolonAtEnd)
		{
			return _treed._TrainSetX.Learn(
				nilnul.blob_.csv_.cannonized._OnTblX.ImportFromBlob(semicolonAtEnd)
			);
		}

		static public nilnul.rel_.net_.tree_.positioned_.builder_.ArcNamed<TreeNode, nilnul.obj.Eq3> _ToTree(TreeNode treeNode)
		{

			var root = treeNode;
			var children = treeNode.ChildNodes.Select(c => _ToTree(c));

			var dictMerged = new nilnul.obj.Dict<(TreeNode, TreeNode), string, nilnul.obj.co.eq_.Defaulted<TreeNode, nilnul.obj.Eq3>>();

			foreach (var item in children.Select(x => x.dict).SelectMany(d => d))
			{
				if (!dictMerged.ContainsKey(item.Key))
				{
					dictMerged.Add(item.Key, item.Value);

				}
			}
			foreach (var item in treeNode.ChildNodes)
			{
				if (!dictMerged.ContainsKey((root, item)))
				{
					dictMerged.Add(
		(root, item)
		, item.Edge
	);
				}
			}

			var r = new nilnul.rel_.net_.tree_.positioned_.builder_.ArcNamed<TreeNode, nilnul.obj.Eq3>(
				new rel_.net_.tree_.positioned_.Builder1<TreeNode, nilnul.obj.Eq3>(root, children.Select(c => c.boxed).ToList())
			)
			{ dict = dictMerged };
			return r;

		}




	}
}
