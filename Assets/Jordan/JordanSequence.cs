using UnityEngine;
using System.Collections;

public class JordanSequence : JordanComposite {

    public JordanSequence()
    {

    }

    public override bool execute()
    {
        foreach (JordanNode task in listTask)
        {
            if (!task.execute())
                return false;
        }
        return true;
    }

}
