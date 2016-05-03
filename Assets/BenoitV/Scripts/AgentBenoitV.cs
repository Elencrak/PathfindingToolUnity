﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentBenoitV : MonoBehaviour {

    Vector3 spawnPosition;
    public List<GameObject> targets;
    NavMeshAgent myAgent;
    public GameObject myTarget;
    public GameObject myTargetShoot;
    float distanceMin;
    float currentDistance;
    public Transform[] pointOfInterest;
    int index;

    float coolDown = 1.0f;
    public float currentCoolDown;

	// Use this for initialization
	void Start () {
        index = 0;
        coolDown = 1.0f;
        currentCoolDown = 1.0f;
        spawnPosition = transform.position;
        distanceMin = Mathf.Infinity;
        myAgent = GetComponent<NavMeshAgent>();
       // pointOfInterest = new Transform[3];
        FindTargets();
        myTarget = pointOfInterest[0].gameObject;
        //FindTarget();
        InvokeRepeating("MoveToTarget", 0.1f, 0.5f);
        InvokeRepeating("FindTarget", 0.1f, 0.1f);
        InvokeRepeating("SwitchPosition", 0.1f, 0.1f);
    }

    void FindTargets()
    {
        GameObject[] tempTargets;
        tempTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in tempTargets)
        {
            if (target.gameObject != this.gameObject)
            {
                targets.Add(target);
            }
        }
        
    }

    void OnCollisionEnter(Collision otherCollider)
    {
        /*if (otherCollider.gameObject.tag == "Target")
        {
            targets.Remove(otherCollider.gameObject);
            //FindTarget();
        }*/
        if(otherCollider.gameObject.tag == "Bullet")
        {
            transform.position = spawnPosition;
            myTargetShoot = null;
        }
    }

    void MoveToTarget()
    {
        myAgent.SetDestination(myTarget.transform.position);
    }

    void FindTarget()
    {
        if (targets.Count > 0)
        {
            distanceMin = Mathf.Infinity;
            for (int i = 0; i < targets.Count; ++i)
            {
                Vector3 direction = targets[i].transform.position - transform.position;
                RaycastHit _hit;
                Physics.Raycast(transform.position, direction, out _hit);
                if(_hit.transform.gameObject == targets[i])
                {
                    currentDistance = Vector3.Distance(transform.position, targets[i].transform.position);
                    if (currentDistance < distanceMin)
                    {
                        myTargetShoot = targets[i];
                        distanceMin = currentDistance;
                    }
                }
            }
        }
        if (myTargetShoot != null)
        {
            if(currentCoolDown >= coolDown)
            {
                Shoot(myTargetShoot);
                currentCoolDown = 0;
            }
            else
            {
                currentCoolDown += Time.deltaTime;

            }
                
        }


    }

    void Shoot(GameObject _target)
    {
        transform.LookAt(new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z));
       GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position+ transform.forward*2.0f, Quaternion.identity) as GameObject;

        bullet.transform.LookAt(_target.transform.position);
    }
    /*void ChangeTarget()
    {
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; ++i)
            {
                currentDistance = Vector3.Distance(transform.position, targets[i].transform.position);
                if (currentDistance < distanceMin && currentDistance < 10f)
                {
                    myTargetShoot = targets[i];
                    distanceMin = currentDistance;
                }
            }
        }
    }*/

    void SwitchPosition()
    {
        if(Vector3.Distance(myTarget.transform.position, transform.position)<1.5f)
        {
            if(index == pointOfInterest.Length-1)
            {
                index = 0;
            }else
            {
                index++;
            }
           myTarget = pointOfInterest[index].gameObject;
        }
    }
}
