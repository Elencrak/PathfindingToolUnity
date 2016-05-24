using UnityEngine;
using System.Collections;
using System;

public class PoulpeTaskBullet : PoulpeNode
{
    GameObject player;

    public override bool DoIt()
    {
        return player.GetComponent<Poulpe>().thereIsBullet;
    }

    public PoulpeTaskBullet(GameObject Player)
    {
        player = Player;
    }
}
