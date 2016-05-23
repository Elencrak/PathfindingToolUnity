using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoulpeDogge : PoulpeState
{
    GameObject player;
    public Vector3 bullet;

    public PoulpeDogge(GameObject Player)
    {
        player = Player;
    }

    public override void Step()
    {
        foreach (PoulpeTransition transition in transitions)
        {
            transition.Check();
        }
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            rand--;
        }
        player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position + bullet * rand);
    }
}
