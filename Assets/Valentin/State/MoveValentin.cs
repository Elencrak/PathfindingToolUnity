using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveValentin : StateValentin
{



    TeamAgentValentin teamAgent;

    public MoveValentin(TeamAgentValentin team)
    {
        teamAgent = team;
    }

    protected override void execute()
    {
        teamAgent.seekPlayer();
    }

}
