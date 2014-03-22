using System;
using HiddenMarkovModel;
using NUnit.Framework;

namespace HiddenMarkovModelTests
{
	[TestFixture ()]
	public class TransitionEmissionsTests
	{
		[Test ()]
		public void RecordsAddedWhenAccessedThroughMethod ()
		{
			// arrange
			const string Transition = "test1";
			const string Emission = "test2";

			var transitionEmission = new TransitionEmissions ();

			// act
			transitionEmission.AddTransitionEmission (Transition, Emission);

			// assert
			Assert.AreEqual (1, transitionEmission.Records.Count);
			Assert.AreEqual (Transition, transitionEmission.Records [0].Transition);
			Assert.AreEqual (Emission, transitionEmission.Records [0].Emission);
		}
	}
}

