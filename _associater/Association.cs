using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.data.mining._associater
{
	public class Association<T>
		:nilnul._set_.MemberEqA_ovEl<T>
	{
		private IEnumerable<T> _precedent;

		public IEnumerable<T> precedent
		{
			get { return _precedent; }
			set { _precedent = value; }
		}

		private IEnumerable<T> _postedent;

		public IEnumerable<T> postedent
		{
			get { return _postedent; }
			set { _postedent = value; }
		}



		public Association(IEnumerable<T> precedent, IEnumerable<T> postedent,IEqualityComparer<T> elEq):base(elEq)
		{
			_precedent = precedent;
			_postedent = postedent;
		}


		public Association(IEnumerable<T> precedent, IEnumerable<T> postedent):this(
			precedent
			,
			postedent
			,
			EqualityComparer<T>.Default
		)
		{

		}

		public override string ToString()
		{
			return $"{nilnul.set._PhraseX.Phrase(_precedent,memberEq)} -> {nilnul.set._PhraseX.Phrase(postedent,memberEq)}";
		}


	}
}
