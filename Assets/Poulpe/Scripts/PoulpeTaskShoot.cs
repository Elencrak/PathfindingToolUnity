using UnityEngine;
using System.Collections;
using System;

public class PoulpeTaskShoot : PoulpeNode
{
    GameObject player;

    public override bool DoIt()
    {
        Shoot(player.GetComponent<Poulpe>().target);
        player.GetComponent<Poulpe>().startShoot = Time.time;
        return true;
    }

    void Shoot(GameObject hit)
    {
        player.transform.LookAt(CalcShootAngle(hit));
        GameObject bullet = player.GetComponent<Poulpe>().Instantiation();
        bullet.GetComponent<bulletScript>().launcherName = "Poulpe";
    }

    Vector3 CalcShootAngle(GameObject hit)
    {
        Vector3 hitPos = hit.transform.position;
        float hitSpeed = Vector3.Distance(player.GetComponent<Poulpe>().lastTargetPos, player.GetComponent<Poulpe>().targetPos);
        float distance = Vector3.Distance(player.transform.position, hitPos);
        float bulletSpeed = 40;
        float erreur = 0.3f;
        float temps = distance / bulletSpeed;
        Vector3 hitPosArrive = hitPos + hit.transform.forward * hitSpeed * temps;
        float newDist = Vector3.Distance(player.transform.position, hitPosArrive);
        while (newDist - distance > erreur)
        {
            hitPos = hitPosArrive;
            distance = Vector3.Distance(player.transform.position, hitPos) - distance;
            temps = distance / bulletSpeed;
            hitPosArrive = hitPos + hit.transform.forward * hitSpeed * temps;
            newDist = Vector3.Distance(player.transform.position, hitPosArrive);
            distance = Vector3.Distance(player.transform.position, hitPos);
        }
        Vector3 point = hitPosArrive;
        return point;
    }

    public PoulpeTaskShoot(GameObject Player)
    {
        player = Player;
    }
}
