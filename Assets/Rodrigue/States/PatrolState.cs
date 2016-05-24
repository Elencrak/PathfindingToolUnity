using UnityEngine;
using System.Collections;
using System;

namespace Rodrigue
{
    public class PatrolState : State
    {

        TeamLeader teamLeader;

        public PatrolState(TeamLeader parAgent)
        {
            teamLeader = parAgent;
        }
        public override void Execute()
        {
			
            teamLeader.PatrolF();
            //execute
        }
    }
}

