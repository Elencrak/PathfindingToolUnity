using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StateMachineBenoitV : StateBenoitV {
    
    public StateBenoitV _currentState;

    public override void Execute(GameObject parAgent)
    {
        _currentState = _currentState.step(parAgent);
    }
    
}
