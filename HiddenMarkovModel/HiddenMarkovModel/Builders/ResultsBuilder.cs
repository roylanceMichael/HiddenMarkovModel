using System;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	public class ResultsBuilder : IBuilder<IEnumerable<ResultRecord>>
	{
		private readonly IEnumerable<Bigram> bigramInput;

		private readonly IDictionary<string, DictionaryLookup<TransitionRecord>> transitions;

		private readonly IDictionary<string, DictionaryLookup<EmissionRecord>> emissions;

		public ResultsBuilder (
			IEnumerable<Bigram> bigramInput, 
			IDictionary<string, DictionaryLookup<TransitionRecord>> transitions,
			IDictionary<string, DictionaryLookup<EmissionRecord>> emissions)
		{
			bigramInput.CheckWhetherArgumentIsNull ("bigramInput");
			transitions.CheckWhetherArgumentIsNull ("transitions");
			emissions.CheckWhetherArgumentIsNull ("emissions");

			this.bigramInput = bigramInput;
			this.transitions = transitions;
			this.emissions = emissions;
		}

		public IEnumerable<ResultRecord> Build ()
		{
			// start at the beginning
			var resultRecords = new List<ResultRecord> ();

			// build all resultRecords together
			foreach (var bigram in this.bigramInput) {
				// does our fromTransition exist?
				if (this.transitions.ContainsKey (bigram.From) && 
					this.transitions[bigram.From].HasDestination(bigram.To) && 
					this.emissions.ContainsKey(bigram.To)) {

					var transition = this.transitions [bigram.From].GetDestination (bigram.To);

					foreach (var emission in this.emissions[bigram.To].Destinations) {
						var newResultRecord = new ResultRecord (transition, emission);
					}
				}
			}

			// build structure linking all possibilities together
			foreach (var resultRecord in resultRecords) {
				
			}
				
			throw new NotImplementedException ();
		}
	}
}

