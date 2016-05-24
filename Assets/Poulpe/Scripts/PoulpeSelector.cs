using UnityEngine;
using System.Collections;

public class PoulpeSelector : PoulpeComposite
{
    public override bool DoIt()
    {
        foreach (PoulpeNode node in nodes)
        {
            if (node.DoIt())
            {
                return true;
            }
        }
        return false;
    }
}