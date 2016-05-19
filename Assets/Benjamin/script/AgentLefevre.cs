using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using benjamin;

public class AgentLefevre : MonoBehaviour
{
    public static AgentLefevre instance = null;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
    }



    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    public Vector3 currentTarget;
    public Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();
    public List<GameObject> targets = new List<GameObject>();
    Vector3 spawn;
    public GameObject bullet;
    public float fireRate;
    public float bulletSpeed = 40f;

    public StateMachine sm;

    // Use this for initialization
    void Start()
    {
        sm = new StateMachine();
        sm.SetCurrentState(new MoveToState());

        spawn = transform.position;

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

    }

    // Update is called once per frame
    void Update()
    {
        sm.Check();
        sm.StateUpdate();
        
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
            transform.position = spawn;
        }
    }
    
    public void Fire()
    {
        Debug.Log("FIRE");
        if (target == null)
            return;
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        GameObject instanceBul = Instantiate(bullet, transform.position+relativePos.normalized*2f, rotation) as GameObject;
        instanceBul.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
        //StartCoroutine(fireRoutine());
    }

    public IEnumerator fireRoutine()
    {
        target = GetTarget();
        if (target == null)
        {
            yield return null;
        }
        else
        {   //Avec anticipation
            Vector3 startPos = target.transform.position;
            yield return new WaitForSeconds(0.1f);
            Vector3 direction = target.transform.position-startPos;
            float dist = Vector3.Distance(transform.position, target.transform.position);
            float timeToHit = dist/ bulletSpeed;
            Vector3 posToShoot = startPos+(direction*10f)*timeToHit;
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
                    yield return null;
                }
            }
            if (timeToHit > 0.5f)
            {
                Vector3 relativePos = posToShoot - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                Debug.DrawLine(transform.position, posToShoot, Color.blue, 2f);
                instance = Instantiate(bullet, transform.position+ relativePos.normalized*2.0f, rotation) as GameObject;

            }
            else
            {
                Vector3 relativePos = target.transform.position+direction - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                Debug.DrawLine(transform.position, posToShoot, Color.blue, 2f);
                instance = Instantiate(bullet, transform.position + relativePos.normalized * 2.0f, rotation) as GameObject;
            }
            instance.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;

            // sans anticipation
            /*Vector3 relativePos = target.transform.position + direction - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Debug.DrawLine(transform.position, posToShoot, Color.blue, 2f);
            instance = Instantiate(bullet, transform.position + relativePos.normalized * 2.0f, rotation) as GameObject;
            instance.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;*/

        }
        yield return null;
    }
    
    GameObject GetTarget()
    {
        float dist = Mathf.Infinity;
        GameObject target = null;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Target"))
        {
            RaycastHit hit;
            Vector3 direction = (obj.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position+ direction, direction, out hit))
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
}
