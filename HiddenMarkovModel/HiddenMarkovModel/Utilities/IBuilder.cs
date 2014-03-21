using System;

namespace HiddenMarkovModel
{
	public interface IBuilder<out T>
	{
		T Build();
	}
}

