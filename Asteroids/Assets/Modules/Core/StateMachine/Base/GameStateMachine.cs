using System;
using System.Collections.Generic;

namespace Modules.Core
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        public Dictionary<Type, IState> States => _states;

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            TState state = GetState<TState>();
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}