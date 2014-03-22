using System;
using System.Linq;
using System.Collections.Generic;

namespace HiddenMarkovModel
{
	internal class StateRecord
	{
		private readonly TransitionRecord transition;

		private readonly EmissionRecord emission;

		private readonly double logProbability;

		private readonly StateRecord previousState;

		private readonly HashSet<StateRecord> nextStates;

		internal StateRecord (TransitionRecord transition, EmissionRecord emission, StateRecord previousState)
		{
			transition.CheckWhetherArgumentIsNull ("transition");
			emission.CheckWhetherArgumentIsNull ("emission");

			this.transition = transition;
			this.emission = emission;
			this.previousState = previousState;
			this.nextStates = new HashSet<StateRecord> ();

			this.logProbability = this.transition.LogProbability + this.emission.LogProbability;
		}

		public TransitionRecord Transition {
			get {
				return this.transition;
			}
		}

		public EmissionRecord Emission {
			get {
				return this.emission;
			}
		}

		public double LogProbability {
			get {
				return this.logProbability;
			}
		}

		public StateRecord PreviousState {
			get {
				return this.previousState;
			}
		}

		public IEnumerable<StateRecord> NextStates {
			get {
				return this.nextStates;
			}
		}

		public void AddNextState(StateRecord nextState) {
			nextState.CheckWhetherArgumentIsNull ("nextState");
			this.nextStates.Add (nextState);
		}
	}
}

