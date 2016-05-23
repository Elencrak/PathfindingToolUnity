using UnityEngine;
using System.Collections;
using System;

namespace JojoKiller
{
    public class Reform : IStateLeader {


        public override IState execution()
        {
            IState temp = base.execution();
            if (temp == this)
            {
                Debug.Log("reform");
                teamLeader.reformAction();
            }
            return temp;
        }


        public Reform(TeamLeader tm)
        {
            teamLeader = tm;
        }
    }
}
