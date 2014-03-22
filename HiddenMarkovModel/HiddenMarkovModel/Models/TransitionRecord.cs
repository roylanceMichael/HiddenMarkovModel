using System;

namespace HiddenMarkovModel
{
	internal class TransitionRecord : KeyRecord
	{
		internal TransitionRecord (string fromTransition, string toTransition, double logProbability)
			: base(fromTransition, toTransition, logProbability)
		{
		}

		public string FromTransition { get { return this.Key; } }

		public string ToTransition { get { return this.Value; } }
	}
}

