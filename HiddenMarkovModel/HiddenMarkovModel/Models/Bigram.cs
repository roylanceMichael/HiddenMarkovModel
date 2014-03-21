using System;

namespace HiddenMarkovModel
{
	public class Bigram
	{
		public Bigram (string from, string to)
		{
			from.CheckWhetherArgumentIsNull ("from");
			to.CheckWhetherArgumentIsNull ("to");

			this.From = from;
			this.To = to;
		}

		public string From { get; private set; }

		public string To { get; private set; }
	}
}

