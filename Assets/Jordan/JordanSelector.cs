using UnityEngine;
using System.Collections;

public class JordanSelector : JordanComposite {

	public JordanSelector()
    {

    }

    public override bool execute()
    {
        foreach (JordanNode task in listTask)
        {
            if (task.execute())
                return true;
        }
        return false;
    }
}
