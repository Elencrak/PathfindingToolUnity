using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentValentin : MonoBehaviour {

    NavMeshAgent agent;
    List<GameObject> players;
    GameObject target;
    public AnimationCurve curve;
    Material mat;
    Vector3 position;
    Vector3[] allPos = new Vector3[5];
    public Vector3 targetMovement;
    int mov = 1;
    public int score = 0;
    float cd = 0f;

	// Use this for initialization
	void Start () {
        position = transform.position;
        agent = GetComponent<NavMeshAgent>();
        GameObject[] pl = GameObject.FindGameObjectsWithTag("Target");
        players = new List<GameObject>(pl);

        allPos[0] = new Vector3(-40, 0, -40);
        allPos[1] = new Vector3(-10.8f, 14.8f, 2.8f);
        allPos[2] = new Vector3(40, 0, 40);
        allPos[3] = new Vector3(40, 0, -40);
        allPos[4] = new Vector3(-40, 0, 40);

        mat = gameObject.GetComponent<Renderer>().material;
        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;

        targetMovement = allPos[1];
        agent.SetDestination(targetMovement);


	}
	
	// Update is called once per frame
	void Update () {
       // transform.localScale = new Vector3((1-curve.Evaluate(Time.time)/2), (1+curve.Evaluate(Time.time)/2), (1-curve.Evaluate(Time.time)/2));
        mat.color = new Color(1-curve.Evaluate(Time.time+0.6f), curve.Evaluate(Time.time+0.4f), 1-curve.Evaluate(Time.time+0.2f));
        

        if (Vector3.Distance(transform.position, targetMovement)< 3)
        {
            mov++;
            if (mov == 5)
                mov = 0;

            targetMovement = allPos[mov];
            
            agent.SetDestination(targetMovement);
        }

        if(cd != 0)
        {
            cd = Mathf.Max(0f, cd - Time.deltaTime);
        }
        else
        {
            UpdateValentin();
        }
    }

    void UpdateValentin()
    {
        findTarget();
    }

    void findTarget()
    {
        foreach (GameObject player in players)
        {
            if (player != gameObject)
            {
                RaycastHit hit;
                Vector3 direction = player.transform.position - transform.position;
                direction.Normalize();
                Ray ray = new Ray(transform.position,direction);
                if (Physics.Raycast(transform.position, direction, out hit, 100.0f))
                {
                    if(hit.transform.tag == "Target")
                    {
                        GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + direction * 2, Quaternion.identity)as GameObject;
                        bullet.GetComponent<bulletScript>().launcherName = "TeamValentinPharhaLaunchRocket";
                        float t = Vector3.Distance(transform.position, player.transform.position) / (bullet.GetComponent<bulletScript>().speed);
                        Vector3 temp = player.transform.position + (player.GetComponent<NavMeshAgent>().velocity * (t));
                        bullet.transform.LookAt(temp);
                        cd = 1f;
                        break;
                    }
                }
            }
        }
    }

    void Shoot()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Bullet")
        {
            agent.Warp(position);
            agent.SetDestination(targetMovement);
            findTarget();
        }
    }

}
