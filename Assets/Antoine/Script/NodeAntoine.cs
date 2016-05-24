using UnityEngine;
using System.Collections;

public abstract class NodeAntoine
{

    public GameObject player;

    public NodeAntoine()
    {

    }

    public void SetPlayer(GameObject thePlayer)
    {
        player = thePlayer;
    }

    public abstract bool Execute();

}
