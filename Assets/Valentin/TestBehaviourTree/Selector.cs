using UnityEngine;
using System.Collections;

public class Selector : Composite {

    public override bool execute()
    {
        foreach(NodeTree node in listNode)
        {
            if(node.execute())
            {
                return true;
            }
        }
        return false;
    }



}
