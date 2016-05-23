using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootValentin : StateValentin
{

    TeamAgentValentin teamAgent;

    public ShootValentin(TeamAgentValentin team)
    {
        teamAgent = team;
    }

    protected override void execute()
    {
        teamAgent.shootPlayer();
    }

}
