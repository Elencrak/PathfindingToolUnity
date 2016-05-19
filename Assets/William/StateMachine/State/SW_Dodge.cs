using UnityEngine;
using System.Collections;

public class SW_Dodge : StateWill {
    GameObject player;
    public float timerLength=1;
    float distance;
    float lastTime = 0;
    float speed;
    Vector3 destination;
    public SW_Dodge(int id,float pSpeed, float pDist, float timer ,string graphName):base(id)
    {
        speed = pSpeed;
        distance = pDist;
        timerLength = timer;
        player = TeamManagerWill.instance.members[id].gameObject;
    }

    public override StateWill execute()
    {
        StateWill next = checkTransition();
        if (next != null)
            return next;
        Dodge();
        return null;
    }
    
    void Dodge()
    {
        if (timerLength + lastTime < Time.time || Vector3.Distance(player.transform.position, destination) < distance)
        {
            lastTime = Time.time;
            defineDestination();
        }
        else
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, destination, speed * Time.deltaTime);
            //playerController.SimpleMove(target.transform.position.normalized * speed * Time.deltaTime);
            player.transform.LookAt(destination);
        }
    }

    void defineDestination()
    {
        Vector3 targetDist = TeamManagerWill.instance.mainTarget.transform.position; 
        if (Vector3.Distance(targetDist, player.transform.position) < distance)
        {
            destination = player.transform.position;
            destination = (player.transform.position - targetDist) * distance;
            destination.y = player.transform.position.y;
            return;
        }

        RaycastHit hit;        
        if (Physics.Raycast(player.transform.position, player.transform.right, out hit, 0.5f))
        {
                //Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == 20)
            {
                //destination = player.transform.position + (player.transform.right*-1 * distance);
                destination = TeamManagerWill.instance.mainTarget.transform.position;
            }
            else
            {
                destination = player.transform.position + (player.transform.right * distance);
            }
        }
        else
        {
            destination = player.transform.position + (player.transform.right * distance);
        }
        
    }
}
