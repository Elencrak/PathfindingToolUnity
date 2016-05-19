using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoulpeMove : PoulpeState
{
    NavMeshAgent agent;
    GameObject player;

    public PoulpeMove(GameObject Player, NavMeshAgent Agent)
    {
        player = Player;
        agent = Agent;
    }

    public override void Step()
    {
        foreach (PoulpeTransition transition in transitions)
        {
            transition.Check();
        }
        if (player.GetComponent<Poulpe>().target == null)
        {
            int rand = Random.Range(0, player.GetComponent<Poulpe>().targets.Count);
            player.GetComponent<Poulpe>().target = player.GetComponent<Poulpe>().targets[rand];
        }
        agent.SetDestination(player.GetComponent<Poulpe>().target.transform.position);
    }
}
