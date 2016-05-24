using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Rodrigue
{
    public abstract class Composite : Node
    {

        protected List<Node> nodeList;

        public abstract override bool Execute();

        public void AddChild(Node parNode)
        {
            nodeList.Add(parNode);
        }

        public void RemoveChild(Node parNode)
        {
            nodeList.Remove(parNode);
        }
    }

}
