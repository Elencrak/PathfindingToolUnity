using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachineValentin : StateValentin{

    //List<StateValentin> listState = new List<StateValentin>(); 
    StateValentin currentState;

    public StateMachineValentin(StateValentin current)
    {
        currentState = current;
    }


    protected override void execute()
    {
        StateValentin SV = currentState.Step();
        if (SV != null)
        {
            currentState = SV;
        }
    }

}
