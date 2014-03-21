using System;

namespace HiddenMarkovModel
{
	public class EmissionRecord : KeyRecord
	{
		public EmissionRecord (string transition, string emission, double logProbability)
			: base(transition, emission, logProbability)
		{
		}

		public string Transition { get { return this.Key; } }

		public string Emission { get { return this.Value; } }
	}
}

