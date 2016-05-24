using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
namespace JojoKiller
{
    public delegate bool check();

    public class Transition
    {
        protected check _delegate;
        public IState _state;

        public Transition(check myDelegate, IState state)
        {
            _delegate += myDelegate;
            _state = state;
        }

        public IState check()
        {
            if (_delegate())
                return _state;
            return null;
        }

    }
}
