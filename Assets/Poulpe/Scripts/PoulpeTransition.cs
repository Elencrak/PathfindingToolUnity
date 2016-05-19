using UnityEngine;
using System.Collections;

public delegate bool Delegate();

public class PoulpeTransition : MonoBehaviour
{
    Delegate myDelegate;
    PoulpeState state;
    PoulpeStateMachine machine;

    public PoulpeTransition(Delegate MyDelegate, PoulpeState State, PoulpeStateMachine Machine)
    {
        myDelegate = MyDelegate;
        state = State;
        machine = Machine;
    }

    public void Check()
    {
        if(myDelegate())
        {
            machine.SetCurrentState(state);
        }
    }
}
