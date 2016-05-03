using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poulpe3 : MonoBehaviour
{
    private Vector3 begin;
    private GameObject leader;
    private GameObject bot1;
    private List<GameObject> players;
    private float startShoot;
    private float delayShoot = 1;
    // Use this for initialization
    void Start()
    {
        begin = transform.position;
        leader = transform.parent.GetChild(0).gameObject;
        bot1 = transform.parent.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject pla in players)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, pla.transform.position - transform.position, out hit);
            if (hit.collider.tag == "Target" && hit.collider.gameObject != leader && hit.collider.gameObject != bot1)
            {
                if (startShoot + delayShoot <= Time.time)
                {
                    Shoot(hit.transform.position);
                }
                break;
            }
        }
        Vector3 dest = new Vector3(1, 15, 2);
        transform.eulerAngles = leader.transform.eulerAngles;
        GetComponent<NavMeshAgent>().SetDestination(dest);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetComponent<NavMeshAgent>().Warp(begin);
        }
    }

    public void GetTargets(List<GameObject> pla)
    {
        players = pla;
    }

    void Shoot(Vector3 hit)
    {
        startShoot = Time.time;
        transform.LookAt(hit);
        GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward * 2, Quaternion.Euler(this.transform.eulerAngles)) as GameObject;
        bullet.GetComponent<bulletScript>().launcherName = "Poulpe";
    }
}