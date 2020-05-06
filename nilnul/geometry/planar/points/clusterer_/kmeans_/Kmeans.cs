using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.geometry.planar.points.clusterer_.kmeans_
{
	public class Dichonotomy
	{
		private nilnul.geometry.planar.point.SetDbl _points;

		public nilnul.geometry.planar.point.SetDbl points
		{
			get { return _points; }
			set { _points = value; }
		}


		//private nilnul.geometry.planar.point.SetDbl _initials;

		//public nilnul.geometry.planar.point.SetDbl initials
		//{
		//	get { return _initials; }
		//	set { _initials = value; }
		//}


		private nilnul.num_.Plural1 _count= new num_.Plural1(2);

		///// <summary>
		///// if at initial, all points are nearer to one cluster and leave the other cluster empty, then the avg of the other cluster will throw exception. grab initial centers from the points themselves can avoid the issue in that all cluster will be populated, and will remain populated in that if one cluster is empty, you can get one from another cluster to reduce the overall distances.
		///// </summary>
		///// <param name="points1"></param>
		///// <param name="initials__"></param>
		//public Dichonotomy(nilnul.geometry.planar.point.SetDbl points1 , nilnul.geometry.planar.point.SetDbl  initials__ ) 
		//{


		//	new nilnul.obj_.bit.vow_.true_.xpN_.OfThis("clusters.Count must be no less than points.Count ").vow(
				
		//		points1.Count >= _count.en
		//	); ;

		//	new nilnul.obj_.bit.vow_.true_.xpN_.OfThis("intials.Count must be equal to clusters.Count").vow(
				
		//		initials__.Count == _count.en
		//	); ;

		//	_points = (points1);
		//	_initials = initials__;

		//}
		public Dichonotomy(point.SetDbl points1)
			
		{
			new nilnul.obj_.bit.vow_.true_.xpN_.OfThis("clusters.Count must be no less than points.Count ").vow(

				points1.Count >= _count.en
			); ;

			
			_points = (points1);

		}
		public Dichonotomy(List<PointDbl> points1)
			:this(new point.SetDbl(points1))
		{


		}

		public nilnul.num_.Plural1 count
		{
			get { return _count; }
			set { _count = value; }
		}

		public (IEnumerable<int>,IEnumerable<int> ) mine() {

			var pointsStr = points.AsEnumerable();
			var center0 = pointsStr.ElementAt(0);
			var center1 = pointsStr.ElementAt(1);

			var distance4center0 = pointsStr.Select(p => nilnul.geometry.planar.span._DistanceX.Distance(p, center0)).ToArray();

			var distance4center1 = pointsStr.Select(p => nilnul.geometry.planar.span._DistanceX.Distance(p, center1)).ToArray();
			var indexes = Enumerable.Range(0, pointsStr.Count());// distance4center0.Select((e, i) => i).ToArray();

			var indexes0 = indexes.Where(i => distance4center0[i] <= distance4center1[i]);
			var indexes1 = indexes.Except(indexes0);

			var newCenter0 = nilnul.geometry.planar.points_.started._CenterX._Center_assumeStarted(
				pointsStr.Where((e, i) => indexes0.Contains(i))
			);
			var newCenter1 = nilnul.geometry.planar.points_.started._CenterX._Center_assumeStarted(
				pointsStr.Where((e, i) => indexes1.Contains(i))
			);

			while ((newCenter0, newCenter1) != (center0, center1))
			{
				center0 = newCenter0;
				center1 = newCenter1;
				distance4center0 = pointsStr.Select(p => nilnul.geometry.planar.span._DistanceX.Distance(p, center0)).ToArray();

				distance4center1 = pointsStr.Select(p => nilnul.geometry.planar.span._DistanceX.Distance(p, center1)).ToArray();

				indexes0 = indexes.Where(i => distance4center0[i] <= distance4center1[i]);
				indexes1 = indexes.Except(indexes0);

				newCenter0 = nilnul.geometry.planar.points_.started._CenterX._Center_assumeStarted(
				   pointsStr.Where((e, i) => indexes0.Contains(i))
			   );
				newCenter1 = nilnul.geometry.planar.points_.started._CenterX._Center_assumeStarted(
				   pointsStr.Where((e, i) => indexes1.Contains(i))
			   );
			}

			return (indexes0,indexes1);
		}


	}
}
