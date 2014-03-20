using HiddenMarkovModel;
using NUnit.Framework;
using System;

namespace HiddenMarkovModelTests
{
	[TestFixture ()]
	public class TransitionRecordTests
	{
		[Test ()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionThrownWhenNullFromTransition ()
		{
			new TransitionRecord (null, "something");
		}

		[Test ()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionThrownWhenNullToTransition ()
		{
			new TransitionRecord ("something", null);
		}
	}
}

