using System;
using System.Collections.Generic;
using System.Linq;

namespace HiddenMarkovModel
{
	public class HiddenMarkovModel
	{
		private readonly HiddenMarkovData hiddenMarkovData;

		internal HiddenMarkovModel (HiddenMarkovData hiddenMarkovData)
		{
			hiddenMarkovData.CheckWhetherArgumentIsNull ("hiddenMarkovData");

			this.hiddenMarkovData = hiddenMarkovData;
		}

		// this will contain transitions and emissions
		public IEnumerable<InputResults> GetEmissions(string[] input)
		{
			input.CheckWhetherArgumentIsNull ("input");

			if (input.Length < 1) 
			{
				return new InputResults[0];
			}

			// we need to separate out the input into bigrams
			var bigramTransitions = new BigramBuilder (input).Build ();

			// generate the leaf results
			var stateResults = new StateRecordBuilder (bigramTransitions, this.hiddenMarkovData.Transitions, this.hiddenMarkovData.Emissions).Build ();

			// build the output
			return new InputResultsBuilder (stateResults).Build ();
		}
	}
}

