using System;
using System.Collections.Generic;
using System.Linq;

namespace HiddenMarkovModel
{
	public class HiddenMarkovModel
	{
		private readonly IDictionary<string, DictionaryLookup<TransitionRecord>> transitions;

		private readonly IDictionary<string, DictionaryLookup<EmissionRecord>> emissions;

		public HiddenMarkovModel (IEnumerable<DictionaryLookup<TransitionRecord>> transitions, IEnumerable<DictionaryLookup<EmissionRecord>> emissions)
		{
			transitions.CheckWhetherArgumentIsNull ("transitions");
			emissions.CheckWhetherArgumentIsNull ("emissions");

			this.transitions = this.BuildDictionary(transitions);
			this.emissions = this.BuildDictionary (emissions);
		}

		// this will contain transitions and emissions
		public IEnumerable<QueryResults> GetEmissions(string[] input)
		{
			input.CheckWhetherArgumentIsNull ("input");

			if (input.Length < 1) 
			{
				return new QueryResults[0];
			}

			// we need to separate out the input into bigrams
			var bigramTransitions = new BigramBuilder (input).Build ();


			return null;
		}

		private IDictionary<string, DictionaryLookup<T>> BuildDictionary<T>(IEnumerable<DictionaryLookup<T>> dictionaryLookups) where T : KeyRecord
		{
			var newDictionary = new Dictionary<string, DictionaryLookup<T>> ();

			foreach (var dictionaryLookup in dictionaryLookups) {
				newDictionary [dictionaryLookup.Key] = dictionaryLookup;
			}

			return newDictionary;
		}
	}
}

