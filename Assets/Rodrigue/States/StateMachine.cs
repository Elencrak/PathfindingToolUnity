using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Rodrigue
{
    public class StateMachine : State
    {
        public State currentState;

        public State GetCurrentState()
        {
            return currentState;
        }

        public override void Execute()
        {
            currentState = currentState.step();
        }
    }
}

