using System;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	internal class BigramBuilder : IBuilder<IEnumerable<Bigram>>
	{
		private readonly string[] input;

		internal BigramBuilder (string[] input)
		{
			input.CheckWhetherArgumentIsNull ("input");
			this.input = input;
		}

		public IEnumerable<Bigram> Build ()
		{
			var bigramTransitions = new List<Bigram> ();

			// is the length bigger than 1?
			if (this.input.Length < 1) {
				return bigramTransitions;
			}

			bigramTransitions.Add (this.BuildFirstTransition (this.input [0]));

			for (var i = 1; i < this.input.Length; i++) {
				var fromTransition = this.input [i - 1];
				var toTransition = this.input [i];

				bigramTransitions.Add (new Bigram (fromTransition, toTransition));
			}
				
			return bigramTransitions;
		}

		private Bigram BuildFirstTransition(string firstTransition) 
		{
			return new Bigram (string.Empty, firstTransition);
		}
	}
}

