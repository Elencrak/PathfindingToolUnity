using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BenjaminBehaviorTree
{
    public class Selector : Composite
    {

        public Selector()
        {
            tasks = new List<Node>();
        }

        public Selector(List<Node> taskList)
        {
            tasks = taskList;
        }

        public override bool Execute()
        {
            foreach(Node task in tasks)
            {
                if (task.Execute())
                    return true;
            }
            return false;
        }
    }
}
