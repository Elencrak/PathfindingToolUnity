using UnityEngine;
using System.Collections;

namespace Rodrigue
{
    public class SearchState : State
    {
        TeamLeader teamLeader;

        public SearchState(TeamLeader parAgent)
        {
            teamLeader = parAgent;
        }

        public override void Execute()
        {
            teamLeader.SearchF();
            //execute
        }

    }
}

