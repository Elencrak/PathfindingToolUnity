using UnityEngine;
using System.Collections;

public class StateDodgeJordanF : StateJordan {
   
    public StateDodgeJordanF()
    {

    }

    public override StateJordan check()
    {
        if (listTransition != null)
        {
            foreach (TransitionJordan trans in listTransition)
            {
                StateJordan temp = trans.check();

                if (temp != null)
                    return temp;
            }
        }
        return null;
    }

    public override void step()
    {
    }
}
