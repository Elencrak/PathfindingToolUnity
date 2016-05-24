using UnityEngine;
using System.Collections;
using System;

public class PoulpeTaskCoolDown : PoulpeNode
{
    GameObject player;
    float delayShoot = 1.0f;

    public override bool DoIt()
    {
        float startShoot = player.GetComponent<Poulpe>().startShoot;
        return startShoot + delayShoot <= Time.time;
    }

    public PoulpeTaskCoolDown(GameObject Player)
    {
        player = Player;
    }
}
