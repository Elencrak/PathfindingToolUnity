using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Rodrigue
{
    public abstract class State
    {
        public List<Transition> _listTransition = new List<Transition>();

        public enum StateID
        {
            Idle,
            Search,
            Patrol
        }

        public abstract void Execute();

        public virtual State step()
        {
            foreach(Transition _parTransition in _listTransition)
            {
                if (_parTransition.Check())
                {
                    return _parTransition.nextState;
                }
            }
            Execute();
            return this;
        }

    }
}

