using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentAntoine : MonoBehaviour {

    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;

    public GameObject[] enemies;

    bool finished = false;

    public Material cube;

	// Use this for initialization
	void Start ()
    { 
        InvokeRepeating("ChangeColor", 0.5f, 0.1f);

        enemies = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == gameObject)
            {
                enemies[i] = null;
            }
        }

        FindNewTarget();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!finished)
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        else
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 50f);
        }
	}

    void ChangeColor()
    {
        if(!finished)
            GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1), Random.Range(0f, 1), Random.Range(0f, 1));
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Target" && other.gameObject == target)
        {
            FindInTargets(other.gameObject);
            FindNewTarget();
            if (target == null)
            {
                finished = true;
                GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Cube").transform.position);
                GetComponent<MeshRenderer>().material = cube;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else
        {
            FindInTargets(other.gameObject);
        }
    }

    void FindInTargets(GameObject obj)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if(obj == enemies[i])
            {
                enemies[i] = null;
            }
        }
    }

    void FindNewTarget()
    {
        target = null;
        float dist = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                float tempDist = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (enemies[i] == gameObject)
                {
                    enemies[i] = null;
                }
                else if (tempDist < dist)
                {
                    target = enemies[i];
                    dist = tempDist;
                }
            }
        }
    }
}
