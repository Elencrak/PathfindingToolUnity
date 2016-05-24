using UnityEngine;
using System.Collections;

public class SuperSequenceAntoine : CompositeAntoine
{

    public SuperSequenceAntoine()
    {

    }


    // TO DO : Rajouter le multithread avec des coroutines
    public override bool Execute()
    {

        foreach (NodeAntoine n in child)
        {
            if (n.Execute() != true)
            {
                return false;
            }
        }

        return true;
    }
}
