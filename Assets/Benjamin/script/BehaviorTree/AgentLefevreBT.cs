using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BenjaminBehaviorTree;


public class AgentLefevreBT : MonoBehaviour {

    public float lastFire;
    public float fireRate = 1f;
    public GameObject bullet;
    public GameObject target;
    public List<GameObject> targets = new List<GameObject>();
    public Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    public Vector3 currentTarget;
    public NavMeshAgent agent;

    Vector3 spawn;
    float bulletSpeed = 40f;
    float RespawnTime = 1f;
    float lastRespawn = 1f;
    bool fireSuccess = false;

    BenjaminBehaviorTree.Composite behaviorTree = new BenjaminBehaviorTree.Selector();
    BenjaminBehaviorTree.Composite shootSequence = new BenjaminBehaviorTree.Sequence();
    BenjaminBehaviorTree.Composite moveToTarget = new BenjaminBehaviorTree.Sequence();
    BenjaminBehaviorTree.Composite moveToRandom = new BenjaminBehaviorTree.Sequence();

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();

        graph = new Pathfinding();
        graph.Load("benPath");
        graph.setNeighbors();

        bulletSpeed = bullet.GetComponent<bulletScript>().speed;

        shootSequence.AddTask(new BenjaminBehaviorTree.ChooseTargetTask(this));
        shootSequence.AddTask(new BenjaminBehaviorTree.CanShootCondition(this));
        shootSequence.AddTask(new BenjaminBehaviorTree.ShootTask(this));
        moveToTarget.AddTask(new BenjaminBehaviorTree.IsTargetValidCondition(this));
        moveToTarget.AddTask(new BenjaminBehaviorTree.MoveToTask(this));
        //moveToRandom.AddTask(new GetRandomNavigationPointTask(this));
        //moveToRandom.AddTask(new MoveToTask(this));
        behaviorTree.AddTask(shootSequence);
        behaviorTree.AddTask(moveToTarget);
        //behaviorTree.AddTask(moveToRandom);

        spawn = transform.position;
        Debug.Log("start");
        Debug.Log(1 << 20);
        InvokeRepeating("UpdateRoad", 0f, 0.5f);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach (Vector3 pos in road)
            Gizmos.DrawSphere(pos, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawn, 0.5f);

    }

    void Update()
    {
        if(Time.time > lastRespawn+RespawnTime)
            behaviorTree.Execute();
        else
        {
            UpdateRoad();
        }
    }

    public void RefreshTargets()
    {
        targets.Clear();
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject obj in tmp)
        {
            if (obj.transform.parent.GetComponent<TeamNumber>().teamName.Equals(transform.parent.GetComponent<TeamNumber>().teamName))
                continue;
            else
                targets.Add(obj);
        }
    }
    public bool Fire()
    {
        Debug.Log("FIRE");
        agent.Stop();
        lastFire = Time.time;
        fireSuccess = false;
        StartCoroutine(fireRoutine());
        return fireSuccess;
    }

    void UpdateRoad()
    {
        if (target == null)
        {
            RefreshTargets();
            target = targets[Random.Range(0, targets.Count)];
        }
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
        road = PathfindingManager.GetInstance().SmoothRoad(road);
    }

    IEnumerator fireRoutine()
    {
        if (target == null)
        {
            fireSuccess = false;
            yield return null;
        }
        else
        {   //Avec anticipation
            Vector3 startPos = target.transform.position;
            yield return new WaitForSeconds(0.1f);
            Vector3 direction = target.transform.position - startPos;
            float dist = Vector3.Distance(transform.position, target.transform.position);
            float timeToHit = dist / bulletSpeed;
            Vector3 posToShoot = startPos + (direction * 10f) * timeToHit;
            dist = Vector3.Distance(transform.position, posToShoot);
            timeToHit = dist / bulletSpeed;
            posToShoot = startPos + (direction * 10f) * timeToHit;
            float targetY = target.transform.position.y;
            float Y = transform.position.y;
            //tirer un peu plus haut quand il y a une difference de hauteur
            if (targetY - Y > 1f)
                posToShoot += Vector3.up * 0.4f;
            GameObject instance;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, target.transform.position, out hit))
            {
                if (hit.transform != null && hit.transform.gameObject != target)
                {
                    fireSuccess = false;
                    yield return null;
                }
            }
            if (timeToHit > 0.5f)
            {
                Vector3 relativePos = posToShoot - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                Debug.DrawLine(transform.position, posToShoot, Color.blue, 2f);
                instance = Instantiate(bullet, transform.position + relativePos.normalized * 2.0f, rotation) as GameObject;

            }
            else
            {
                Vector3 relativePos = target.transform.position + direction - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                Debug.DrawLine(transform.position, posToShoot, Color.blue, 2f);
                instance = Instantiate(bullet, transform.position + relativePos.normalized * 2.0f, rotation) as GameObject;
            }
            instance.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
        }
        fireSuccess = true;
        yield return null;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Bullet")
        {
            Respawn();
        }
    }
    void Respawn()
    {
        Debug.Log("respawn");
        agent.Warp(spawn);
        lastRespawn = Time.time;
        UpdateRoad();
    }
}
