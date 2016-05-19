using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentAntoine : MonoBehaviour
{
    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;

    public GameObject[] enemies;

    public GameObject nodes;

    public float rate = 1.0f;
    public float lastShoot = 0.0f;
    private bool canShoot = true;
    private bool isShooting = false;

    bool finished = false;

    public Material cube;

    public Vector3 SpawnPos;

    public GameObject[] points;
    private int index = 0;
    private Vector3 PathPoint;

    public GameObject bullet;
    public GameObject spawnBullet;
    public GameObject spawnBulletRotation;

    private bool esquive;

    private float offset = 0;

    public GameObject bro1;
    public GameObject bro2;

    private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();

    private bool isMoving = false;
    private Vector3 lastPoint;

    StateMachineAntoine theStateMachine;

    // Use this for initialization
    void Start()
    {
        theStateMachine = new StateMachineAntoine(gameObject);

        graph = new Pathfinding();
        graph.Load("antoinePathFinding");
        graph.setNeighbors();

        SpawnPos = transform.position;

        int rand = Random.Range(0, nodes.transform.childCount - 1);

        target = nodes.transform.GetChild(rand).gameObject;

        //PathPoint = points[index].transform.position;

        InvokeRepeating("ChangeColor", 0.5f, 0.1f);

        //InvokeRepeating("FindNewTarget", 0.5f, 0.5f);

        InvokeRepeating("Calc", 0f, 1.0f);

        enemies = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == gameObject)
            {
                enemies[i] = null;
            }
            else if (enemies[i] == bro1)
            {
                enemies[i] = null;
            }
            else if (enemies[i] == bro2)
            {
                enemies[i] = null;
            }
        }

        isMoving = false;
        isShooting = false;
        canShoot = true;
        bullet = Resources.Load("Bullet") as GameObject;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(theStateMachine.GetCurrentState().currentType);
        theStateMachine.Update();

        offset = 7 * Mathf.Sin(Time.time * 5);

         if (canShoot && target != null)
         {
            RaycastHit hit;
            if (Physics.Raycast(spawnBullet.transform.position, (target.transform.position - transform.position).normalized, out hit, 5000.0f))
            {
                if (hit.transform.tag == "Target" && hit.transform.gameObject != bro1 && hit.transform.gameObject != bro2 && hit.transform.gameObject != gameObject)
                { 
                    spawnBulletRotation.transform.LookAt(target.transform.position);
                    isShooting = true;

                    GameObject go = Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity) as GameObject;
                    go.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
                    go.transform.LookAt(target.transform.position + target.transform.forward);
                    canShoot = false;
                    lastShoot = 0.0f;
                }
            }
         }
         else
         {
             isShooting = false;
             lastShoot += Time.deltaTime;
             if (lastShoot >= rate)
             {
                 lastShoot = 0.0f;
                 canShoot = true;
             }
             if (!target)
                 spawnBulletRotation.transform.LookAt(lastPoint);
         }
    }

    void Calc()
    {
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
        road = PathfindingManager.GetInstance().SmoothRoad(road);
    }

    public bool MustChase()
    {
        foreach (GameObject g in enemies)
        {
            RaycastHit hit;
            if (g != null && Physics.Raycast(spawnBullet.transform.position, (target.transform.position - transform.position).normalized, out hit, 5000.0f))
            {
                if (hit.transform.gameObject.tag == "Target" && hit.transform.gameObject != bro1 && hit.transform.gameObject != bro2 && hit.transform.gameObject != gameObject)
                {
                    Debug.Log("must chase");
                    target = hit.transform.gameObject;
                    return true;
                }
                else
                {
                    Debug.Log(hit.transform.name);
                }
            }
        }
        return false;
    }

    public void Chase()
    {
       // Debug.Log("chase");
        transform.position = Vector3.MoveTowards(transform.position, road[0], speed * Time.deltaTime);
        transform.LookAt(road[0]);
        if (Vector3.Distance(transform.position, road[0]) <= 0.1f)
        {
            // Debug.Log("change point");
            road.RemoveAt(0);
        }
    }

    public void Patrol()
    {
       // Debug.Log("patrol");
        if (Vector3.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            //Debug.Log("change target");
            int rand = Random.Range(0, nodes.transform.childCount - 1);

            target = nodes.transform.GetChild(rand).gameObject;

            road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
            road = PathfindingManager.GetInstance().SmoothRoad(road);
        }
        else if(Vector3.Distance(transform.position, road[0]) <= 0.1f)
        {
           // Debug.Log("change point");
            road.RemoveAt(0);
            
        }
        else
        {
           // Debug.Log("move to point");
            transform.position = Vector3.MoveTowards(transform.position, road[0], speed * Time.deltaTime);
            transform.LookAt(road[0]);
        }
    }

    public bool NoTarget()
    {
       // Debug.Log("no target");
        foreach (GameObject g in enemies)
        {
            RaycastHit hit;
            if (Physics.Raycast(spawnBullet.transform.position, (target.transform.position - transform.position).normalized, out hit, 1000.0f))
            {
                if (hit.transform.tag == "Target" && hit.transform.gameObject != bro1 && hit.transform.gameObject != bro2 && hit.transform.gameObject != gameObject)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool HaveShoot()
    {
        return !canShoot;
    }

    public bool GetCanShoot()
    {
        return canShoot;
    }

    public void Shoot()
    {
        Debug.Log("shoot");
        spawnBulletRotation.transform.LookAt(target.transform.position);
        isShooting = true;

        GameObject go = Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity) as GameObject;
        // go.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
        go.transform.LookAt(target.transform.position + target.transform.forward);
        canShoot = false;
        lastShoot = 0.0f;
    }

    void ChangeColor()
    {
        if (!finished)
            GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1), Random.Range(0f, 1), Random.Range(0f, 1));
    }

    void OnCollisionEnter(Collision other)
    {
        /*if(other.gameObject.tag == "Target" && other.gameObject == target)
        {
            FindInTargets(other.gameObject);
            FindNewTarget();
            if (target == null)
            {
                finished = true;
                GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Cube").transform.position);
                GetComponent<MeshRenderer>().material = cube;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                Debug.Log("Antoine a posé ses couilles sur vos nez !");
            }
        }
        else
        {
            FindInTargets(other.gameObject);
        }*/

        if (other.gameObject.tag == "Bullet" && other.transform.GetComponent<bulletScript>().launcherName != transform.parent.GetComponent<TeamNumber>().teamName)
        {
            transform.position = SpawnPos;
            int rand = Random.Range(0, nodes.transform.childCount - 1);

            target = nodes.transform.GetChild(rand).gameObject;

            //Calc();
            //GetComponent<NavMeshAgent>().Warp(SpawnPos);
            //GetComponent<NavMeshAgent>().SetDestination(points[index].transform.position);
        }
    }

    /*void FindInTargets(GameObject obj)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (obj == enemies[i])
            {
                enemies[i] = null;
            }
        }
    }*/

    /*void FindNewTarget()
    {
        target = null;
        float dist = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                float tempDist = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (enemies[i] == gameObject)
                {
                    enemies[i] = null;
                }
                else if (tempDist < dist)
                {
                    Vector3 fwd = enemies[i].transform.position - transform.position;
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, fwd, out hit) && hit.transform.tag == "Target" && hit.transform.gameObject != bro1 && hit.transform.gameObject != bro2)
                    {
                        target = enemies[i];
                        //transform.LookAt(target.transform.position);
                        spawnBulletRotation.transform.LookAt(target.transform.position);
                        dist = tempDist;
                    }
                }
            }
        }
    }*/

    public void Dodge(Vector3 pos, Vector3 v, Vector3 forward)
    {
        if (canShoot && (!target || Vector3.Distance(transform.position, target.transform.position) > Vector3.Distance(transform.position, pos)))
        {
            spawnBulletRotation.transform.LookAt(pos);
            GameObject go = Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity) as GameObject;
            go.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
            go.transform.LookAt(pos + forward);
            canShoot = false;
        }
        else if(esquive != true)
        {
            esquive = true;
            StartCoroutine(Esquive(v));
        }
    }

    IEnumerator Esquive(Vector3 v)
    {
        GetComponent<NavMeshAgent>().SetDestination(transform.position + v * 10);
        yield return new WaitForSeconds(2f);
        GetComponent<NavMeshAgent>().SetDestination(points[index].transform.position);
        esquive = false;
    }
}