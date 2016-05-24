using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BenjaminBehaviorTree
{
    public abstract class Composite : Node {
        public List<Node> tasks;

        public abstract override bool Execute();

        public void AddTask(Node task)
        {
            tasks.Add(task);
        }
    }
}
