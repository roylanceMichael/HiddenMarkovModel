using System;
using System.Text;
using System.Linq;
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

		public string EmissionsResult(string separator) {
			return string.Join (separator, this.emissions);
		}

		public string TransitionsResult(string separator) {
			return string.Join (separator, this.transitions);
		}

		public string CombinedResult(string separator) {
			var emissionCount = this.emissions.Count ();
			if (emissionCount != this.transitions.Count ()) {
				throw new InvalidOperationException ("emissions and transitions have a different count... for some reason");
			}

			var workspace = new StringBuilder ();

			for (var i = 0; i < emissionCount; i++) {
				var emission = this.emissions.ElementAt(i);
				var transition = this.transitions.ElementAt(i);

				if (i == emissionCount - 1) {
					workspace.Append (emission + "/" + transition);
				} else {
					workspace.Append (emission + "/" + transition + separator);
				}
			}

			return workspace.ToString();
		}
	}
}

