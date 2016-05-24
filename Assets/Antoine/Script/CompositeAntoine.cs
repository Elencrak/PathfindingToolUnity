using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CompositeAntoine : NodeAntoine
{
    public List<NodeAntoine> child = new List<NodeAntoine>();

    public void AddNode(NodeAntoine n)
    {
        child.Add(n);
    }

}
