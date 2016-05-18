using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RodrigueAgent : MonoBehaviour {

    public float speed = 10.0f;
    public float acceleration = 20.0f;
    public GameObject[] targetPossible;
    public NavMeshAgent navMeshAgent;
    float distance;
    float currentDistance;
    public GameObject currentTarget;
    public Vector3 spawnPoint;
    public List<GameObject> listOfTarget = new List<GameObject>();

    public GameObject[] interestPoints = new GameObject[2];

    public List<GameObject> listOfBullets = new List<GameObject>();
    public float rateOfFire;

    public bool canShoot;
    public bool isDodging;

    public string teamName = "RektByRodrigue";
    

    // Use this for initialization
    void Start () {
        interestPoints[0] = GameObject.Find("left plateforme");
        interestPoints[1] = GameObject.Find("right plateforme");
        canShoot = true;
        targetPossible = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject temp in targetPossible)
        {
            if(temp != this.gameObject)
            {
                //if(temp.transform.parent.GetComponent<TeamNumber>().teamName != "RektByRodrigue")
                //{
                    listOfTarget.Add(temp);
                //}
                
            }
        }
        //InvokeRepeating("GetTarget", 0.5f, 0.5f);
        currentTarget = targetPossible[0];
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = acceleration;

        spawnPoint = transform.position;
        navMeshAgent.SetDestination(interestPoints[0].transform.position);
        rateOfFire = 1;
        InvokeRepeating("FindTarget", 0.1f, 0.1f);
        isDodging = false;
    }
	
    //void OnTriggerEnter(Collider parOther)
    //{
    //    if(parOther.tag == "Bullet" && parOther.GetComponent<bulletScript>().launcherName !="RektByRodrigue")
    //    {
    //        listOfBullets.Add(parOther.gameObject);
    //        Vector3 bulletForward = parOther.transform.forward;
    //        RaycastHit hit;
    //        Debug.DrawRay(parOther.transform.position, bulletForward*100, Color.red, 0.5f);
    //        if(Physics.Raycast(parOther.transform.position, bulletForward, out hit, 100))
    //        {

    //        }
    //        else
    //        {
                
    //            StartCoroutine(Dodge());
    //        }
    //    }
    //}

    //IEnumerator Dodge()
    //{
    //    isDodging = true;
    //    navMeshAgent.Stop();
    //    yield return new WaitForSeconds(1f);
    //    navMeshAgent.Resume();
    //    isDodging = false;
    //}

    //void OnTriggerExit(Collider parOther)
    //{
    //    if (parOther.tag == "Bullet" && parOther.GetComponent<bulletScript>().launcherName != "RektByRodrigue")
    //    {
    //        listOfBullets.Remove(parOther.gameObject);
    //    }
    //}

	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, interestPoints[0].transform.position) < 1 && !isDodging)
        {
            navMeshAgent.SetDestination(interestPoints[1].transform.position);
        }
        else if (Vector3.Distance(transform.position, interestPoints[1].transform.position) < 1 && !isDodging)
        {
            navMeshAgent.SetDestination(interestPoints[0].transform.position);
        }
        transform.LookAt(currentTarget.transform);
    }


    void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "Target")
        //{
        //    for (int i = 0; i < listOfTarget.Count; i++)
        //    {
        //        if(listOfTarget[i] == collision.gameObject)
        //        {
        //            listOfTarget.RemoveAt(i);
        //        } 
        //    }
        //    ChangeTarget();
        //}

        if (collision.gameObject.tag == "Bullet")
        {
            navMeshAgent.Warp(spawnPoint);
            navMeshAgent.SetDestination(interestPoints[0].transform.position);
        }
    }

    void FindTarget()
    {
        foreach(GameObject player in listOfTarget){
            Vector3 direction = player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 100))
            {
                if(hit.transform.tag == "Target")
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
        canShoot = false;
        GameObject bullets = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward*2.0f + new Vector3(0, 1.5f, 0), Quaternion.identity) as GameObject;
        Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), bullets.GetComponent<CapsuleCollider>());
        Vector3 velocity = target.GetComponent<Rigidbody>().velocity;
        float t = Vector3.Distance(transform.position, target.transform.position) / 40f;

        bullets.transform.LookAt(target.transform.position + velocity * t);
        bullets.GetComponent<bulletScript>().launcherName = teamName;
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
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
        navMeshAgent.SetDestination(currentTarget.transform.position);
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
        navMeshAgent.SetDestination(currentTarget.transform.position);
        if(listOfTarget.Count == 0)
        {
            Debug.Log("Rodrigue > All");
            navMeshAgent.Stop();
        }
    }
}
