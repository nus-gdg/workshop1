using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Common
{
    public class StateMachine
    {
        public IState currentState;

        private List<Transition> currentTransitions;
        private Dictionary<Type, List<Transition>> transitions;
        private List<Transition> anyTransitions;
        private static readonly List<Transition> EmptyTransitions = new List<Transition>();
        private float decisionUpdateTimer = 0;

        public StateMachine()
        {
            currentTransitions = EmptyTransitions;
            transitions = new Dictionary<Type, List<Transition>>();
            anyTransitions = new List<Transition>();
        }

        public void Tick()
        {
            if (currentState != null)
            {
                currentState.Tick();
            }

            decisionUpdateTimer -= Time.deltaTime;
            if (decisionUpdateTimer < 0)
            {
                UpdateDecision();
            }
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            Transition transition = new Transition(to, condition);
            List<Transition> transitionsOfState;
            if (!transitions.TryGetValue(from.GetType(), out transitionsOfState))
            {
                transitionsOfState = new List<Transition>();
                transitions.Add(from.GetType(), transitionsOfState);
            }

            transitionsOfState.Add(transition);
        }

        /// <summary>
        /// Adds multiple transitions together.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="tos">toStates and their probabilites</param>
        public void AddTransitions(IState from, Func<bool> condition, params KeyValuePair<IState, float>[] tos)
        {
            Transition transition = new Transition(tos, condition);
            List<Transition> transitionsOfState;
            if (!transitions.TryGetValue(from.GetType(), out transitionsOfState))
            {
                transitionsOfState = new List<Transition>();
                transitions.Add(from.GetType(), transitionsOfState);
            }

            transitionsOfState.Add(transition);
        }

        /// <summary>
        /// An AnyTransition can be transited from any States.
        /// </summary>
        public void AddAnyTransition(IState to, Func<bool> condition)
        {
            Transition transition = new Transition(to, condition);
            anyTransitions.Add(transition);
        }

        /// <summary>
        /// Adds multiple any transitions together.
        /// </summary>
        /// <param name="condition">toStates and their probabilites</param>
        public void AddAnyTransitions(Func<bool> condition, params KeyValuePair<IState, float>[] tos)
        {
            Transition transition = new Transition(tos, condition);
            anyTransitions.Add(transition);
        }

        /// <summary>
        /// SetState sets the current state to be the given state, can be used to set the enter state.
        /// </summary>
        public void SetState(IState state)
        {
            currentState = state;
            if (!transitions.TryGetValue(state.GetType(), out currentTransitions))
            {
                currentTransitions = EmptyTransitions;
            }
            decisionUpdateTimer = state.DecisionUpdateRate;
            state.OnEnter();
        }

        private void UpdateDecision()
        {
            Transition transition = GetTransition();
            if (transition != null)
            {
                if (currentState != null)
                {
                    currentState.OnExit();
                }

                IState toState = GetToState(transition);
                if (toState != null)
                {
                    SetState(toState);
                    return;
                }
            }
            decisionUpdateTimer = currentState.DecisionUpdateRate;
        }

        private IState GetToState(Transition transition)
        {
            if (transition.to != null)
            {
                return transition.to;
            }

            if (transition.tos != null)
            {
                float x = UnityEngine.Random.value;
                float y = 0;
                foreach(KeyValuePair<IState, float> to in transition.tos)
                {
                    y += to.Value;
                    if (x <= y)
                    {
                        return to.Key;
                    }
                }
            }

            return null;
        }

        private Transition GetTransition()
        {
            foreach (Transition transition in anyTransitions)
            {
                if (transition.condition())
                {
                    return transition;
                }
            }

            foreach (Transition transition in currentTransitions)
            {
                if (transition.condition())
                {
                    return transition;
                }
            }

            return null;
        }

        private class Transition
        {
            public IState to;
            public KeyValuePair<IState, float>[] tos;
            public Func<bool> condition;

            public Transition(IState to, Func<bool> condition)
            {
                this.to = to;
                tos = null;
                this.condition = condition;
            }

            public Transition(KeyValuePair<IState, float>[] tos, Func<bool> condition)
            {
                to = null;
                this.tos = tos;
                this.condition = condition;
            }
        }
    }
}
