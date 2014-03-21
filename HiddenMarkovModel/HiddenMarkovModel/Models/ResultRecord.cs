using System;

namespace HiddenMarkovModel
{
	public class ResultRecord
	{
		private readonly TransitionRecord transition;

		private readonly EmissionRecord emission;

		private readonly double logProbability;

		public ResultRecord (TransitionRecord transition, EmissionRecord emission)
		{
			transition.CheckWhetherArgumentIsNull ("transition");
			emission.CheckWhetherArgumentIsNull ("emission");

			this.transition = transition;
			this.emission = emission;

			this.logProbability = this.transition.LogProbability + this.emission.LogProbability;
		}

		public TransitionRecord Transition {
			get {
				return this.transition;
			}
		}

		public EmissionRecord Emission {
			get {
				return this.emission;
			}
		}

		public double LogProbability {
			get {
				return this.logProbability;
			}
		}
	}
}

