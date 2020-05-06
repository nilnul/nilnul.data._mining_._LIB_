using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace nilnul.data.mining.classifier_._treed._tree._node
{
	public class MyAttribute
	{
		public MyAttribute(string name, List<string> distinctValues)
		{
			Name = name;
			DifferentAttributeNames = distinctValues;
		}

		public string Name { get; }

		/// <summary>
		/// distinct values
		/// </summary>
		public List<string> DifferentAttributeNames { get; }

		public double InformationGain { get; set; }

		/// <summary>
		/// get the cells/values
		/// </summary>
		/// <param name="data"></param>
		/// <param name="columnIndex"></param>
		/// <returns></returns>
		public static List<string> GetDifferentAttributeNamesOfColumn(DataTable data, int columnIndex)
		{
			var differentAttributes = new List<string>();

			for (var i = 0; i < data.Rows.Count; i++)
			{
				var found = differentAttributes.Any(t => t.ToUpper().Equals(data.Rows[i][columnIndex].ToString().ToUpper()));

				if (!found)
				{
					differentAttributes.Add(data.Rows[i][columnIndex].ToString());
				}
			}

			return differentAttributes;
		}
	}
}