using UnityEngine;
using System.Collections;
using System;
namespace Rodrigue
{
    public class Selector : Composite
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
                if (parNode.Execute())
                {
                    return true;
                }
            }
            return false;
        }
    }

}
