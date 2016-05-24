using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentValentinTree : MonoBehaviour {

    public bool ennemyVisibleLol = false;
    public bool shootDefLol = false;
    public bool shootAttLol = false;

    public List<GameObject> listJoueurs = new List<GameObject>();
    public GameObject target;
    NavMeshAgent agent;
    public Vector3 initialPos;
    Selector S1;
    float cdShootMax = 1f;
    public float cdShoot = 1f;
    public bool rightOrNot = true;
    float needToChange = 0f;

    // Use this for initialization
    void Start()
    {
        initialPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        seekAllOtherPlayers();
        InvokeRepeating("chooseATarget", 0, 1f);

        #region startTree
        Task taskSeePlayer = new Task(seePlayer);
        Task taskCAnShoot = new Task(canIShoot);
        Task taskShoot = new Task(shoot);
        Task taskEsquive = new Task(esquive);
        Task taskIsNear = new Task(isNear);
        Task taskIdle = new Task(stay);
        Task taskWalk = new Task(walk);

        S1 = new Selector();
        Selector S2 = new Selector();
        Selector S3 = new Selector();

        Sequence SE1 = new Sequence();
        Sequence SE3 = new Sequence();
        Sequence SE4 = new Sequence();

        List<NodeTree> list1 = new List<NodeTree>();
        list1.Add(taskIsNear);
        list1.Add(taskIdle);
        SE4.addListNode(list1);
        List<NodeTree> list2 = new List<NodeTree>();
        list2.Add(SE4);
        list2.Add(taskWalk);
        S3.addListNode(list2);
        List<NodeTree> list3 = new List<NodeTree>();
        list3.Add(taskCAnShoot);
        list3.Add(taskShoot);
        SE3.addListNode(list3);
        List<NodeTree> list4 = new List<NodeTree>();
        list4.Add(SE3);
        list4.Add(taskEsquive);
        S2.addListNode(list4);
        List<NodeTree> list5 = new List<NodeTree>();
        list5.Add(taskSeePlayer);
        list5.Add(S2);
        SE1.addListNode(list5);
        List<NodeTree> list6 = new List<NodeTree>();
        list6.Add(SE1);
        list6.Add(S3);
        S1.addListNode(list6);






        #endregion

        InvokeRepeating("checkTree", 0, 0.1f);
        changeDirection();
    }

    void checkTree()
    {

        S1.execute();
    }

    void seekAllOtherPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject go in players)
        {
            AgentValentinTree team = go.GetComponent<AgentValentinTree>();
            if (team == null)
            {
                listJoueurs.Add(go);
            }
        }
    }
    void chooseATarget()
    {
        GameObject go = null;
        for (int i = 0; i < listJoueurs.Count; i++)
        {
            if (go == null)
            {
                go = listJoueurs[0];
            }
            else if (Vector3.Distance(transform.position, go.transform.position) > Vector3.Distance(transform.position, listJoueurs[i].transform.position))
            {
                go = listJoueurs[i];
            }
        }

        target = go;
    }



    void Update()
    {
        if (cdShoot != 0)
        {
            cdShoot = Mathf.Max(0f, cdShoot - Time.deltaTime);
        }
        if (cdShoot != 0)
        {
            needToChange = Mathf.Max(0f, needToChange - Time.deltaTime);
        }
        else
        {
            changeDirection();
        }
    }

    #region pour le tree



    public bool seePlayer()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize(); ;
        Ray ray = new Ray(transform.position + direction * 2 / 3, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.tag == "Target")
            {
                if(listJoueurs.Contains(hit.transform.gameObject))
                {
                    return true;

                }

            }
        }
        
        return false;
    }
    public bool canIShoot()
    {
        return cdShoot == 0;
    }
    public bool shoot()
    {
        NavMeshAgent ag = target.GetComponent<NavMeshAgent>();
        if (ag != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.Normalize();
            GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + direction * 2 / 3, Quaternion.identity) as GameObject;
            float t = Vector3.Distance(transform.position, target.transform.position) / (bullet.GetComponent<bulletScript>().speed);
            Vector3 temp = target.transform.position + (target.GetComponent<NavMeshAgent>().velocity * (t));
            direction = temp - transform.position;

            Ray ray = new Ray(transform.position + direction * 2 / 3, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.tag == "Target")
                {
                    if (listJoueurs.Contains(hit.transform.gameObject))
                    {


                    }
                    else
                    {
                        direction = target.transform.position - transform.position;
                        direction.Normalize();
                    }

                }
            }
            cdShoot = cdShootMax;

            bullet.GetComponent<bulletScript>().launcherName = "PapaValentin";
            bullet.transform.LookAt(temp);
        }
        else
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.Normalize();
            GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + direction*2/3, Quaternion.identity) as GameObject;
            bullet.GetComponent<bulletScript>().launcherName = "PapaValentin";
            bullet.transform.LookAt(target.transform.position);
            cdShoot = cdShootMax;
        }
        return true;
    }

    void changeDirection()
    {
        needToChange = Random.Range(0.5f, 2.5f);
        rightOrNot = !rightOrNot;
    }

    public bool esquive()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        transform.LookAt(target.transform);
        if (rightOrNot)
        {
            agent.SetDestination(transform.position - transform.right * 2 - direction * 2);
        }
        else
        {
            agent.SetDestination(transform.position + transform.right * 2 - direction * 2);
        }
        return true;
    }
    public bool isNear()
    {
        if (Vector3.Distance(target.transform.position,transform.position) < 12)
        {
            return true;
        }
        return false;
    }
    public bool stay()
    {
        agent.SetDestination(transform.position);
        return true;
    }
    public bool walk()
    {
        agent.SetDestination(target.transform.position);
        return true;
    }


    #endregion


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            agent.Warp(initialPos);
            agent.SetDestination(target.transform.position);
        }
    }
}
