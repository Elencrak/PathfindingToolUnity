using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamAgentValentin : MonoBehaviour {

    StateMachineValentin smValentin;

    public bool ennemyVisibleLol = false;
    public bool shootDefLol = false;
    public bool shootAttLol = false;

    public List<GameObject> listJoueurs = new List<GameObject>();
    public GameObject target;
    NavMeshAgent agent;
    Vector3 initialPos;

    float cdShootMax = 1f;
    float cdShoot = 1f;
    // Use this for initialization
    void Start ()
    {
        #region startState
        ShootValentin shoot = new ShootValentin(this);
        ChaseValentin chase = new ChaseValentin(this);
        ShootDefensif shootdef = new ShootDefensif(this);
        MoveValentin move = new MoveValentin(this);

        List<TransitionValentin> shootTrans = new List<TransitionValentin>();
        List<TransitionValentin> chaseTrans = new List<TransitionValentin>();
        List<TransitionValentin> shootDefTrans = new List<TransitionValentin>();
        List<TransitionValentin> moveTrans = new List<TransitionValentin>();


        TransitionValentin seeMyEnnemy = new TransitionValentin(seeEnnemy, chase);
        moveTrans.Add(seeMyEnnemy);
        TransitionValentin dontSeeMyennemy = new TransitionValentin(dontSeeEnnemy, move);
        chaseTrans.Add(dontSeeMyennemy);
        TransitionValentin offensiveShoot = new TransitionValentin(canIOffensiveShoot, shoot);
        chaseTrans.Add(offensiveShoot);
        TransitionValentin defensiveShoot = new TransitionValentin(canIDefensiveShoot, shootdef);
        chaseTrans.Add(defensiveShoot);
        TransitionValentin shooted = new TransitionValentin(shootIsGone, move);
        shootDefTrans.Add(shooted);
        shootTrans.Add(shooted);


        shoot.addTransition(shootTrans);
        chase.addTransition(chaseTrans);
        shootdef.addTransition(shootDefTrans);
        move.addTransition(moveTrans);

        smValentin = new StateMachineValentin(move);
        List<TransitionValentin> stateMachineTransition = new List<TransitionValentin>();
        smValentin.addTransition(stateMachineTransition);
        #endregion

        initialPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        seekAllOtherPlayers();
        chooseATarget();
    }

    void seekAllOtherPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject go in players)
        {
            TeamAgentValentin team = go.GetComponent<TeamAgentValentin>();
            if(team == null)
            {
                listJoueurs.Add(go);
            }
        }
    }
    void chooseATarget()
    {
        target = listJoueurs[Random.Range(0, listJoueurs.Count)];
    }



    void Update()
    {
        if(cdShoot !=0)
        {
            cdShoot = Mathf.Max(0f, cdShoot - Time.deltaTime);
        }

        //UpdateState
        smValentin.Step();
    }

    #region pour le state

    public void seekPlayer()
    {
        agent.SetDestination(target.transform.position);
    }

    public void shootPlayer()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + direction , Quaternion.identity) as GameObject;
        bullet.GetComponent<bulletScript>().launcherName = "TeamValentinPharhaLaunchRocket";
        bullet.transform.LookAt(target.transform.position);
        cdShoot = cdShootMax;
    }

    public void chasePlayer()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        transform.LookAt(target.transform);
        if (Random.Range(0, 2) < 1)
        {
            agent.SetDestination(transform.position - transform.right * 2);
        }
        else
        {
            agent.SetDestination(transform.position + transform.right * 2);
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
        if(/*shootAttLol &&*/ cdShoot ==0)
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
        Ray ray = new Ray(transform.position+direction * 2 / 3, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100f))
        {
            if(hit.transform.tag == "Target")
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
        Ray ray = new Ray(transform.position + direction*2/3, direction);
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
