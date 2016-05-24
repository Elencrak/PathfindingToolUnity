using UnityEngine;
using System.Collections;

public class PierreSuperSequence : PierreComposite {

    public override bool Execute()
    {
        foreach (PierreNode n in nodes)
        {

        }

        return true;
    }

    IEnumerator StartNode(PierreNode node)
    {
        node.Execute();

        yield return null;
    }
}
