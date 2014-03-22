using System;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	public class TransitionEmissions
	{
		private readonly List<TransitionEmission> internalRepresentation;

		public TransitionEmissions ()
		{
			this.internalRepresentation = new List<TransitionEmission> ();
		}

		public IList<TransitionEmission> Records {
			get {
				return this.internalRepresentation;
			}
		}

		public void AddTransitionEmission(string transition, string emission) {
			transition.CheckWhetherArgumentIsNull ("transition");
			emission.CheckWhetherArgumentIsNull ("emissions");

			this.internalRepresentation.Add (new TransitionEmission (transition, emission));
		}
	}
}

