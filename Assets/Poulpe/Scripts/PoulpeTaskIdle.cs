using UnityEngine;
using System.Collections;

public class PoulpeTaskIdle : PoulpeNode
{
    GameObject player;

    public override bool DoIt()
    {
        if(player.GetComponent<Poulpe>().destination == new Vector3(0, -100, 0))
        {
            player.GetComponent<Poulpe>().destination = player.transform.position;
        }
        if (Vector3.Distance(player.transform.position, player.GetComponent<Poulpe>().destination) <= 1.0f)
        {
            float rand = Random.Range(0.0f, 10.0f) - 5.0f;
            float rand2 = Random.Range(0.0f, 10.0f) - 5.0f;
            player.GetComponent<Poulpe>().destination.x = player.transform.position.x + rand;
            player.GetComponent<Poulpe>().destination.y = player.transform.position.y;
            player.GetComponent<Poulpe>().destination.z = player.transform.position.z + rand2;
            player.GetComponent<NavMeshAgent>().SetDestination(player.GetComponent<Poulpe>().destination);
        }
        return true;
    }

    public PoulpeTaskIdle(GameObject Player)
    {
        player = Player;
    }
}
