using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdleValentin : StateValentin
{

    TeamAgentValentin teamAgent;

    public IdleValentin(TeamAgentValentin team)
    {
        teamAgent = team;
    }



    protected override void execute()
    {
        teamAgent.idlePlayer();
    }



}
