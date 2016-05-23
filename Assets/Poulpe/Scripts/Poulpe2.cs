using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poulpe2 : MonoBehaviour
{
    private Vector3 begin;
    private GameObject leader;
    private GameObject bot2;
    private List<GameObject> players;
    private float startShoot;
    private float delayShoot = 1;
    private Vector3[] patrol;
    private int index;
    private float startDogge;
    private float delayDogge = 0.25f;
    // Use this for initialization
    void Start ()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        begin = transform.position;
        leader = transform.parent.GetChild(0).gameObject;
        bot2 = transform.parent.GetChild(2).gameObject;
        patrol = new Vector3[4];
        patrol[0] = new Vector3(18, 5.7f, -16);
        patrol[1] = new Vector3(-18, 5.7f, -16);
        patrol[2] = new Vector3(-18, 5.7f, 21);
        patrol[3] = new Vector3(18, 5.7f, 21);
        index = Random.Range(0, patrol.Length);
    }
	
	// Update is called once per frame
	void Update ()
    {
        foreach (GameObject pla in players)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, pla.transform.position - transform.position, out hit);
            if (hit.collider.tag == "Target" && hit.collider.gameObject != leader && hit.collider.gameObject != bot2)
            {
                if (startShoot + delayShoot <= Time.time)
                {
                    Shoot(hit.transform.gameObject);
                }
                break;
            }
        }
        if (Vector3.Distance(transform.position, patrol[index]) <= 1.0f)
        {
            index = Random.Range(0, patrol.Length);
        }
        if (startDogge + delayDogge <= Time.time)
        {
            GetComponent<NavMeshAgent>().SetDestination(patrol[index]);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GetComponent<NavMeshAgent>().Warp(begin);
        }
    }

    public void GetTargets(List<GameObject> pla)
    {
        players = pla;
    }

    void Shoot(GameObject hit)
    {
        startShoot = Time.time;
        transform.LookAt(CalcShootAngle(hit));
        GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward * 2, Quaternion.Euler(this.transform.eulerAngles)) as GameObject;
        bullet.GetComponent<bulletScript>().launcherName = "Poulpe";
    }

    Vector3 CalcShootAngle(GameObject hit)
    {
        Vector3 hitPos = hit.transform.position;
        float hitSpeed = hit.GetComponent<NavMeshAgent>().speed;
        float distance = Vector3.Distance(transform.position, hitPos);
        float bulletSpeed = 40;
        float erreur = 0.3f;
        float temps = distance / bulletSpeed;
        Vector3 hitPosArrive = hitPos + hit.transform.forward * hitSpeed * temps;
        float newDist = Vector3.Distance(transform.position, hitPosArrive);
        while (newDist - distance > erreur)
        {
            hitPos = hitPosArrive;
            distance = Vector3.Distance(transform.position, hitPos) - distance;
            temps = distance / bulletSpeed;
            hitPosArrive = hitPos + hit.transform.forward * hitSpeed * temps;
            newDist = Vector3.Distance(transform.position, hitPosArrive);
            distance = Vector3.Distance(transform.position, hitPos);
        }
        Vector3 point = hitPosArrive;
        return point;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Target" && collider.gameObject != leader && collider.gameObject != bot2)
        {
            if (startShoot + delayShoot <= Time.time)
            {
                Shoot(collider.gameObject);
            }
        }
        else if (collider.tag == "Bullet")
        {
            //GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet" && collider.GetComponent<bulletScript>().launcherName != "Poulpe")
        {
            startDogge = Time.time;
            Vector3 point = transform.position + transform.forward * 1.0f;
            if (Vector3.Distance(collider.transform.position, point) <= 2f)
            {
                GetComponent<NavMeshAgent>().SetDestination(transform.position + transform.right * 2);
                return;
            }
            point = transform.position + transform.forward * -1.0f;
            if (Vector3.Distance(collider.transform.position, point) <= 2f)
            {
                GetComponent<NavMeshAgent>().SetDestination(transform.position + transform.right * 2);
                return;
            }
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }
}