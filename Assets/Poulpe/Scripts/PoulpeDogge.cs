using UnityEngine;
using System.Collections;
using System;

public class PoulpeDogge : PoulpeState
{
    GameObject player;

    public PoulpeDogge(GameObject Player)
    {
        player = Player;
    }

    public override void Step()
    {
        player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }
}
