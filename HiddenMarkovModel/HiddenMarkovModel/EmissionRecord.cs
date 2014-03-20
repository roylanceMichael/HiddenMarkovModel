using System;

namespace HiddenMarkovModel
{
	public class EmissionRecord
	{
		public EmissionRecord (string transition, string emission)
		{
			transition.CheckWhetherArgumentIsNull ("transition");
			emission.CheckWhetherArgumentIsNull ("emission");

			this.Transition = transition;
			this.Emission = emission;
		}

		public string Transition { get; private set; }

		public string Emission { get; private set; }
	}
}

