using UnityEngine;
using System.Collections;

public class PoulpeIdle : PoulpeState
{
    GameObject player;
    NavMeshAgent agent;
    public Vector3 destination;

    public PoulpeIdle(GameObject Player, NavMeshAgent Agent)
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
        if(Vector3.Distance(player.transform.position, destination) <= 1.0f)
        {
            float rand = Random.Range(0.0f, 10.0f) - 5.0f;
            float rand2 = Random.Range(0.0f, 10.0f) - 5.0f;
            destination.x = player.transform.position.x + rand;
            destination.y = player.transform.position.y;
            destination.z = player.transform.position.z + rand2;
            agent.SetDestination(destination);
        }
    }
}
