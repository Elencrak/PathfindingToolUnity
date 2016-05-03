using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentLefevre : MonoBehaviour
{

    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();
    public List<GameObject> targets = new List<GameObject>();
    Vector3[] coverPoints;
    Vector3 spawn;
    int currentCover;
    NavMeshAgent agent;
    public GameObject bullet;
    float fireRate;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        float dist = Mathf.Infinity;
        GameObject[] test = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject obj in test)
        {
            if (obj == gameObject)
                continue;
            float tmp = Vector3.Distance(transform.position, obj.transform.position);
            if (tmp < dist)
            {
                dist = tmp;
                target = obj;
            }
            targets.Add(obj);
            
        }
        spawn = transform.position;
        coverPoints = new Vector3[4];
        coverPoints[0] = transform.GetChild(0).position;
        coverPoints[1] = transform.GetChild(1).position;
        coverPoints[2] = transform.GetChild(2).position;
        coverPoints[3] = transform.GetChild(3).position;
        currentCover = 0;

        /*
        //Select your pathfinding
        graph = new Pathfinding();
        graph.Load(PlayerPrefs.GetString("Pierre"));
        graph.setNeighbors();
        //


        target = GameObject.FindGameObjectWithTag("Target");
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
        Debug.Log(PathfindingManager.GetInstance().test);

        */
        InvokeRepeating("Fire", 0f, 1f);

    }
    void Init()
    {
        transform.position = spawn;
        currentCover = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (targets.Count > 0)
            agent.SetDestination(target.transform.position);
        else
        {
            Debug.Log("Benjamin à fini !");
            Destroy(this);
        }*/
        /*
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
        }*/
    }

    void UpdateRoad()
    {
        float dist = Mathf.Infinity;
        foreach (GameObject obj in targets)
        {
            float tmp = Vector3.Distance(transform.position, obj.transform.position);
            if (tmp < dist)
            {
                dist = tmp;
                target = obj;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Target" && targets.Contains(col.gameObject))
        {
            targets.Remove(col.gameObject);
            UpdateRoad();
        }
        if (col.transform.tag == "Bullet")
        {
            Init();
        }
    }

    public void Fire()
    {
        StartCoroutine(fireRoutine(currentCover));
    }

    public IEnumerator fireRoutine(int coverPointIndex)
    {
        agent.Stop();
        target = GetTarget(coverPointIndex);
        if (target == null)
        {

            ChangeCover();
            yield return null;
        }
        else
        {
            agent.Resume();
            agent.SetDestination(coverPoints[coverPointIndex + 1]);
            yield return new WaitForSeconds(0.5f);
            agent.Stop();
            Vector3 relativePos = target.transform.position - coverPoints[coverPointIndex + 1];
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            GameObject instance = Instantiate(bullet, transform.position+ relativePos.normalized*2.0f, rotation) as GameObject;
            instance.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
        }
        agent.Resume();
        agent.SetDestination(coverPoints[coverPointIndex]);
        yield return null;
    }

    GameObject GetTarget(int coverPointIndex)
    {
        float dist = Mathf.Infinity;
        GameObject target = null;
        foreach(GameObject obj in targets)
        {
            RaycastHit hit;
            if (Physics.Raycast(coverPoints[coverPointIndex + 1], obj.transform.position - coverPoints[coverPointIndex + 1], out hit))
            {
                if (hit.transform.gameObject == obj)
                {
                    float tmp = Vector3.Distance(transform.position, obj.transform.position);
                    if(tmp < dist)
                    {
                        dist = tmp;
                        target = obj;
                    }
                }
            }
        }
        return target;
        

    }
    void ChangeCover()
    {
        agent.Resume();
        currentCover+=2;
        currentCover %= coverPoints.Length;
        agent.SetDestination(coverPoints[currentCover]);
    }
}
