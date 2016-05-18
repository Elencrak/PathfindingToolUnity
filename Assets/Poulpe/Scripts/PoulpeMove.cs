using UnityEngine;
using System.Collections;

public class PoulpeMove : PoulpeState
{
    NavMeshAgent agent;

    public PoulpeMove(NavMeshAgent Agent)
    {
        agent = Agent;
    }

    public override void Step()
    {
        if(target == null)
        {
            int rand = Random.Range(0, targets.Length);
            target = targets[rand];
        }
        agent.SetDestination(target.transform.position);
    }
}
