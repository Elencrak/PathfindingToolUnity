using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentM : MonoBehaviour {

	//public GameObject cancer;
	GameObject bullet;
    GameObject target;
	GameObject[] tabTarget;
	GameObject currentBullet = null;
	float distToTarget;
	NavMeshAgent agent;
	Vector3 startPos = new Vector3(58,1,-38);
	float fireRate = 1;
	public int ID;
	int state = 1;
	bool isArrived = false;
	bool isAvoiding = false;
	GameObject toAvoid = null;

	// Use this for initialization
	void Start () 
	{
		bullet = Resources.Load ("Bullet") as GameObject;
		//startPos = this.gameObject.transform.position;
		agent = GetComponent<NavMeshAgent>();
		tabTarget = GameObject.FindGameObjectsWithTag ("Target");
		RemoveTargetFromTab (this.gameObject);
		target = FindCloseTarget();
		while (target.GetComponent<AgentM> ()) 
		{
			RemoveTargetFromTab (target);
			target = FindCloseTarget();
		}
		switch (ID) 
		{
			case 0: 
				agent.destination = new Vector3 (74,1,-5);
				//agent.destination = new Vector3 (-40,15,2.5f);
				break;
			case 1:
				agent.destination = new Vector3 (51,1,-4);
				//agent.destination = new Vector3 (35,15,2.5f);
				break;
			case 2:
				agent.destination = new Vector3 (58,1,-12);
				//agent.destination = new Vector3 (30,15,2.5f);
				break;
		}
		/*if (target != null) 
		{
			agent.destination = target.transform.position; 
		}*/
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (target != null)
        {
			LookAtTarget ();
            Cheat ();
            //agent.destination = target.transform.position;
        }
		if (currentBullet != null) {Dunk ();}
		fireRate -= Time.deltaTime;
		if (fireRate < 0) 
		{
			Coloring (this.gameObject);
			fireRate = 1;
			Shoot ();
		}
		if (target == null) 
		{
			Debug.Log ("Miformat Have Finish");
		}
		if (agent.remainingDistance < 0.5f) 
		{
			isArrived = true;
			isAvoiding = false;
		}
		if (!isAvoiding && isArrived) {Patrol ();}
		if (!isArrived){Avoid ();}
	}

	void Patrol()
	{
		switch (ID) 
		{
		case 0:
			agent.destination = new Vector3 (74,1,-5);
			//agent.destination = new Vector3 (-40,15,2.5f);
			break;
		case 1:
			agent.destination = new Vector3 (51,1,-4); 
			//agent.destination = new Vector3 (35,15,2.5f);
			break;
		case 2:
			if (state == 0) 
			{
				agent.destination = new Vector3 (50,1,-24);
				//agent.destination = new Vector3 (-30,15,2.5f);
				state = 1;
			} 
			else 
			{
				agent.destination = new Vector3 (58,1,-12);
				//agent.destination = new Vector3 (30,15,2.5f);
				state = 0;
			}
			isArrived = false;
			break;
		}
	}

	void Suicide()
	{
		GameObject[] tabBullet;
		tabBullet = GameObject.FindGameObjectsWithTag ("Bullet");
		foreach (GameObject go in tabBullet) 
		{
			float distBull = Vector3.Distance (this.gameObject.transform.position, go.transform.position);
			if (go.GetComponent<bulletScript> ().launcherName != "OSOK" && distBull < 2) {Death ();}
		}
	}

	void Avoid()
	{
		Collider[] closeBullet = Physics.OverlapSphere (this.gameObject.transform.position, 5);
		foreach (Collider go in closeBullet) 
		{
			if (go.gameObject != toAvoid && go.gameObject.tag == "Bullet" && go.gameObject.GetComponent<bulletScript> ().launcherName != "OSOK") 
			{
				Vector3 newDest = this.gameObject.transform.position;
				if (go.gameObject.transform.position.x > this.gameObject.transform.position.x) {newDest += go.gameObject.transform.right*2;}
				else if(go.gameObject.transform.position.x < this.gameObject.transform.position.x) {newDest -= go.gameObject.transform.right*2;}

				if (go.gameObject.transform.position.z > this.gameObject.transform.position.z) {newDest += go.gameObject.transform.forward;}
				else if(go.gameObject.transform.position.z < this.gameObject.transform.position.z) {newDest -= go.gameObject.transform.forward;}

				//newDest = this.gameObject.transform.position + go.gameObject.transform.right * 2 + go.gameObject.transform.forward;
				agent.destination = newDest;
				isAvoiding = true;
				isArrived = false;
				toAvoid = go.gameObject;
				//Debug.Log (this.gameObject.name + " avoiding " + go.gameObject.name + " to : " + newDest);
				break;
			}
		}
	}

	GameObject FindCloseTarget()
	{
		GameObject T = null;
		float dist = 999999;
		//if (ID != 1) 
		//{
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
		//} 
		/*else 
		{
			GameObject[] tabBull = GameObject.FindGameObjectsWithTag ("Bullet");
			foreach (GameObject go in tabBull) 
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
		}*/
		return T;
	}

	void Cheat()
	{
		distToTarget = Vector3.Distance (this.gameObject.transform.position, target.transform.position);
		if (distToTarget < 2) 
		{
			this.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
			Danger ();
		} 
		else 
		{
			this.gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		}
	}

	/*void Cancer(GameObject hit)
	{
		Vector3 pos = hit.transform.position;
		Destroy (hit);
		Instantiate (cancer, pos, Quaternion.identity);
	}*/

	void Danger()
	{
		Vector3 dir = target.GetComponent<NavMeshAgent> ().destination;
		agent.destination = this.transform.position + dir;
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

	void LookAtTarget()
	{
		this.gameObject.GetComponentInChildren<Transform> ().LookAt (target.transform.position);
	}

	void Coloring(GameObject toColor)
	{
		float r = Random.Range (0.0f,1.0f);
		float g = Random.Range (0.0f,1.0f);
		float b = Random.Range (0.0f,1.0f);
		Color col = new Color (r,g,b);
		if (toColor.GetComponent<MeshRenderer> ()) {toColor.GetComponent<MeshRenderer> ().material.color = col;}
	}

	void Dunk()
	{
		currentBullet.transform.localScale += currentBullet.transform.localScale * Time.deltaTime * 2;
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<bulletScript>().launcherName != "OSOK"){Death ();}
		if (collision.gameObject.tag == "Target") 
		{
			//Cancer (collision.gameObject);
			//this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			Coloring (collision.gameObject);
			//RemoveTargetFromTab (collision.gameObject);
			target = FindCloseTarget();
			while (target.GetComponent<AgentM> ()) 
			{
				RemoveTargetFromTab (target);
				target = FindCloseTarget();
			}
			//if (target != null) 
			//{
			//	agent.destination = target.transform.position; 
			//}
		}
	}

	void Death()
	{
		agent.Warp (startPos);
		this.gameObject.transform.position = startPos;
		target = FindCloseTarget();
		agent.destination = target.transform.position;
	}

	void Shoot()
	{
		Vector3 asmodunk = this.gameObject.transform.position;
		asmodunk.y += 1.8f;
		currentBullet = Instantiate (bullet, asmodunk, Quaternion.identity) as GameObject;
		target = FindCloseTarget();
		while (target.GetComponent<AgentM> ()) 
		{
			RemoveTargetFromTab (target);
			target = FindCloseTarget();
		}
		currentBullet.transform.LookAt (target.transform.position);
		currentBullet.GetComponent<bulletScript>().launcherName = "OSOK";
	}

	void OnCollisionExit(Collision collision) 
	{
		if (collision.gameObject.tag == "Target") 
		{
			//this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			Coloring (collision.gameObject);
			//RemoveTargetFromTab (collision.gameObject);
			target = FindCloseTarget();
			while (target.GetComponent<AgentM> ()) 
			{
				RemoveTargetFromTab (target);
				target = FindCloseTarget();
			}
			//if (target != null) 
			//{
			//	agent.destination = target.transform.position; 
			//}
		}
	}
}
