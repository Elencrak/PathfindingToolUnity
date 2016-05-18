using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RodrigueAgentNew : MonoBehaviour
{
    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;

    private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();

    public GameObject[] targetPossible;
    float distance;
    float currentDistance;
    public Vector3 currentTarget;
    public Vector3 spawnPoint;
    public List<GameObject> listOfTarget = new List<GameObject>();

    public List<GameObject> listOfBullets = new List<GameObject>();
    public float rateOfFire;

    public bool canShoot;
    public string teamName = "RektByRodrigue";

    // Use this for initialization
    void Start()
    {
        graph = new Pathfinding();
        graph.Load("PathfindingRodrigueTest");
        graph.setNeighbors();
        
        
        target = GameObject.FindGameObjectWithTag("Target");
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
        road = PathfindingManager.GetInstance().SmoothRoad(road);

        canShoot = true;
        //targetPossible = GameObject.FindGameObjectsWithTag("Target");
        //foreach (GameObject temp in targetPossible)
        //{
        //    if (temp != this.gameObject)
        //    {
        //        //if(temp.transform.parent.GetComponent<TeamNumber>().teamName != "RektByRodrigue")
        //        //{
        //        listOfTarget.Add(temp);
        //        //}
        //    }
        //}
        ////InvokeRepeating("GetTarget", 0.5f, 0.5f);
        //currentTarget = targetPossible[0];
        spawnPoint = transform.position;
        rateOfFire = 1;
        //InvokeRepeating("FindTarget", 0.1f, 0.1f);
        InvokeRepeating("UpdateRoad", 0.5f, 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        if (road.Count > 0)
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
    }

    void UpdateRoad()
    {
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
        road = PathfindingManager.GetInstance().SmoothRoad(road);
        while (true) ;
    }

    IEnumerator Shoot(GameObject target)
    {
        canShoot = false;
        GameObject bullets = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward * 2.0f + new Vector3(0, 1.5f, 0), Quaternion.identity) as GameObject;
        Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), bullets.GetComponent<CapsuleCollider>());
        Vector3 velocity = target.GetComponent<Rigidbody>().velocity;
        float t = Vector3.Distance(transform.position, target.transform.position) / 40f;

        bullets.transform.LookAt(target.transform.position + velocity * t);
        bullets.GetComponent<bulletScript>().launcherName = teamName;
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }

    //void FindTarget()
    //{
    //    foreach (GameObject player in listOfTarget)
    //    {
    //        Vector3 direction = player.transform.position - transform.position;
    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, direction, out hit, 100))
    //        {
    //            if (hit.transform.tag == "Target")
    //            {
    //                if (Vector3.Distance(transform.position, player.transform.position) < Vector3.Distance(transform.position, currentTarget.transform.position))
    //                {
    //                    transform.LookAt(player.transform);
    //                    currentTarget = player;

    //                }
    //                if (canShoot)
    //                {
    //                    StartCoroutine(Shoot(player));
    //                }
    //            }
    //        }
    //    }
    //}
}
