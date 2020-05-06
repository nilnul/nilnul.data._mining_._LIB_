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

namespace nilnul.data.mining.classifier_._treed.trainSet.act_
{
	static public class _PurgeDefiniteAttrsX
	{
	

		public static bool _IsDefinite(DataTable data, int indexOfColumnToCheck)
		{
			var str = nilnul.data.tbl.col.cels._ValsX._Txts_assumeIndexInRange(data, indexOfColumnToCheck);
			var first = str.First();
			return nilnul.obj.seq_.headed._TailX._Tail(str).All(
				x => nilnul.txt.nulable.Eq.Singleton.Equals(x, first)
			);

		}

		public static int[] _DefiniteCols( DataTable data)
		{
			return Enumerable.Range(0, data.Columns.Count).Where(x=> _IsDefinite(data,x)  ).ToArray();
			

		}



		public static void _PurgeDefiniteAttrs( DataTable data)
		{
			var cols2del = _DefiniteCols(data);
			foreach (var item in cols2del.Reverse())
			{
				data.Columns.RemoveAt(item);

			}
		}
	}
}
