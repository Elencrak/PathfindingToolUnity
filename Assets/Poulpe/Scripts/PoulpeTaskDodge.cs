using UnityEngine;
using System.Collections;

public class PoulpeTaskDodge : PoulpeNode
{
    GameObject player;
    public override bool DoIt()
    {
        if(player.GetComponent<Poulpe>().bullet != null)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                rand--;
            }
            player.transform.position += player.GetComponent<Poulpe>().bullet.transform.right * 10 * Time.deltaTime * rand;
        }
        return true;
    }

    public PoulpeTaskDodge(GameObject Player)
    {
        player = Player;
    }
}
