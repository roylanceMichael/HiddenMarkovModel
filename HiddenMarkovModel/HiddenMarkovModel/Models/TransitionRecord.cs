using System;

namespace HiddenMarkovModel
{
	public class TransitionRecord : KeyRecord
	{
		public TransitionRecord (string fromTransition, string toTransition, double logProbability)
			: base(fromTransition, toTransition, logProbability)
		{
		}

		public string FromTransition { get { return this.Key; } }

		public string ToTransition { get { return this.Value; } }
	}
}

