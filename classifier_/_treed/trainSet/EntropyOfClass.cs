using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.data.mining.classifier_._treed.trainSet
{
	static public class _EntropyOfClassX
	{
		public static double Entropy(DataTable data)
		{
			//var totalRows = data.Rows.Count;
			//var amountForDifferentValue = _TrainSetX._GetDistByVal(data, data.Columns.Count - 1);
			var amountForDifferentValue1 = _TrainSetX._GetDistOfClassifier(data);

			

			//var stepsForCalculation = amountForDifferentValue
			//	.Select(item => item[0, 0] / (double)totalRows)
			//	.Select(division => -division * Math.Log(division, 2))
			//	.ToList();



			//var r= stepsForCalculation.Sum();

			var r1 = nilnul.stat.dist_.finite._EntropyX.Entropy_ofAssumeDistribution(amountForDifferentValue1.ToArray());
			return r1;
		}
	}
}
