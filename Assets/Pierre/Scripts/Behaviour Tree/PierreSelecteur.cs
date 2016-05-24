using UnityEngine;
using System.Collections;

public class PierreSelecteur : PierreComposite
{
    
    public override bool Execute()
    {
        bool b = false;

        Debug.Log("selecteur " + nodes.Count);

        foreach (PierreNode n in nodes)
        {
            b = n.Execute();

            if (b) break;
        }

        return b;
    }
}
