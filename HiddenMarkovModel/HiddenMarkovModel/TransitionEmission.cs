using System;

namespace HiddenMarkovModel
{
	public class TransitionEmission
	{
		internal TransitionEmission (string transition, string emission)
		{
			transition.CheckWhetherArgumentIsNull ("transition");
			emission.CheckWhetherArgumentIsNull ("emissions");

			this.Transition = transition;
			this.Emission = emission;
		}

		public string Transition { get; private set; }

		public string Emission { get; private set; }
	}
}

