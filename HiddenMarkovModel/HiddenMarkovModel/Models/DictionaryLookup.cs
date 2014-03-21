using System;
using System.Collections.Generic;
using System.Linq;

namespace HiddenMarkovModel
{
	public class DictionaryLookup<T> where T: KeyRecord
	{
		private readonly string key;

		private readonly HashSet<T> frequency;

		private readonly double smoothing;

		public DictionaryLookup (string key, HashSet<T> frequency, double smoothing = 0.00001)
		{
			key.CheckWhetherArgumentIsNull ("key");
			frequency.CheckWhetherArgumentIsNull ("frequency");

			this.key = key;
			this.frequency = frequency;
			this.smoothing = smoothing;
		}

		public string Key 
		{
			get 
			{
				return this.key;
			}
		}

		public IEnumerable<string> DestinationKeys 
		{
			get 
			{
				return this.frequency.Select(keyRecord => keyRecord.Key);
			}
		}

		public bool HasDestination(string destinationKey) 
		{
			destinationKey.CheckWhetherArgumentIsNull ("destinationKey");
			return this.frequency.Any (keyRecord => keyRecord.Key == destinationKey);
		}

		public double GetFrequency(string destinationKey)
		{
			destinationKey.CheckWhetherArgumentIsNull ("destinationKey");

			var record = this.frequency.FirstOrDefault (keyRecord => keyRecord.Key == destinationKey);

			if (record != null) {
				return record.LogProbability;
			}

			return this.smoothing;
		}
	}
}

