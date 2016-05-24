using UnityEngine;
using System.Collections;

namespace JojoBehaviourTree
{
    public class Selector : Composite
    {
        public Selector()
        {

        }

        public override bool execute()
        {
            foreach (Node n in elementInComposite)
            {
                if (n.execute() == true)
                    return true;                
            }
            return false;
        }
    }
}
