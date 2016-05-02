using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentValentin : MonoBehaviour {

    NavMeshAgent agent;
    List<GameObject> players;
    GameObject target;
    public AnimationCurve curve;
    Material mat;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        GameObject[] pl = GameObject.FindGameObjectsWithTag("Target");
        players = new List<GameObject>(pl);

        mat = gameObject.GetComponent<Renderer>().material;
        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;

        InvokeRepeating("UpdateValentin", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3((1-curve.Evaluate(Time.time)/2), (1+curve.Evaluate(Time.time)/2), (1-curve.Evaluate(Time.time)/2));
        mat.color = new Color(1-curve.Evaluate(Time.time+0.6f), curve.Evaluate(Time.time+0.4f), 1-curve.Evaluate(Time.time+0.2f));
    }

    void UpdateValentin()
    {
        findTarget();
        if(target != null)
            agent.SetDestination(target.transform.position);
    }

    void findTarget()
    {
        target = null;
        if (players.Count > 0)
        {
            foreach (GameObject player in players)
            {
                if (player != gameObject)
                {
                    if (target == null)
                    {
                        target = player;
                    }
                    else if (Vector3.Distance(transform.position, target.transform.position) > Vector3.Distance(transform.position, player.transform.position))
                    {
                        target = player;
                    }
                }
            }
        }
        else
        {
            Debug.Log("j'ai finis hihihihihihihihihi , Valentin m'a bien prog !");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Target")
        {
            players.Remove(collision.gameObject);
            findTarget();
        }
    }

}
