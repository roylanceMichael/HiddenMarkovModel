using System;
using System.Linq;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	public class ResultsBuilder : IBuilder<IEnumerable<StateRecord>>
	{
		private readonly IEnumerable<Bigram> bigramInput;

		private readonly IDictionary<string, DictionaryLookup<TransitionRecord>> transitions;

		private readonly IDictionary<string, DictionaryLookup<EmissionRecord>> emissions;

		private readonly List<StateRecord> stateRecordWorkspace;

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

			this.stateRecordWorkspace = new List<StateRecord> ();
		}

		public IEnumerable<StateRecord> Build ()
		{
			// start at the beginning
			this.stateRecordWorkspace.Clear ();

			for (var i = 0; i < this.bigramInput.Count(); i++) {
				var bigram = this.bigramInput.ElementAt(i);

				// handle first transition
				if (this.transitions.ContainsKey (bigram.From) &&
				    this.transitions [bigram.From].HasDestination (bigram.To) &&
				    this.emissions.ContainsKey (bigram.To)) {
					var transition = this.transitions [bigram.From].GetDestination (bigram.To);

					var temporaryList = new List<StateRecord> ();

					foreach (var emission in this.emissions[bigram.To].Destinations) {

						foreach (var newStateRecord in this.CreateNonRootStateRecords(transition, emission, i == 0)) {
							temporaryList.Add (newStateRecord);
						}
					}

					this.stateRecordWorkspace.Clear ();
					this.stateRecordWorkspace.AddRange (temporaryList);
				}
			}
				
			return this.stateRecordWorkspace;
		}

		private IEnumerable<StateRecord> CreateNonRootStateRecords(TransitionRecord transition, EmissionRecord emission, bool isRoot) 
		{
			var fromTransition = transition.FromTransition;

			if (isRoot) {
				yield return new StateRecord (transition, emission, null);
				yield break;
			}

			var previousStateRecords = this.stateRecordWorkspace.Where (stateRecord => stateRecord.Transition.ToTransition == fromTransition);

			foreach (var previousStateRecord in previousStateRecords) {
				var newStateRecord = new StateRecord (transition, emission, previousStateRecord);
				previousStateRecord.AddNextState (newStateRecord);
				yield return newStateRecord;
			}
		}
	}
}

