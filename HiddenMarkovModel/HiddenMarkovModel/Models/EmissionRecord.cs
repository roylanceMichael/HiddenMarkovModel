using System;

namespace HiddenMarkovModel
{
	internal class EmissionRecord : KeyRecord
	{
		internal EmissionRecord (string transition, string emission, double logProbability)
			: base(transition, emission, logProbability)
		{
		}

		public string Transition { get { return this.Key; } }

		public string Emission { get { return this.Value; } }
	}
}

