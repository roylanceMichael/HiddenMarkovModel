using System;

namespace HiddenMarkovModel
{
	public abstract class KeyRecord
	{
		private readonly string key;

		private readonly string value;

		private readonly double logProbability;

		protected KeyRecord (string key, string value, double logProbability)
		{
			key.CheckWhetherArgumentIsNull ("key");
			value.CheckWhetherArgumentIsNull ("value");

			this.key = key;
			this.value = value;
			this.logProbability = logProbability;
		}

		public string Key {
			get {
				return this.key;
			}
		}

		public string Value {
			get {
				return this.value;
			}
		}

		public double LogProbability {
			get {
				return this.logProbability;
			}
		}
	}
}

