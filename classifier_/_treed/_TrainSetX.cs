using nilnul.data.mining.classifier_._treed._tree;
using nilnul.data.mining.classifier_._treed._tree._node;
using nilnul.data.mining.classifier_._treed.trainSet;
using nilnul.stat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.data.mining.classifier_._treed
{
	static public class _TrainSetX
	{
		/// <summary>
		/// for each distinct value of the given col:
		///		get the subtable.
		///		compute the distribution of the classifier
		///	Note: if the given col is the classifier col, then:
		///		for each classifier val such as "Yes":
		///			we have a subtable that has zero columns
		///			the count of "Yes" is a number; the count of "No" s zero.
		///			we still get a distribution
		/// get :
		///		different values Count
		///		the positive classified for each val.
		///			note: the first val of the last col is called positive
		/// </summary>
		/// <param name="data">must have classifier columns (in trivia case, the table may contain only that classifier cols); must have rows. </param>
		/// <param name="indexOfColumnToCheck"></param>
		/// <returns></returns>
		public static List<int[,]> _GetDistByVal(DataTable data, int indexOfColumnToCheck)
		{
			var foundValues = new List<int[,]>();
			var knownValues = nilnul.data.tbl.col.cels.vals._DistinctX.Distinct(data, indexOfColumnToCheck);

			foreach (var item in knownValues)
			{
				var amount = 0;
				var positiveAmount = 0;

				for (var i = 0; i < data.Rows.Count; i++)
				{
					if (data.Rows[i][indexOfColumnToCheck].ToString().Equals(item))
					{
						amount++;

						// Counts the positive cases and adds the sum later to the array for the calculation
						if (data.Rows[i][data.Columns.Count - 1].ToString().Equals(data.Rows[0][data.Columns.Count - 1]))
						{
							positiveAmount++;
						}
					}
				}

				int[,] array = { { amount, positiveAmount } };
				foundValues.Add(array);
			}

			return foundValues;
		}

		public static Dictionary<string, nilnul.txt.Bag1> _GetClassStatByCandidateVal(DataTable data, int indexOfColumnToCheck)
		{

			var r = new Dictionary<string, nilnul.txt.Bag1>();



			for (var i = 0; i < data.Rows.Count; i++)
			{
				var k = data.Rows[i][indexOfColumnToCheck].ToString();
				if (!r.ContainsKey(k))
				{
					r.Add(k, new txt.Bag1());

				}
				r[k].add(data.Rows[i][data.Columns.Count - 1].ToString());

			}




			return r;
		}

		public static /*List<int[,]>*/ IEnumerable<double> _GetDistOfCol(DataTable data, int indexOfColumnToCheck)
		{
			//var knownValues = nilnul.data.tbl.col.cels.vals._DistinctX. Distinct(data, data.Columns.Count-1);
			var bag = new nilnul.txt.Bag1(
				nilnul.data.tbl.col.cels._ValsX._Txts_assumeIndexInRange(data, indexOfColumnToCheck)

			)
			;
			var dist = nilnul.stat.dist_.finite._FroOccursX._Dbls_assumeTotalPositive(bag, nilnul.txt.Comp.Singleton);
			return dist;
		}


		public static /*List<int[,]>*/ IEnumerable<double> _GetDistOfClassifier(DataTable data)
		{
			//var knownValues = nilnul.data.tbl.col.cels.vals._DistinctX. Distinct(data, data.Columns.Count-1);
			var bag = new nilnul.txt.Bag1(
				nilnul.data.tbl.col.cels._ValsX._Txts_assumeIndexInRange(data, data.Columns.Count - 1)

			)
			;
			var dist = nilnul.stat.dist_.finite._FroOccursX._Dbls_assumeTotalPositive(bag, nilnul.txt.Comp.Singleton);
			return dist;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="data">assume : classEntropy not nil, attr entropy is not nil</param>
		/// <param name="candidateColIndex"></param>
		/// <param name="entropyOfDataset">the entropy of the whole dataset with respect of the classifier</param>
		/// <returns></returns>
		public static double GetGainRatioForCol(DataTable data, int candidateColIndex, double entropyOfDataset)
		{
			var totalRows = data.Rows.Count;
			var amountForDifferentValue = _GetDistByVal(data, candidateColIndex);

			var candidate_andClassStat = _GetClassStatByCandidateVal(data, candidateColIndex);

			//var dist = _GetDistOfCol(data, colIndex);
			var candidateStat = new nilnul.txt.Bag1();
			candidate_andClassStat.Select(kv => new KeyValuePair<string, Num1>(kv.Key, kv.Value.cardinality)).ForEach(
				x => candidateStat.Add(x.Key, x.Value)

			);


			var candidateDistribution = nilnul.stat.dist_.finite._FroBagX._ProbInVowedDbl_assumeTotalPositive(
				candidateStat

			);



			var candidateEntropy = nilnul.stat.dist_.finite._EntropyX.Dbl_ofAssumeDistribution(
				candidateDistribution.Values.Cast<ProbDbl>()
			);

			var eachCandidate_with_ClassEntropy = candidate_andClassStat.Select(
				d => new KeyValuePair<string, double>(
					d.Key,

					nilnul.stat.dist_.finite._EntropyX.Entropy_ofAssumeDistribution(
						nilnul.stat.dist_.finite._FroBagX._ProbInDbl_assumeTotalPositive(
							d.Value
						).Values.Cast<double>()
					)
				)
			);
			var conditionalEntropyOfClassOnCandidate = eachCandidate_with_ClassEntropy.Select(
				candidate => candidateDistribution[candidate.Key]//prob of candidate
				*
				candidate.Value     //entropy
			).Sum();


			var stepsForCalculation = new List<double>();

			foreach (var item in amountForDifferentValue)
			{
				// helper for calculation
				var firstDivision = item[0, 1] / (double)item[0, 0];
				var secondDivision = (item[0, 0] - item[0, 1]) / (double)item[0, 0];

				// prevent dividedByZeroException
				if (firstDivision == 0 || secondDivision == 0)
				{
					stepsForCalculation.Add(0.0);
				}
				else
				{
					stepsForCalculation.Add(-firstDivision * Math.Log(firstDivision, 2) - secondDivision * Math.Log(secondDivision, 2));
				}
			}



			/// to change to ratio
			var gain = stepsForCalculation.Select((t, i) => amountForDifferentValue[i][0, 0] / (double)totalRows * t).Sum();



			gain = entropyOfDataset - gain;

			var gain1 = entropyOfDataset - conditionalEntropyOfClassOnCandidate;


			var gainRatio = gain1 / candidateEntropy;

			return gainRatio;


			return gain;
		}

		///// <summary>
		///// if it's leaf, add childNodes
		///// </summary>
		///// <param name="root"></param>
		///// <param name="data">current table;  not necessaryly the original table</param>
		///// <param name="valToCheck"></param>
		///// <returns></returns>
		//private static bool CheckIfIsLeaf_sealIfLeaf(DataTable data, TreeNode root, string valToCheck)
		//{
		//	var isLeaf = true;
		//	var allEndValues = new List<string>();

		//	// get all leaf values for the attribute in question
		//	for (var i = 0; i < data.Rows.Count; i++)
		//	{
		//		if (data.Rows[i][root.TableIndex].ToString().Equals(valToCheck))
		//		{
		//			allEndValues.Add(data.Rows[i][data.Columns.Count - 1].ToString());
		//		}
		//	}

		//	// check whether all elements of the list have the same value
		//	if (allEndValues.Count > 0 && allEndValues.Any(x => x != allEndValues[0]))
		//	{
		//		isLeaf = false;
		//	}

		//	// create leaf with value to display and edge to the leaf
		//	if (isLeaf)
		//	{
		//		root.ChildNodes.Add(new TreeNode(true, allEndValues[0], valToCheck));
		//	}

		//	return isLeaf;
		//}
		private static TreeNode __GetRootNodeFroClass_tblAssumeDwelt(DataTable data, string inEdge)
		{


			var lastIndex = data.Columns.Count - 1;
			return new TreeNode(
				data.Columns[data.Columns.Count - 1].ColumnName
				, lastIndex
				,
				new MyAttribute(
					data.Columns[lastIndex].ToString()
					,
					MyAttribute.GetDifferentAttributeNamesOfColumn(data, lastIndex)
				),
				inEdge
			);




		}
		private static TreeNode GetRootNode_tblAssumeDefiniteAttrsPurged(DataTable data, string inEdge)
		{
			///if there is only one col: the class .
			///
			if (data.Columns.Count == 1)
			{
				return __GetRootNodeFroClass_tblAssumeDwelt(data, inEdge);
			}

			var attributes = new List<MyAttribute>();
			var highestInformationGainIndex = -1;
			var highestInformationGain = double.MinValue;

			// Get all names, amount of attributes and attributes for every column             
			for (var i = 0; i < data.Columns.Count - 1; i++)
			{
				var differentAttributenames = MyAttribute.GetDifferentAttributeNamesOfColumn(data, i);
				attributes.Add(new MyAttribute(data.Columns[i].ToString(), differentAttributenames));
			}

			// Calculate Entropy (S)
			var tableEntropy = _treed.trainSet._EntropyOfClassX.Entropy(data);

			for (var i = 0; i < attributes.Count; i++)
			{
				attributes[i].InformationGain = _treed._TrainSetX.GetGainRatioForCol(data, i, tableEntropy);

				if (attributes[i].InformationGain > highestInformationGain)
				{
					highestInformationGain = attributes[i].InformationGain;
					highestInformationGainIndex = i;
				}
			}
			/// todo : gainRatio might be all nils
			///
			if (highestInformationGain == 0)
			{
				return __GetRootNodeFroClass_tblAssumeDwelt(data, inEdge);
			}

			return new TreeNode(attributes[highestInformationGainIndex].Name, highestInformationGainIndex, attributes[highestInformationGainIndex], inEdge);
		}

		/// <summary>
		/// edge is the val of the col
		/// </summary>
		/// <param name="data"></param>
		/// <param name="inEdge"></param>
		/// <returns></returns>
		private static TreeNode GetRootNode(DataTable data, string inEdge)
		{

			if (trainSet.be_._ClassDefiniteX._Be_assumeLastColClass(data))
			{
				return __GetRootNodeFroClass_tblAssumeDwelt(data, inEdge);
			}

			trainSet.act_._PurgeDefiniteAttrsX._PurgeDefiniteAttrs(data);


			return GetRootNode_tblAssumeDefiniteAttrsPurged(data, inEdge);
		}

		static public int leaf = 0;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataSubTable">assume dwelt</param>
		/// <param name="inEdgeName"></param>
		/// <returns></returns>
		public static TreeNode _Learn(DataTable dataSubTable, string inEdgeName)
		{

			var root = GetRootNode(dataSubTable, inEdgeName);

			if (root.TableIndex < dataSubTable.Columns.Count - 1)
			{
				foreach (var item in root.NodeAttribute.DifferentAttributeNames)
				{
					var reducedTable = _SubX.CreateSmallerTable(dataSubTable, item, root.TableIndex);
					root.ChildNodes.Add(_Learn(reducedTable, item));

				}
			}
			else
			{
				
				foreach (var item in root.NodeAttribute.DifferentAttributeNames)
				{
					var c = new TreeNode(true, (leaf++).ToString(), item);
					root.ChildNodes.Add(
						c
					);

				}
				

				
			}
			return root;

			//foreach (var item in root.NodeAttribute.DifferentAttributeNames)
			//{
			//	// if a leaf, leaf will be added in this method
			//	var isLeaf = CheckIfIsLeaf_sealIfLeaf(dataSubTable, root, item);

			//	// make a recursive call as long as the node is not a leaf
			//	if (!isLeaf)
			//	{
			//		var reducedTable = _SubX.CreateSmallerTable(dataSubTable, item, root.TableIndex);

			//		root.ChildNodes.Add(_Learn(reducedTable, item));
			//	}
			//}

			//return root;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataSubTable">assume Dwelt</param>
		/// <returns></returns>
		public static TreeNode Learn(DataTable dataSubTable)
		{
			return _Learn(new _treed.TrainSet(dataSubTable).ee, string.Empty);
		}


	}
}
