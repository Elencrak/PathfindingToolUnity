using UnityEngine;
using System.Collections;
using System;

public class PoulpeTaskMove : PoulpeNode
{
    GameObject player;
    NavMeshAgent agent;

    public override bool DoIt()
    {
        player.GetComponent<Poulpe>().target = player.GetComponent<Poulpe>().targets[ChooseTarget()];
        agent.SetDestination(player.GetComponent<Poulpe>().target.transform.position);
        player.GetComponent<Poulpe>().destination = new Vector3(0, -100, 0);
        return true;
    }

    int ChooseTarget()
    {
        int index = 0;
        bool first = false;
        for (int i = 0; i < player.GetComponent<Poulpe>().targets.Count; i++)
        {
            if (!first)
            {
                first = true;
            }
            if (Vector3.Distance(player.transform.position, player.GetComponent<Poulpe>().targets[i].transform.position) < Vector3.Distance(player.transform.position, player.GetComponent<Poulpe>().targets[index].transform.position))
            {
                index = i;
            }
        }
        return index;
    }

    public PoulpeTaskMove(GameObject Player)
    {
        player = Player;
        agent = player.GetComponent<NavMeshAgent>();
    }
}
