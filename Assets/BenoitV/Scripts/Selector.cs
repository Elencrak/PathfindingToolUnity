using UnityEngine;
using System.Collections;

public class SelectorBenoitV : CompositeBenoitV
{
    public override bool Execute()
    {
        foreach (NodeBenoitV _node in _listOfNodes)
        {
            if (_node.Execute())
            {
                return true;
            }
        }
        return false;
    }
}
