using UnityEngine;
using System.Collections;

public class SelectorAntoine : CompositeAntoine
{

    public SelectorAntoine()
    {

    }

    public override bool Execute()
    {
        foreach (NodeAntoine n in child)
        {
            if (n.Execute())
            {
                return true;
            }
        }

        return false;
    }

}
