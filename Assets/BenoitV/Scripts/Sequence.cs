using UnityEngine;
using System.Collections;

public class SequenceBenoitV : Composite {

    public override bool Execute()
    {
        foreach(NodeBenoitV _node in _listOfNodes)
        {
            if(!_node.Execute())
            {
                return false;
            }
        }
        return true;
    }
}
