using UnityEngine;
using System.Collections;

public delegate bool ExecuteFiltre(bool inBool);

public class FiltreAntoine : CompositeAntoine
{
    public ExecuteFiltre theDelegate;

    public FiltreAntoine(ExecuteFiltre aDelegate)
    {
        theDelegate = aDelegate;
    }

    public override bool Execute()
    {
         return theDelegate(child[0].Execute());
    }

}
