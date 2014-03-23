using System;
using HiddenMarkovModel;
using NUnit.Framework;

namespace HiddenMarkovModelTests
{
	[TestFixture ()]
	public class TransitionEmissionsTests
	{
		private const string Transition = "test1";
		private const string Emission = "test2";

		[Test ()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionThrownWhenTransitionIsNull ()
		{
			// arrange
			var transitionEmission = new TransitionEmissions ();

			// act
			transitionEmission.AddTransitionEmission (null, Emission);

			// assert - exception
		}

		[Test ()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionThrownWhenEmissionIsNull ()
		{
			// arrange
			var transitionEmission = new TransitionEmissions ();

			// act
			transitionEmission.AddTransitionEmission (Transition, null);

			// assert - exception
		}

		[Test ()]
		public void RecordsAddedWhenAccessedThroughMethod ()
		{
			// arrange
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

