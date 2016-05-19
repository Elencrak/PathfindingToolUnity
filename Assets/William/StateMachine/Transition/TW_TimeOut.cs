using UnityEngine;
using System.Collections;

public class TW_TimeOut : TransitionWill {
    bool isTrue;

    bool firstTime = true;
    bool timeOut = false;

    public TW_TimeOut(int id, StateWill pState, bool pIsTrue): base(id, pState)
    {
        isTrue = pIsTrue;
    }

    public override StateWill check(StateWill current = null)
    {

        if (current!=null)
        {
            SW_Dodge dodge = (SW_Dodge)current;
            if (firstTime)
            {
                firstTime = false;
            }

            return nextState;
        }
        
        
        
        return null;
    }

    IEnumerator timer(float t)
    {
        yield return new WaitForSeconds(t);
        timeOut = true;
    }
}
