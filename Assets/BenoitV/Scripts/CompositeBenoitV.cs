using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class CompositeBenoitV : NodeBenoitV
{

    protected List<NodeBenoitV> _listOfNodes;

    public override bool Execute()
    {
        return true;
    }
}
