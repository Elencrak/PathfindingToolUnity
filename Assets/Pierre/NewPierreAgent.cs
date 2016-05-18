using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPierreAgent : MonoBehaviour {
    
    TeamNumber team;

    public List<GameObject> targets;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Vector3 currentTargetMove;

    public GameObject bullet;

    NavMeshAgent nav;

    Vector3 startPos;

    Vector3 lastTargetPosition;

    PierreStateMachine stateMachine;

    // Use this for initialization
    void Start() {

        stateMachine = GetComponent<PierreStateMachine>();

        team = transform.parent.GetComponent<TeamNumber>();

        startPos = transform.position;
        
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 10;
        nav.acceleration = 20;
        nav.stoppingDistance = 5;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Target"))
        {
            targets.Add(go);
        }
        targets.Remove(gameObject);
        
        InvokeRepeating("UpdateRoad", 0.1f, 0.1f);
        //Debug.Log(PathfindingManager.GetInstance().test);
        InvokeRepeating("Fire", 0f, 1f);
    } 

    void Fire()
    {
        transform.LookAt(currentTarget + (currentTarget - lastTargetPosition)*Vector3.Distance(currentTarget, transform.position)/4);

        GameObject b = Instantiate(bullet, transform.position + transform.forward * 1.5f, Quaternion.identity) as GameObject;

        b.transform.LookAt(currentTarget + (currentTarget - lastTargetPosition) * Vector3.Distance(currentTarget, transform.position)/4);
        b.GetComponent<bulletScript>().launcherName = team.teamName;
    }

	// Update is called once per frame
	void Update () {

        stateMachine.Move(nav);

    }

    void UpdateRoad()
    {
        if (targets.Count <= 0) return;
        
        lastTargetPosition = currentTarget;

        currentTarget = stateMachine.UpdateTarget(currentTarget, targets);

        currentTargetMove = stateMachine.UpdateTargetMove(currentTargetMove, targets);

        nav.SetDestination(currentTargetMove);
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (targets.Contains(col.gameObject))
        {
            if(targets.Count <= 0)
            {
                Debug.Log("Pierre a gagné !");
            }
        }
        else if(col.transform.tag == "Bullet" && col.transform.GetComponent<bulletScript>().launcherName != team.teamName)
        {
            nav.Warp(startPos);
        }
    }
}
