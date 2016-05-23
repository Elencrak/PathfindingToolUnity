using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootDefensif : StateValentin
{

    TeamAgentValentin teamAgent;

    public ShootDefensif(TeamAgentValentin team)
    {
        teamAgent = team;
    }

    protected override void execute()
    {
        teamAgent.shootDefensifPlayer();
    }

}
