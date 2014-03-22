using System;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	public class HiddenMarkovModelBuilder : IBuilder<HiddenMarkovModel>
	{
		private readonly IList<TransitionEmissions> transitionEmissions;

		public HiddenMarkovModelBuilder (IList<TransitionEmissions> transitionEmissions)
		{
			transitionEmissions.CheckWhetherArgumentIsNull ("transitionEmissions");
			this.transitionEmissions = transitionEmissions;
		}

		public HiddenMarkovModel Build ()
		{
			return new HiddenMarkovModel (new HiddenMarkovDataBuilder (this.transitionEmissions).Build ());
		}
	}
}

