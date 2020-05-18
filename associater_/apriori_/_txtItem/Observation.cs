using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.data.mining.associater_.apriori_._txtItem
{
	public class Observation : List<string>
	{
		public Observation()
		{
		}

		public Observation(int capacity) : base(capacity)
		{
		}

		public Observation(IEnumerable<string> collection) : base(collection)
		{
		}
	}
}
