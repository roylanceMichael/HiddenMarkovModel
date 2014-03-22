using System;

namespace HiddenMarkovModel
{
	internal interface IBuilder<out T>
	{
		T Build();
	}
}

