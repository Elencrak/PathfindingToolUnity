using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentPoulpe : MonoBehaviour
{

    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();

    private List<GameObject> players;
    private Vector3 begin;
    private float startShoot;
    private float delayShoot = 1;
    private GameObject bot1;
    private GameObject bot2;
    private int index;
    public Vector3[] patrol;

    public GameObject[] temp;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        /*
        //Select your pathfinding
        graph = new Pathfinding();
        graph.Load("poulpe");
        graph.setNeighbors();
        //


        target = GameObject.FindGameObjectWithTag("Target");
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position,graph);
        InvokeRepeating("UpdateRoad", 0.5f, 0.5f);
        Debug.Log(PathfindingManager.GetInstance().test);*/
        //bot1 = transform.parent.GetChild(1).gameObject;
        //bot2 = transform.parent.GetChild(2).gameObject;
        players = new List<GameObject>();
        temp = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject pla in temp)
        {
            if(pla != this.gameObject && pla != bot1 && pla != bot2)
            {
                players.Add(pla);
            }
        }
        begin = transform.position;
        //bot1.GetComponent<Poulpe2>().GetTargets(players);
        //bot2.GetComponent<Poulpe3>().GetTargets(players);
        patrol = new Vector3[4];
        patrol[0] = new Vector3(-67, 1, -67);
        patrol[1] = new Vector3(67, 1, -67);
        patrol[2] = new Vector3(67, 1, 67);
        patrol[3] = new Vector3(-67, 1, 67);
        index = 0;
    }
	
	// Update is called once per frame
	/*void Update ()
    {
        
        if(road.Count > 0)
        {
            currentTarget = road[0];
            if (Vector3.Distance(transform.position, currentTarget) < closeEnoughRange)
            {
                road.RemoveAt(0);
                currentTarget = road[0];
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
	}*/

    void Update()
    {
        foreach (GameObject pla in players)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, pla.transform.position - transform.position, out hit);
            if (hit.collider.tag == "Target" && hit.collider.gameObject != bot1 && hit.collider.gameObject != bot2)
            {
                if(startShoot + delayShoot <= Time.time)
                {
                    Shoot(hit.transform.gameObject);
                }
                break;
            }
        }
        if(Vector3.Distance(transform.position, patrol[index]) <= 1.0f)
        {
            index = Random.Range(0, patrol.Length);
        }
        GetComponent<NavMeshAgent>().SetDestination(patrol[index]);
    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Bullet":
                GetComponent<NavMeshAgent>().Warp(begin);
                break;
            case "Target":
                break;
        }
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
        float erreur = 0.5f;
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
        if(collider.tag == "Target" && collider.gameObject != bot1 && collider.gameObject != bot2)
        {
            if(startShoot + delayShoot <= Time.time)
            {
                Shoot(collider.gameObject);
            }
        }
        else if(collider.tag == "Bullet")
        {
            transform.position = new Vector3(Mathf.Cos(Time.time) / 10 + transform.position.x, transform.position.y, Mathf.Sin(Time.time) / 10 + transform.position.z);
        }
    }
}