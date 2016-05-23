using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateAttaque : StateValentin
{

    TeamAgentValentin teamAgent;

    public StateAttaque(TeamAgentValentin team)
    {
        teamAgent = team;
    }

    protected override void execute()
    {
        teamAgent.shootPlayer();
    }

}