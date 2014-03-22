using System;

namespace HiddenMarkovModel
{
	internal static class ObjectExtensions
	{
		internal static void CheckWhetherArgumentIsNull(this object value, string name)
		{
			if (value == null) 
			{
				throw new ArgumentNullException (name);
			}
		}
	}
}

