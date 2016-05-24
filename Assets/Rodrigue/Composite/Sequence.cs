using UnityEngine;
using System.Collections;
using System;

namespace Rodrigue
{
    public class Sequence : Composite
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override bool Execute()
        {
            foreach (Node parNode in nodeList)
            {
                if (!parNode.Execute())
                {
                    return false;
                }
            }
            return true;
        }
    }

}
