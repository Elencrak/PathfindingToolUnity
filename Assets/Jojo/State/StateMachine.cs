using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace JojoKiller
{
    [System.Serializable]
    public class StateMachine
    {
        [SerializeField]
        public IState currentState;

        public int test;
        public StateMachine(){}

        public void execution()
        {
            currentState = currentState.execution();
        }
    }
}
