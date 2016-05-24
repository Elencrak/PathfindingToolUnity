using UnityEngine;
using System.Collections;

namespace Rodrigue
{
    public class GroupState : State
    {

        TeamLeader teamLeader;

        public GroupState(TeamLeader parAgent)
        {
            teamLeader = parAgent;
        }

        public override void Execute()
        {
            teamLeader.RegroupF();
            //execute
        }

    }
}

