using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace JojoBehaviourTree
{
    public class Composite : Node
    {
        protected List<Node> elementInComposite = new List<Node>();


        public void addElementIncomposite(Node p_Node)
        {
            elementInComposite.Add(p_Node);
        }

        public override bool execute()
        {
            return false;
        }

        public void removeElementInComposite(Node p_Node)
        {
            elementInComposite.Remove(p_Node);
        }
    }
}
