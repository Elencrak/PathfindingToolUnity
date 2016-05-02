using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentM : MonoBehaviour {

    public GameObject target;
	public GameObject[] tabTarget;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		tabTarget = GameObject.FindGameObjectsWithTag ("Target");
		RemoveTargetFromTab (this.gameObject);
		target = FindCloseTarget();
		if (target != null) 
		{
			agent.destination = target.transform.position; 
		}
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null) 
		{
			Debug.Log ("finish");
		}
	}

	GameObject FindCloseTarget()
	{
		GameObject T = null;
		float dist = 999999;
		foreach (GameObject go in tabTarget) 
		{
			if (go != null) 
			{
				float actualDist = Vector3.Distance (this.gameObject.transform.position, go.transform.position);
				if (actualDist < dist) 
				{
					dist = actualDist;
					T = go;
				}
			}
		}
		return T;
	}

	void RemoveTargetFromTab(GameObject toRemove)
	{
		int index = -1;
		foreach (GameObject go in tabTarget) 
		{
			index++;
			if (go == toRemove) 
			{
				Debug.Log (toRemove.name);
				Debug.Log (tabTarget [index].name);
				Debug.Log (index);
				tabTarget [index] = null;
			}
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		Debug.Log ("hit");
		if (collision.gameObject.tag == "Target") 
		{
			RemoveTargetFromTab (collision.gameObject);
			target = FindCloseTarget();
			if (target != null) 
			{
				agent.destination = target.transform.position; 
			}
		}
	}
}
