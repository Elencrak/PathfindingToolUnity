using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Composite : NodeTree {

    protected List<NodeTree> listNode = new List<NodeTree>();

    public override bool execute()
    {
        return false;
    }

    public void addListNode(List<NodeTree> listNodeToAdd)
    {
        listNode = listNodeToAdd;
    }
}
