using UnityEngine;
using System.Collections;

public abstract class TransitionWill {
    public int idAgent;
    public StateWill nextState;
    public TransitionWill(int id, StateWill pState)
    {
        idAgent = id;
        nextState = pState;
    }
    public abstract StateWill check(StateWill current =null);
}
