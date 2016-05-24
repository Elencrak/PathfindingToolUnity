using UnityEngine;
using System.Collections;

public class SequenceAntoine : CompositeAntoine
{

    public SequenceAntoine()
    {

    }

    public override bool Execute()
    {

        foreach(NodeAntoine n in child)
        {
            if(n.Execute() != true)
            {
                return false;
            }
        }

        return true;
    }
}
