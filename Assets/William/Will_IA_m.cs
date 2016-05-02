using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Will_IA_m : MonoBehaviour {
    float shootCooldown=1;
    //float range = 10;
    Vector3 spawn;
    List<GameObject> targets;
    GameObject currentTarget;
    NavMeshAgent agent;
    

    void Start () {
        spawn = transform.position;
        agent = GetComponent<NavMeshAgent>();
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        targets.Remove(this.gameObject);

        InvokeRepeating("targetUpdate", 0, 0.8f);
        InvokeRepeating("shoot", 1, shootCooldown);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void move()
    {
        GameObject tempTarget = targets[0];
        Vector3 myPos = transform.position;
        float distance = Vector3.Distance(myPos, targets[0].transform.position);

        for (int i = 1; i < targets.Count; i++)
        {
            float tempDist = Vector3.Distance(myPos, targets[i].transform.position);
            if (tempDist < distance)
            {
                distance = tempDist;
                tempTarget = targets[i];
            }
        }
        currentTarget = tempTarget;
        agent.SetDestination(currentTarget.transform.position);
        if (distance < 15)
        {

            agent.SetDestination(transform.right*Random.Range(-5,5));
            
        }
    }

    void targetUpdate()
    {
        //if (shooting == true)
        //{
        //    float dist = Vector3.Distance(transform.position, currentTarget.transform.position);
        //    if (dist < range)
        //    {
        //        strafe();
        //        return;
        //    }
        //    else
        //    {
        //        shooting = false;
        //    }
        //}
        move();
        
    }

    

    void OnCollisionEnter(Collision col)
    {
        //if (col.collider.gameObject == currentTarget)
        //{
        //    targets.Remove(currentTarget);
        //    targetUpdate();
        //}
        if (col.collider.tag == "Bullet")
        {
            transform.position = spawn;
            targetUpdate();
        }
    }
    

    void shoot()
    {
        if (currentTarget == null)
            return;

        RaycastHit hit;
        Vector3 dir = currentTarget.transform.position - transform.position;
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            //Debug.Log("ray " + hit.collider.name);
            if (hit.collider.tag == currentTarget.tag)
            {
                //Debug.Log("shoot");
                GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"), transform.position, transform.rotation);
                bullet.transform.LookAt(currentTarget.transform.position);
                Physics.IgnoreCollision(GetComponent<BoxCollider>(), bullet.GetComponent<CapsuleCollider>());
            }
            
        }

    }

    void win()
    {
        currentTarget = null;
        agent.SetDestination(new Vector3(1,15,2));
        Debug.Log("Mission completed by " + gameObject.name);
    }
}
