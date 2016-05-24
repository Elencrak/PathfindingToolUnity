using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PierreComposite : PierreNode {

    protected List<PierreNode> nodes = new List<PierreNode>();
    
    public void AddNode(PierreNode node)
    {
        nodes.Add(node);
    }

    public void RemoveNode(PierreNode node)
    {
        nodes.Remove(node);
    }

    public override bool Execute()
    {
        return true;
    }
}
