using System;
using System.IO;
using System.Text.RegularExpressions;

using HiddenMarkovModel;
using System.Reflection;
using System.Collections.Generic;

namespace HiddenMarkovModelTests
{
	public static class EmbeddedResourceUtilities
	{
		private const string Brown_Ca01File = "HiddenMarkovModelTests.docs.brown-ca01.txt";

		private static readonly Regex WhiteSpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);

		private static readonly Regex ForwardSpaceRegex = new Regex("/", RegexOptions.Compiled);

		public static IEnumerable<TransitionEmissions> BuildTransitionEmissionsFromBrownText() 
		{
			var assembly = Assembly.GetExecutingAssembly ();

			using (var fileStream = assembly.GetManifestResourceStream (Brown_Ca01File)) {
				if (fileStream == null) {
					throw new InvalidOperationException ("brown-ca01.txt should exist. It doesn't, so you're seeing this error");
				}

				using (var stringReader = new StreamReader (fileStream)) {
					string line = stringReader.ReadLine ();

					while (line != null) {
						var transitionEmissions = new TransitionEmissions ();

						foreach (var tuple in ReadLine(line)) {
							transitionEmissions.AddTransitionEmission (tuple.Item1, tuple.Item2);
						}

						yield return transitionEmissions;

						line = stringReader.ReadLine ();
					}
				}
			}
		}

		private static IEnumerable<Tuple<string, string>> ReadLine(string line) 
		{
			// split on whitespace
			var wordPairs = WhiteSpaceRegex.Split (line.Trim ());

			foreach (var wordPair in wordPairs) {
				var splitWordPair = ForwardSpaceRegex.Split (wordPair);

				if (splitWordPair.Length == 2) {
					// word / pos, pos is our transition, word is our emission
					yield return Tuple.Create (splitWordPair [1], splitWordPair [0]);
				}
			}
		}
	}
}

