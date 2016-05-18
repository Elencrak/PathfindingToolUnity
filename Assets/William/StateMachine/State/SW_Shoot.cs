using UnityEngine;
using System.Collections;

public class SW_Shoot : StateWill {
    
    GameObject player;
    GameObject currentTarget;
    GameObject bullet;
    float lastShoot = 0;
    public float shootCooldown;

    public SW_Shoot(int id, GameObject pBullet, float shootSpeed)
    {
        player = TeamManagerWill.instance.members[id].gameObject;
        shootCooldown = shootSpeed;
        bullet = pBullet;
        currentTarget = TeamManagerWill.instance.mainTarget;
    }

    public override StateWill execute()
    {
        StateWill next = checkTransition();
        if (next != null)
            return next;
        shoot();
        return null;
    }

    protected override StateWill checkTransition()
    {
        StateWill next = null;
        foreach (TransitionWill trans in transition)
        {
            next = trans.check();
            if (next != null)
            {
                return next;
            }
        }
        return null;
    }

    void shoot()
    {
        if (currentTarget == null)
            return;
        if (lastShoot + shootCooldown > Time.time)
            return;
        currentTarget = TeamManagerWill.instance.mainTarget;
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
            }

        }

    }

    void shootBullet(GameObject targ)
    {
        lastShoot = Time.time;
        GameObject spawnedBullet = (GameObject)Instantiate(bullet, player.transform.position, player.transform.rotation);
        spawnedBullet.transform.LookAt(targ.transform.position + (targ.GetComponent<NavMeshAgent>().velocity.normalized));
        spawnedBullet.GetComponent<bulletScript>().launcherName = "TeamWill";
        Physics.IgnoreCollision(GetComponent<BoxCollider>(), spawnedBullet.GetComponent<CapsuleCollider>());

    }
}
