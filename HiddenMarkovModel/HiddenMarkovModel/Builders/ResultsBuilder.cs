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

			foreach (var bigram in this.bigramInput) {
				// does our fromTransition exist?
				if (this.transitions.ContainsKey (bigram.From) && 
					this.transitions[bigram.From].HasDestination(bigram.To) && 
					this.emissions.ContainsKey(bigram.To)) {

					var frequency = this.transitions [bigram.From].GetFrequency (bigram.To);

					foreach (var emissionKey in this.emissions[bigram.To].DestinationKeys) {
						
					}

					// var newResultRecord = new ResultRecord(bigramTransition.FromTransition, bigramTransition.ToTransition, 
				
				}
				
			}


			throw new NotImplementedException ();
		}
	}
}

