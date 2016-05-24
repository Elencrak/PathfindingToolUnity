using UnityEngine;
using System.Collections;
using System;

namespace JojoKiller
{
    public class Regroup : IStateLeader
    {
        public override IState execution()
        {
            IState temp = base.execution();
            if (temp == this)
            {
                Debug.Log("regroup");
                teamLeader.regroupAction();
            }
            return temp;
        }

        public Regroup(TeamLeader tm)
        {
            teamLeader = tm;
        }
    }
}