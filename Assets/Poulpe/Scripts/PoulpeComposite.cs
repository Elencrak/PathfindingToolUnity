using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class PoulpeComposite : PoulpeNode
{
    protected List<PoulpeNode> nodes;

    public void SetNodes(List<PoulpeNode> Nodes)
    {
        nodes = Nodes;
    }
}
