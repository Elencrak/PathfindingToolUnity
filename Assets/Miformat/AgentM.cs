using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentM : MonoBehaviour {

	public GameObject cancer;
    GameObject target;
	GameObject[] tabTarget;
	float timeToColor = 1;
	float distToTarget;
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
		if (target != null) 
		{
			agent.destination = target.transform.position; 
			Cheat ();
		}
		timeToColor -= Time.deltaTime;
		if (timeToColor < 0) 
		{
			timeToColor = 1;
			Coloring (this.gameObject);
		}
		if (target == null) 
		{
			Debug.Log ("MiformatFinish");
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

	void Cheat()
	{
		distToTarget = Vector3.Distance (this.gameObject.transform.position, target.transform.position);
		if (distToTarget < 2) 
		{
			this.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		} 
		else 
		{
			this.gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		}
	}

	void Cancer(GameObject hit)
	{
		Vector3 pos = hit.transform.position;
		Destroy (hit);
		Instantiate (cancer, pos, Quaternion.identity);
	}

	void RemoveTargetFromTab(GameObject toRemove)
	{
		int index = -1;
		foreach (GameObject go in tabTarget) 
		{
			index++;
			if (go == toRemove) 
			{
				tabTarget [index] = null;
			}
		}
	}

	void Coloring(GameObject toColor)
	{
		float r = Random.Range (0.0f,1.0f);
		float g = Random.Range (0.0f,1.0f);
		float b = Random.Range (0.0f,1.0f);
		Color col = new Color (r,g,b);
		if (toColor.GetComponent<MeshRenderer> ()) {toColor.GetComponent<MeshRenderer> ().material.color = col;}
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.gameObject.tag == "Target") 
		{
			//Cancer (collision.gameObject);
			//this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			Coloring (collision.gameObject);
			RemoveTargetFromTab (collision.gameObject);
			target = FindCloseTarget();
			if (target != null) 
			{
				agent.destination = target.transform.position; 
			}
		}
	}

	void OnCollisionExit(Collision collision) 
	{
		if (collision.gameObject.tag == "Target") 
		{
			//this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			Coloring (collision.gameObject);
			RemoveTargetFromTab (collision.gameObject);
			target = FindCloseTarget();
			if (target != null) 
			{
				agent.destination = target.transform.position; 
			}
		}
	}
}
