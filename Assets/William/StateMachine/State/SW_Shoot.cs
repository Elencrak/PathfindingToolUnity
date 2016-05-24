using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SW_Shoot : StateWill {

    public SW_Dodge dodge;
    GameObject player;
    GameObject currentTarget;
    GameObject bullet;
    float lastShoot = 0;
    public float shootCooldown;

    public SW_Shoot(int id, GameObject pBullet, float shootSpeed, SW_Dodge dodgeState=null) :base(id)
    {
        player = TeamManagerWill.instance.members[id].gameObject;
        shootCooldown = shootSpeed;
        bullet = pBullet;
        currentTarget = TeamManagerWill.instance.mainTarget;
        if (dodgeState != null)
        {
            dodge = dodgeState;
        }
    }

    public override StateWill execute()
    {
        if (lastShoot + shootCooldown > Time.time)
        {
            
            if (dodge!=null)
            dodge.execute();
        }
        else
        {
            shoot();
            StateWill next = checkTransition();
            if (next != null)
                return next;
        }

        return null;
    }
    
    

    void shoot()
    {
        currentTarget = TeamManagerWill.instance.mainTarget;
        if (currentTarget == null)
            return;
        RaycastHit hit;
        Vector3 dir = currentTarget.transform.position - player.transform.position;
        if (Physics.Raycast(player.transform.position, dir, out hit))
        {
            //Debug.Log("ray " + hit.collider.name);
            if (hit.collider.gameObject == currentTarget)
            {
                //strafe();
                shootBullet(currentTarget);
            }
            else
            {
                // test all enemy and shoot anyone we can see
                GameObject anyTarget = TeamManagerWill.instance.getTargetCanShoot(idAgent);
                if(anyTarget!=null)
                shootBullet(anyTarget);
            }
        }

    }

    void shootBullet(GameObject targ)
    {
        lastShoot = Time.time;
        player.GetComponent<Will_IA_M2>().shoot(targ);
    }
}
