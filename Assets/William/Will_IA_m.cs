using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Will_IA_m : MonoBehaviour {
    
    float shootCooldown=1;
    float range = 20;
    Vector3 spawn;
    List<GameObject> targets;
    GameObject currentTarget;
    NavMeshAgent agent;
    GameObject bullet;

    void Start () {
        spawn = transform.position;
        agent = GetComponent<NavMeshAgent>();
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        targets.Remove(this.gameObject);
        bullet = new GameObject();
        bullet = (GameObject) Resources.Load("Bullet");
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
        if (distance < range)
        {
            if (Random.Range(0, 3) > 1)
            {
                agent.SetDestination(transform.right * -10);
            }
            else
            {
                agent.SetDestination(transform.right * 10);
            }
            //agent.SetDestination(transform.right * Random.Range(-5, 5));
        }
    }

    void targetUpdate()
    {
        move();
        
    }

    

    void OnCollisionEnter(Collision col)
    {
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
                GameObject spawnedBullet = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
                spawnedBullet.transform.LookAt(currentTarget.transform.position);
                spawnedBullet.GetComponent<bulletScript>().launcherName = "TeamWill";
                Physics.IgnoreCollision(GetComponent<BoxCollider>(), spawnedBullet.GetComponent<CapsuleCollider>());
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
