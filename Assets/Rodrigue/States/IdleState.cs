using UnityEngine;
using System.Collections;

namespace Rodrigue
{
    public class IdleState : State
    {

        TeamLeader teamLeader;

        public IdleState(TeamLeader parAgent)
        {
            teamLeader = parAgent;
        }

        public override void Execute()
        {
            teamLeader.IdleF();
            //execute
        }

    }
}

