using UnityEngine;
using System.Collections;
using System;

public class PoulpeTaskEnemySpotted : PoulpeNode
{
    GameObject player;

    public override bool DoIt()
    {
        for (int i = 0; i < player.GetComponent<Poulpe>().targets.Count; i++)
        {
            RaycastHit hit;
            Physics.Raycast(player.transform.position, player.GetComponent<Poulpe>().targets[i].transform.position - player.transform.position, out hit);
            if (hit.collider.tag == "Target" && hit.collider.name != "Poulpe")
            {
                player.GetComponent<Poulpe>().target = player.GetComponent<Poulpe>().targets[i];
                return true;
            }
        }
        return false;
    }

    public PoulpeTaskEnemySpotted(GameObject Player)
    {
        player = Player;
    }
}
