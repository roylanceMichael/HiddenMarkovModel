using System;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	internal class HiddenMarkovData
	{
		private readonly IDictionary<string, DictionaryLookup<TransitionRecord>> transitions;

		private readonly IDictionary<string, DictionaryLookup<EmissionRecord>> emissions;

		internal HiddenMarkovData (IDictionary<string, DictionaryLookup<TransitionRecord>> transitions, IDictionary<string, DictionaryLookup<EmissionRecord>> emissions)
		{
			transitions.CheckWhetherArgumentIsNull ("transitions");
			emissions.CheckWhetherArgumentIsNull ("emissions");

			this.transitions = transitions;
			this.emissions = emissions;
		}

		internal IDictionary<string, DictionaryLookup<TransitionRecord>> Transitions {
			get {
				return this.transitions;
			}
		}

		internal IDictionary<string, DictionaryLookup<EmissionRecord>> Emissions {
			get {
				return this.emissions;
			}
		}
	}
}

