using System;

namespace HiddenMarkovModel
{
	public class TransitionRecord
	{
		public TransitionRecord (string fromTransition, string toTransition)
		{
			fromTransition.CheckWhetherArgumentIsNull ("fromTransition");
			toTransition.CheckWhetherArgumentIsNull ("toTransition");

			this.FromTransition = fromTransition;
			this.ToTransition = toTransition;
		}

		public string FromTransition { get; private set; }

		public string ToTransition { get; private set; }
	}
}

