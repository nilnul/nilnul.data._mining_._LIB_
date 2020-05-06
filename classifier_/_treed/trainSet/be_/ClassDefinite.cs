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

namespace nilnul.data.mining.classifier_._treed.trainSet.be_
{
	static public class _ClassDefiniteX
	{
		public static  bool _Be_assumeLastColClass(DataTable data)
		{
			var str = nilnul.data.tbl.col.cels._ValsX._Txts_assumeIndexInRange(data, data.Columns.Count - 1);
			var first = str.First();
			return nilnul.obj.seq_.headed._TailX._Tail(str).All(
				x => nilnul.txt.nulable.Eq.Singleton.Equals(x,first)
			);
		}
	}
}
