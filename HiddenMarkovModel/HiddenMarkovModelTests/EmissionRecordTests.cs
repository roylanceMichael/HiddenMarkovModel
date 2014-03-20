using HiddenMarkovModel;
using NUnit.Framework;
using System;

namespace HiddenMarkovModelTests
{
	[TestFixture ()]
	public class EmissionRecordTests
	{
		[Test ()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionThrownWhenNullTransition ()
		{
			new EmissionRecord (null, "something");
		}

		[Test ()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExceptionThrownWhenNullEmission ()
		{
			new EmissionRecord ("something", null);
		}
	}
}

