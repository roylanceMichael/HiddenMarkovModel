using System;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	public class QueryResults
	{
		private readonly IEnumerable<string> results;

		public QueryResults (IEnumerable<string> results)
		{
			results.CheckWhetherArgumentIsNull ("results");
			this.results = results;
		}

		public IEnumerable<string> Results
		{
			get 
			{
				return this.results;
			}
		}
	}
}

