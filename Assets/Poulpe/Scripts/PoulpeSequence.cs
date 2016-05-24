using UnityEngine;
using System.Collections;
using System;

public class PoulpeSequence : PoulpeComposite
{
    public override bool DoIt()
    {
        foreach(PoulpeNode node in nodes)
        {
            if(!node.DoIt())
            {
                return false;
            }
        }
        return true;
    }
}
