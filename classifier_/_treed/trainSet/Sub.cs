using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.data.mining.classifier_._treed.trainSet
{
	static public class _SubX
	{
		/// <summary>
		/// create a subtable by removing:
		///		rows that the val of the extracted attr is given
		///		the col
		/// </summary>
		/// <param name="data"></param>
		/// <param name="edgePointingToNextNode"></param>
		/// <param name="colIndex"></param>
		/// <returns></returns>
		public static DataTable CreateSmallerTable(DataTable data, string edgePointingToNextNode, int colIndex)
		{
			var smallerData = new DataTable();

			// add column titles
			for (var i = 0; i < data.Columns.Count; i++)
			{
				smallerData.Columns.Add(data.Columns[i].ToString());
			}

			// add rows which contain edgePointingToNextNode to new datatable
			for (var i = 0; i < data.Rows.Count; i++)
			{
				if (data.Rows[i][colIndex].ToString().Equals(edgePointingToNextNode))
				{
					var row = new string[data.Columns.Count];

					for (var j = 0; j < data.Columns.Count; j++)
					{
						row[j] = data.Rows[i][j].ToString();
					}

					smallerData.Rows.Add(row);
				}
			}

			// remove column which was already used as node            
			smallerData.Columns.Remove(smallerData.Columns[colIndex]);

			return smallerData;
		}
	}
}
