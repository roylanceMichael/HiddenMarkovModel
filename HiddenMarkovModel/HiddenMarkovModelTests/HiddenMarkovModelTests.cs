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
		private List<TransitionEmissions> BuildTestTransitionEmissions()
		{
			// http://nlp.stanford.edu:8080/parser/index.jsp - Thank You!
			// My dog also likes eating sausage.
			// My/PRP$ dog/NN also/RB likes/VBZ eating/VBG sausage/NN ./.
			var firstTransitionEmissions = new TransitionEmissions ();
			firstTransitionEmissions.AddTransitionEmission ("PRP$", "My");
			firstTransitionEmissions.AddTransitionEmission ("NN", "dog");
			firstTransitionEmissions.AddTransitionEmission ("RB", "also");
			firstTransitionEmissions.AddTransitionEmission ("VBZ", "likes");
			firstTransitionEmissions.AddTransitionEmission ("VBG", "eating");
			firstTransitionEmissions.AddTransitionEmission ("NN", "sausage");
			firstTransitionEmissions.AddTransitionEmission (".", ".");

			// My/PRP$ dog/NN likes/VBZ eating/VBG turkey/NN ./.
			var secondTransitionEmissions = new TransitionEmissions ();
			secondTransitionEmissions.AddTransitionEmission ("PRP$", "My");
			secondTransitionEmissions.AddTransitionEmission ("NN", "dog");
			secondTransitionEmissions.AddTransitionEmission ("VBZ", "likes");
			secondTransitionEmissions.AddTransitionEmission ("VBG", "eating");
			secondTransitionEmissions.AddTransitionEmission ("NN", "turkey");
			secondTransitionEmissions.AddTransitionEmission (".", ".");

			// My/PRP$ dog/NN likes/VBZ walking/VBG ./.
			var thirdTransitionEmissions = new TransitionEmissions ();
			thirdTransitionEmissions.AddTransitionEmission ("PRP$", "My");
			thirdTransitionEmissions.AddTransitionEmission ("NN", "dog");
			thirdTransitionEmissions.AddTransitionEmission ("VBZ", "likes");
			thirdTransitionEmissions.AddTransitionEmission ("VBG", "walking");
			thirdTransitionEmissions.AddTransitionEmission (".", ".");

			return new List<TransitionEmissions> { firstTransitionEmissions, secondTransitionEmissions, thirdTransitionEmissions };
		}
			
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
			const string Transition = "noun";
			const string Emission = "Cardio";

			var transitionEmissions = new TransitionEmissions ();
			transitionEmissions.AddTransitionEmission (Transition, Emission);
			var transitionEmissionsList = new List<TransitionEmissions> ();
			transitionEmissionsList.Add (transitionEmissions);

			var hmm = new HiddenMarkovModelBuilder (transitionEmissionsList).Build();

			// act
			var result = hmm.GetEmissions (new[] { Transition });

			// assert
			Assert.AreEqual (1, result.Count ());
			Assert.AreEqual (Emission, result.First ().Emissions.First());
		}

		[TestCase ()]
		public void My_dog_also_likes_eating_sausage_TestPassesWhenGivenInSampleData() 
		{
			// arrange
			var input = new[] { "PRP$", "NN", "RB", "VBZ", "VBG", "NN", "." };
			var transitionEmissions = this.BuildTestTransitionEmissions();

			var hmm = new HiddenMarkovModelBuilder (transitionEmissions).Build();

			// act
			var result = hmm.GetEmissions (input);

			// assert
			Assert.AreEqual (10, result.Count ());
		}

		[TestCase ()]
		public void Brown_Example_Test_Gets_Correct_Input() 
		{
			// arrange
			// The/at Fulton/np-tl County/nn-tl Grand/jj-tl Jury/nn-tl said/vbd Friday/nr an/at investigation/nn 
			// of/in Atlanta's/np$ recent/jj primary/nn election/nn produced/vbd ``/`` no/at evidence/nn ''/'' 
			// that/cs any/dti irregularities/nns took/vbd place/nn ./.

			var transitionEmissions = EmbeddedResourceUtilities.BuildTransitionEmissionsFromBrownText ().ToList();

			var input = new[] { "at", "np-tl", "nn-tl", "jj-tl", "nn-tl" };

			// should see The Fulton County Grand Jury said at investigation of Atlanta's recent primary election
			// in the emissions

			var hmm = new HiddenMarkovModelBuilder (transitionEmissions).Build();

			// act
			var results = hmm.GetEmissions (input);

			// assert
			Assert.AreEqual (10, results.Count ());

			const string ExpectedResult = "the Fulton Department Rural County";
			var firstResult = results.First ();
			Assert.AreEqual (ExpectedResult, firstResult.EmissionsResult(" "));
		}
	}
}

