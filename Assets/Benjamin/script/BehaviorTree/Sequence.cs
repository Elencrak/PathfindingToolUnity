using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BenjaminBehaviorTree
{
    public class Sequence : Composite
    {
        public Sequence()
        {
            tasks = new List<Node>();
        }

        public Sequence(List<Node> taskList)
        {
            tasks = taskList;
        }

        public override bool Execute()
        {
            foreach (Node task in tasks)
            {
                if (!task.Execute())
                    return false;
            }
            return true;
        }
        
    }
}
