using System;
using System.Collections.Generic;
using System.Linq;

namespace HiddenMarkovModel
{
	internal class InputResultsBuilder : IBuilder<IEnumerable<InputResults>>
	{
		private readonly IEnumerable<StateRecord> leafStateRecords;

		internal InputResultsBuilder (IEnumerable<StateRecord> leafStateRecords)
		{
			leafStateRecords.CheckWhetherArgumentIsNull ("leafStateRecords");

			this.leafStateRecords = leafStateRecords;
		}

		public IEnumerable<InputResults> Build ()
		{
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

				yield return new InputResults (transitions, emissions, probabilities.Sum (prob => prob));
			}
		}
	}
}

