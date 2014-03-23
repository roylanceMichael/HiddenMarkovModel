using System;
using System.Collections.Generic;
using System.Linq;

namespace HiddenMarkovModel
{
	internal class InputResultsBuilder : IBuilder<IEnumerable<InputResults>>
	{
		private readonly IEnumerable<StateRecord> leafStateRecords;

		private readonly HashSet<InputResults> topInputResults;

		private readonly int totalResultsToReturn;

		internal InputResultsBuilder (IEnumerable<StateRecord> leafStateRecords, int totalResultsToReturn = 10)
		{
			leafStateRecords.CheckWhetherArgumentIsNull ("leafStateRecords");

			this.leafStateRecords = leafStateRecords;
			this.topInputResults = new HashSet<InputResults> ();
			this.totalResultsToReturn = totalResultsToReturn;
		}

		public IEnumerable<InputResults> Build ()
		{
			this.topInputResults.Clear ();
			var referenceKeyHash = new HashSet<string> ();

			foreach (var leafStateRecord in this.leafStateRecords) {
				var emissions = new List<string> ();
				var transitions = new List<string> ();
				var probabilities = new List<double> ();

				var currentStateRecord = leafStateRecord;

				// climb the tree
				while (currentStateRecord != null) {
					emissions.Add (currentStateRecord.Emission.Emission);
					transitions.Add (currentStateRecord.Transition.ToTransition);
					probabilities.Add (currentStateRecord.LogProbability);

					currentStateRecord = currentStateRecord.PreviousState;
				}
					
				var key = string.Join ("~", emissions) + string.Join ("~", transitions);

				var probability = probabilities.Sum (prob => prob);

				if (referenceKeyHash.Contains (key) || !this.ShouldUpdateList(probability)) {
					continue;
				}

				referenceKeyHash.Add (key);

				transitions.Reverse ();
				emissions.Reverse ();

				this.topInputResults.Add (new InputResults (transitions, emissions, probability));
			}

			return this.topInputResults;
		}

		private bool ShouldUpdateList(double probability) {
			if (this.topInputResults.Count < this.totalResultsToReturn) {
				return true;
			}

			var last = this.topInputResults.OrderBy (inputResult => inputResult.Probability).First ();

			if (probability > last.Probability) {
				this.topInputResults.Remove (last);
				return true;
			}

			return false;
		}
	}
}

