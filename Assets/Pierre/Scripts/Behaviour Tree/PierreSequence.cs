using UnityEngine;
using System.Collections;

public class PierreSequence : PierreComposite
{
    
    public override bool Execute()
    {
        bool b = true;

        Debug.Log("sequence " + nodes.Count);

        foreach (PierreNode n in nodes)
        {
            b = n.Execute();

            if (!b) break;
        }

        return b;
    }
}
