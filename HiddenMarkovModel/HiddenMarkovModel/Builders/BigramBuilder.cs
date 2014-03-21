using System;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	public class BigramBuilder : IBuilder<IEnumerable<Bigram>>
	{
		private readonly string[] input;

		public BigramBuilder (string[] input)
		{
			input.CheckWhetherArgumentIsNull ("input");
			this.input = input;
		}

		public IEnumerable<Bigram> Build ()
		{
			var bigramTransitions = new List<Bigram> ();

			// is the length bigger than 1?
			if (this.input.Length < 2) {
				return bigramTransitions;
			}

			for (var i = 1; i < this.input.Length; i++) {
				var fromTransition = this.input [i - 1];
				var toTransition = this.input [i];

				bigramTransitions.Add (new Bigram (fromTransition, toTransition));
			}
				
			return bigramTransitions;
		}
	}
}

