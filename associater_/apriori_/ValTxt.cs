using nilnul.data.mining.associater_.apriori_._txtItem;
using nilnul.obj.seq;
using nilnul.obj.str_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.data.mining.associater_.apriori_
{
	public class TxtItem
	{

		double _support;
		/// <summary>
		/// alias:confidence
		/// </summary>
		double _confidence;

	//	IEnumerable< Observation> samples;

		public TxtItem(double support, double trust//, Observation[] samples
		)
		{
			this._support = support;
			this._confidence = trust;
			//this.samples = samples;
		}
		public IEnumerable<(nilnul.data.mining._associater.Association<string>, double)> getRules(
			IEnumerable<IEnumerable<string>> observations
		)
		{
			return getRules(observations.Select(s=> new Observation(s) ));
		}

		public IEnumerable<(nilnul.data.mining._associater.Association<string>, double)> getRules(
			IEnumerable< Observation> observations
		)
		{
			var minSupport =(observations.Count() * _support);

			var itemCountS = new nilnul.txt.Bag1(
				observations.SelectMany(s => s)
			);

			var supportedItems = new nilnul.txt.Bag1(
				itemCountS.Where(x => (double) x.Value.en >= minSupport)
			);

			var frequentItemSetS = new nilnul.obj.Bag1<IEnumerable<string>>(
				new NotNull2<IEqualityComparer<IEnumerable<string>>>(
					new nilnul.obj.str_.seq.Eq<string>()
				)
			);

			supportedItems.Each(
				component =>
				{
					frequentItemSetS.add(
						new[] { component.Key }
					);
				}
			);

			var itemSetCardinality = 1;

			while (true)
			{

				var itemsInConsideration = new nilnul.txt.Set(frequentItemSetS.Keys.SelectMany(x => x));

				var newFreqItemSets = new nilnul.obj.Bag1<IEnumerable<string>>(
					new NotNull2<IEqualityComparer<IEnumerable<string>>>(
						new nilnul.obj.str_.seq.Eq<string>()
					)
				);

				itemSetCardinality++;

				observations.Each(
					observation =>
					{
						var intersected = nilnul.set.op_.binary_._IntersectX.Intersect(
								itemsInConsideration
								,
								observation
							);

						var combinated = nilnul.set.family.op_.of_.set_.combinate_._ByIndexsX._Cord_assumeDistinct(
							intersected,
							(itemSetCardinality)
						);

						combinated.Each(
							combinatedInstance =>
							newFreqItemSets.add(
									combinatedInstance
							)
						);
					}
				);
				newFreqItemSets.removeKeys_ofFinite(
					newFreqItemSets.Where(x => (double) x.Value.en < minSupport).Select(y => y.Key).ToArray()
				);

				if (newFreqItemSets.None())
				{
					///The algorithm gets terminated when the frequent itemsets cannot be extended further.
					break;
				}
				else
				{
					frequentItemSetS = newFreqItemSets;
				}
			}

			var rules = new List<(nilnul.data.mining._associater.Association<string>, double)>();
			///now we get the frequent itemSetS.
			///to extract rules from each set.
			///
			foreach (var frequentSet in frequentItemSetS)
			{
				for (int i = 1 /*0*/; i </*=*/ frequentSet.Key.Count(); i++)
				{
					foreach (
						var combinated in nilnul.set.family.op_.of_.set_.combinate_._ByIndexsX._Cord_assumeDistinct(
							frequentSet.Key
							,
							i
						)
					)
					{
						var complement =
							frequentSet.Key.Except(combinated)
						;
						rules.Add(
							(
								new mining._associater.Association<string>(
									combinated
									,
									complement
								)
								,
								nilnul.stat.dist_.finite_.multivar_.binary.observation.str._ConfidenceX.Confidence(
									observations.Select(s => new HashSet<string>(s))
									,
									combinated,
									complement
								)
							)
						);
					}
				}
			}
			///now we get the ruleGrpS
			///
			return rules.Where(x => x.Item2 >= this._confidence);
		}
	}
}