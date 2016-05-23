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
        player.GetComponent<Poulpe>().target = player.GetComponent<Poulpe>().targets[ChooseTarget()];
        agent.SetDestination(player.GetComponent<Poulpe>().target.transform.position);
    }

    int ChooseTarget()
    {
        int index = 0;
        bool first = false;
        for(int i = 0; i < player.GetComponent<Poulpe>().targets.Count; i++)
        {
            if(!first)
            {
                first = true;
            }
            if(Vector3.Distance(player.transform.position, player.GetComponent<Poulpe>().targets[i].transform.position) < Vector3.Distance(player.transform.position, player.GetComponent<Poulpe>().targets[index].transform.position))
            {
                index = i;
            }
        }
        return index;
    }
}
