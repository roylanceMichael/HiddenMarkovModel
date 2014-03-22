using System;
using System.Collections.Generic;
using System.Linq;

namespace HiddenMarkovModel
{
	internal class HiddenMarkovDataBuilder : IBuilder<HiddenMarkovData>
	{
		private readonly IList<TransitionEmissions> transitionEmissions;

		internal HiddenMarkovDataBuilder (IList<TransitionEmissions> transitionEmissions)
		{
			transitionEmissions.CheckWhetherArgumentIsNull ("transitionEmissions");
			this.transitionEmissions = transitionEmissions;
		}

		public HiddenMarkovData Build ()
		{
			var transitions = new Dictionary<string, Dictionary<string, int>> ();
			var emissions = new Dictionary<string, Dictionary<string, int>> ();

			foreach (var transitionEmission in this.transitionEmissions) {
				// always start with an empty transition for the start state
				var previousTransition = string.Empty;

				foreach (var record in transitionEmission.Records) {
					this.PopulateDictionary (ref transitions, previousTransition, record.Transition);
					this.PopulateDictionary (ref emissions, record.Transition, record.Emission);
					previousTransition = record.Transition;
				}
			}

			var transitionDictionaryLookup = new Dictionary<string, DictionaryLookup<TransitionRecord>> ();
			// convert transitions
			foreach (var record in transitions) {
				var totalCount = (double) record.Value.Sum (tran => tran.Value);

				var hashSet = new HashSet<TransitionRecord> ();

				foreach (var destination in record.Value) {
					var logProb = Math.Log (destination.Value / totalCount);
					hashSet.Add(new TransitionRecord (record.Key, destination.Key, logProb));
				}

				transitionDictionaryLookup[record.Key] = new DictionaryLookup<TransitionRecord>(record.Key, hashSet);
			}

			var emissionDictionaryLookup = new Dictionary<string, DictionaryLookup<EmissionRecord>> ();
			// convert emissions
			foreach (var record in emissions) {
				var totalCount = (double) record.Value.Sum (tran => tran.Value);

				var hashSet = new HashSet<EmissionRecord> ();

				foreach (var destination in record.Value) {
					var logProb = Math.Log (destination.Value / totalCount);
					hashSet.Add(new EmissionRecord (record.Key, destination.Key, logProb));
				}

				emissionDictionaryLookup[record.Key] = new DictionaryLookup<EmissionRecord>(record.Key, hashSet);
			}


			return new HiddenMarkovData (transitionDictionaryLookup, emissionDictionaryLookup);
		}

		private void PopulateDictionary(ref Dictionary<string, Dictionary<string, int>> dictionary, string key, string value)
		{
			if (dictionary.ContainsKey (key)) {
				var subDictionary = dictionary [key];

				if (subDictionary.ContainsKey (value)) {
					subDictionary [value]++;
				} else {
					subDictionary [value] = 1;
				}
			} else {
				dictionary [key] = new Dictionary<string, int> 
				{
					{ value, 1 }
				};
			}
		}
	}
}

