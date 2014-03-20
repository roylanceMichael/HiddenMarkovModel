using System;

namespace HiddenMarkovModel
{
	public static class ObjectExtensions
	{
		public static void CheckWhetherArgumentIsNull(this object value, string name)
		{
			if (value == null) 
			{
				throw new ArgumentNullException (name);
			}
		}
	}
}

