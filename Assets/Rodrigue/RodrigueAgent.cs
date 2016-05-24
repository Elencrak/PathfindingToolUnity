using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rodrigue;
public class RodrigueAgent : MonoBehaviour {

    float speed = 10.0f;
    float acceleration = 20.0f;
    GameObject rodriguePoints;
    public GameObject[] targetPossible;
    public NavMeshAgent navMeshAgent;
    float distance;
    float currentDistance;
    public GameObject currentTarget;
    public Vector3 spawnPoint;
    public List<GameObject> listOfTarget = new List<GameObject>();
    public List<GameObject> listOfFriends = new List<GameObject>();
    public List<GameObject> interestPoints;

    public List<GameObject> listOfBullets = new List<GameObject>();
    public float rateOfFire;

    public bool canShoot;
    public bool isDodging;
    public float timeSinLastShot;

    public string teamName = "RektByRodrigue";

    public int nbOfDeath;

    // Use this for initialization
    void Start () {
        timeSinLastShot = 0;
        canShoot = true;
        rodriguePoints = GameObject.Find("RodriguePoints");
        targetPossible = GameObject.FindGameObjectsWithTag("Target");
        interestPoints = new List<GameObject>();
        nbOfDeath = 0;
        foreach (Transform child in rodriguePoints.transform)
        {
            interestPoints.Add(child.gameObject);
        }

        foreach (GameObject temp in targetPossible)
        {
            if(temp != this.gameObject)
            {
                listOfTarget.Add(temp);
				if (temp.transform.parent.parent && temp.transform.parent.parent.GetComponent<TeamNumber>() && temp.transform.parent.parent.GetComponent<TeamNumber>().teamName == "RektByRodrigue")
                {
                    listOfTarget.Remove(temp);
                    listOfFriends.Add(temp);
                }
            }
        }
        //InvokeRepeating("GetTarget", 0.5f, 0.5f);
        currentTarget = listOfTarget[0];
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = acceleration;

        spawnPoint = transform.position;
        rateOfFire = 1;
        //InvokeRepeating("Patrol", 0.1f, 0.1f);
        InvokeRepeating("FindTarget", 0.1f, 0.1f);
        isDodging = false;

        navMeshAgent.SetDestination(interestPoints[Random.Range(0, interestPoints.Count)].transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        timeSinLastShot += Time.deltaTime;
        transform.LookAt(currentTarget.transform);
    }


    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        navMeshAgent.Warp(spawnPoint);
    //        navMeshAgent.SetDestination(currentTarget.transform.position);
    //    }
    //}

    void FindTarget()
    {
        foreach(GameObject player in listOfTarget){
            Vector3 direction = player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 100))
            {
                if(hit.transform.tag == "Target" && !hit.transform.gameObject.GetComponent<RodrigueDeath>())
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < Vector3.Distance(transform.position, currentTarget.transform.position))
                    {
                        transform.LookAt(player.transform);
                        currentTarget = player;
                    }
                    if (canShoot)
                    {
                        StartCoroutine(Shoot(player));
                    }
                }
            }
        }
    }

    IEnumerator Shoot(GameObject target)
    {
        timeSinLastShot = 0;
        canShoot = false;
        GameObject bullets = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward*1.5f + new Vector3(0, 1.5f, 0), Quaternion.identity) as GameObject;
        Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), bullets.GetComponent<CapsuleCollider>());
        //Vector3 velocity = target.GetComponent<Rigidbody>().velocity;
        //float t = Vector3.Distance(transform.position, target.transform.position) / 40f
        //bullets.transform.LookAt(target.transform.position + velocity * t);
        Vector3 temp;
        float t = Vector3.Distance(transform.position, target.transform.position) / (bullets.GetComponent<bulletScript>().speed);
        if (target.GetComponent<NavMeshAgent>())
        {
            temp = target.transform.position + (target.GetComponent<NavMeshAgent>().velocity * (t));
        }
        else
        {
            temp = target.transform.position + target.GetComponent<Rigidbody>().velocity;
        }
        bullets.transform.LookAt(temp);
        bullets.GetComponent<bulletScript>().launcherName = teamName;
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }

    public void Patrol()
    {
        
        for(int i =0; i < interestPoints.Count; i++)
        {
            if (Vector3.Distance(transform.position, interestPoints[i].transform.position) < 1)
            {
                if (i != interestPoints.Count - 1)
                {
                    if (!isDodging)
                    {
                        
                        navMeshAgent.SetDestination(interestPoints[i + 1].transform.position);
                    }
                    
                }
                else
                {
                    if (!isDodging)
                    {
                        navMeshAgent.SetDestination(interestPoints[0].transform.position);
                    }
                }
            }
        }
    }

    public bool Active()
    {
        return true;
    }

    public void SearchAndDestroy()
    {
        if (!isDodging)
        {
            navMeshAgent.SetDestination(currentTarget.transform.position);
        }
    }

    public void Idle()
    {
        navMeshAgent.Stop();
    }

    void GetTarget()
    {
        if(currentTarget == this.gameObject)
        {
            currentTarget = listOfTarget[1];
        }
        currentDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
        for (int i =0; i < listOfTarget.Count; i++)
        {
            distance = Vector3.Distance(transform.position, listOfTarget[i].transform.position);
            if(distance < currentDistance && listOfTarget[i]!=this.gameObject)
            {
                currentTarget = listOfTarget[i];
            }
        }
        //navMeshAgent.SetDestination(currentTarget.transform.position);
    }

    public void Regroup()
    {
        float dist = 0;
        foreach(GameObject parObject in listOfFriends)
        {
            if(Vector3.Distance(transform.position, parObject.transform.position) > dist)
            {
                navMeshAgent.SetDestination(parObject.transform.position);
                dist = Vector3.Distance(transform.position, parObject.transform.position);
                
            }
        }
    }

    void ChangeTarget()
    {
        currentDistance = 99999f;
        for (int i = 0; i < listOfTarget.Count; i++)
        {
            distance = Vector3.Distance(transform.position, listOfTarget[i].transform.position);
            if (distance < currentDistance && listOfTarget[i] != this.gameObject)
            {
                currentTarget = listOfTarget[i];
                
            }
        }
        //navMeshAgent.SetDestination(currentTarget.transform.position);
        if(listOfTarget.Count == 0)
        {
            Debug.Log("Rodrigue > All");
            navMeshAgent.Stop();
        }
    }
}
