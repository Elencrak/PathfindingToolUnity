using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace benjamin
{

    public class StateMachine  {

        List<AbstractState> stateList = new List<AbstractState>();
        AbstractState currentState;
        

	    public void AddState(AbstractState state)
        {
            stateList.Add(state);
        }

        public List<AbstractState> GetStateList()
        {
            return stateList;
        }

        public void SetCurrentState(AbstractState state)
        {
            currentState = state;
            currentState.Init();
        }

        public void StateUpdate()
        {
            currentState.StateUpdate();
        }

        public void Check()
        {
            currentState.Check();
        }
    }
}
