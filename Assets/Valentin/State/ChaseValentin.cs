using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChaseValentin : StateValentin
{



    TeamAgentValentin teamAgent;

    public ChaseValentin(TeamAgentValentin team)
    {
        teamAgent = team;
    }

    protected override void execute()
    {
        teamAgent.chasePlayer();
    }

}
