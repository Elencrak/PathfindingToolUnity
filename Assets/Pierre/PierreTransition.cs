using UnityEngine;
using System.Collections;

public class PierreTransition{

    PierreState before;
    PierreState after;
    public Condition condition;

    public delegate bool Condition();

    public PierreTransition(PierreState b, PierreState a)
    {
        before = b;
        after = a;
    }
    
    public void Check()
    {
        if (condition())
        {
            before.StateEnd();
            after.StateStart();
        }
    }
}
