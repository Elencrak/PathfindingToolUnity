using UnityEngine;
using System.Collections;

namespace JojoBehaviourTree
{
    public class Sequence : Composite
    {
        public Sequence()
        {

        }

        public override bool execute()
        {
            foreach (Node n in elementInComposite)
            {
                if (n.execute() == false)
                    return false;             
            }
            return true;
        }

    }
}
