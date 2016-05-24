using UnityEngine;
using System.Collections;

public class Sequence : Composite {


    public override bool execute()
    {
        foreach(NodeTree node in listNode)
        {
            if(node.execute()== false)
            {
                return false;
            }
        }
        return true;
    }


}
