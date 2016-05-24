using UnityEngine;
using System.Collections;
using System;

namespace Rodrigue
{
    public class ReloadNode : Node
    {
        public override bool Execute()
        {
            Debug.Log("toto");
            return true;
        }
    }

}
