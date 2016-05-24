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
        if (timerLength + lastTime < Time.time || Vector3.Distance(player.transform.position, destination) <= 1)
        {
            lastTime = Time.time;
            defineDestination();
        }
        else
        {
            player.GetComponent<Will_IA_M2>().move(destination);
            
        }
    }


    void defineDestination()
    {

        RaycastHit hitRight;
        RaycastHit hitLeft;
        if (Physics.Raycast(player.transform.position, player.transform.right, out hitRight, distance))
        {
            if (Physics.Raycast(player.transform.position, player.transform.right * -1, out hitLeft, distance))
            {
                if (Vector3.Distance(player.transform.position, hitRight.point) > Vector3.Distance(player.transform.position, hitLeft.point))
                {
                    destination = hitRight.point;
                }
                else
                destination = hitLeft.point;
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


    //void defineDestination()
    //{
    //    Vector3 targetDist = TeamManagerWill.instance.mainTarget.transform.position; 
    //    if (Vector3.Distance(targetDist, player.transform.position) < distance)
    //    {
    //        destination = player.transform.position;
    //        destination = (player.transform.position - targetDist) * distance;
    //        destination.y = player.transform.position.y;
    //        return;
    //    }

    //    RaycastHit hit;        
    //    if (Physics.Raycast(player.transform.position, player.transform.right, out hit, 0.5f))
    //    {
    //            //Debug.Log(hit.collider.gameObject.layer);
    //        if (hit.collider.gameObject.layer == 20)
    //        {
    //            //destination = player.transform.position + (player.transform.right*-1 * distance);
    //            destination = TeamManagerWill.instance.mainTarget.transform.position;
    //        }
    //        else
    //        {
    //            destination = player.transform.position + (player.transform.right * distance);
    //        }
    //    }
    //    else
    //    {
    //        destination = player.transform.position + (player.transform.right * distance);
    //    }

    //}
}
