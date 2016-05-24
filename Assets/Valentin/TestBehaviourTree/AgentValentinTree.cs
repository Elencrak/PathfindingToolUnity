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
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + direction, Quaternion.identity) as GameObject;
        bullet.GetComponent<bulletScript>().launcherName = "PapaValentin";
        bullet.transform.LookAt(target.transform.position);
        cdShoot = cdShootMax;
        rightOrNot = !rightOrNot;
        return true;
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


    /* public void seekPlayer()
     {
         agent.SetDestination(target.transform.position);
     }

     public void shootPlayer()
     {
         Vector3 direction = target.transform.position - transform.position;
         direction.Normalize();
         GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + direction, Quaternion.identity) as GameObject;
         bullet.GetComponent<bulletScript>().launcherName = "TeamValentinPharhaLaunchRocket";
         bullet.transform.LookAt(target.transform.position);
         cdShoot = cdShootMax;
         rightOrNot = !rightOrNot;


     }

     public void chasePlayer()
     {
         Debug.Log("chase");
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
     }

     public void idlePlayer()
     {
         Debug.Log("idle");
     }

     public void attaquePlayer()
     {
         Debug.Log("attaqueState");
     }

     public void shootDefensifPlayer()
     {
         Debug.Log("attaqueDefensive");
         cdShoot = cdShootMax;
     }

     public bool shootIsGone()
     {
         return cdShoot != 0;
     }

     public bool canIOffensiveShoot()
     {
         if ( cdShoot == 0)
         {
             return true;
         }
         else
             return false;
     }

     public bool canIDefensiveShoot()
     {
         if (shootDefLol && cdShoot == 0)
         {
             return true;
         }
         else
             return false;
     }


     public bool seeEnnemy()
     {
         Vector3 direction = target.transform.position - transform.position;
         direction.Normalize(); ;
         Ray ray = new Ray(transform.position + direction * 2 / 3, direction);
         RaycastHit hit;
         if (Physics.Raycast(ray, out hit, 100f))
         {
             if (hit.transform.tag == "Target")
             {
                 return true;
             }
         }
         return false;
     }

     public bool dontSeeEnnemy()
     {
         Vector3 direction = target.transform.position - transform.position;
         direction.Normalize(); ;
         Ray ray = new Ray(transform.position + direction * 2 / 3, direction);
         RaycastHit hit;
         if (Physics.Raycast(ray, out hit, 100f))
         {
             if (hit.transform.tag == "Target")
             {
                 return false;
             }
         }
         return true;
     }

     bool canIShoot()
     {
         return cdShoot == 0;
     }
     public bool isBulletNearly()
     {
         return true;
     }*/
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
