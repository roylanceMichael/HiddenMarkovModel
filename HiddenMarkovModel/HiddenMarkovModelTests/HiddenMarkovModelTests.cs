using System;
using System.Linq;
using HiddenMarkovModel;
using NUnit.Framework;
using System.Collections.Generic;

namespace HiddenMarkovModelTests
{
	[TestFixture ()]
	public class HiddenMarkovModelTests
	{
		[TestCase ()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ErrorWhenTransitionEmissionsNull() 
		{
			new HiddenMarkovModelBuilder(null);
		}

		[TestCase ()]
		public void HiddenMarkovModelInstantiatedWithProperTransitionEmissions() 
		{
			// arrange
			var transitionEmissions = new TransitionEmissions ();
			transitionEmissions.AddTransitionEmission ("Cardio", "noun");
			var transitionEmissionsList = new List<TransitionEmissions> ();
			transitionEmissionsList.Add (transitionEmissions);

			var hmm = new HiddenMarkovModelBuilder (transitionEmissionsList).Build();

			// act
			var result = hmm.GetEmissions (new[] {"Cardio"});

			// assert
			Assert.AreEqual (1, result.Count ());
			Assert.AreEqual ("noun", result.First ().Emissions.First());
		}
	}
}

