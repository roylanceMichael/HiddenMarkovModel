using System;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	public class InputResults
	{
		private readonly IEnumerable<string> transitions;

		private readonly IEnumerable<string> emissions;

		private readonly double probability;

		internal InputResults (IEnumerable<string> transitions, IEnumerable<string> emissions, double probability)
		{
			transitions.CheckWhetherArgumentIsNull ("transitions");
			emissions.CheckWhetherArgumentIsNull ("emissions");

			this.transitions = transitions;
			this.emissions = emissions;
			this.probability = probability;
		}

		public IEnumerable<string> Transitions 
		{
			get {
				return this.transitions;
			}
		}

		public IEnumerable<string> Emissions {
			get {
				return this.emissions;
			}
		}

		public double Probability {
			get {
				return this.probability;
			}
		}
	}
}

