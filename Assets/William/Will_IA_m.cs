using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Will_IA_m : MonoBehaviour {
    
    float shootCooldown= 1;
    float range = 20;
    Vector3 spawn;
    List<GameObject> targets;
    GameObject currentTarget;
    NavMeshAgent agent;
    GameObject bullet;
    bool canShoot = false;
    Vector3 strafeDest;
    float lastShoot=0;
    public bool isStrafing = false;
    void Start () {
        spawn = transform.position;
        agent = GetComponent<NavMeshAgent>();
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        targets.Remove(this.gameObject);
        bullet = new GameObject();
        bullet = (GameObject) Resources.Load("Bullet");
        InvokeRepeating("targetUpdate", 0, 0.3f);
        //InvokeRepeating("shoot", 0.1f, shootCooldown);
    }
	
	void Update () {
        shoot();
	}

    void move()
    {
        
        
    }

    void targetUpdate()
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
        if (!isStrafing)
        {
            agent.SetDestination(currentTarget.transform.position);
        }
    }
    

    IEnumerator startStrafe()
    {
        isStrafing = true;
        Vector3 pos = transform.position;
        strafeDest = transform.position+(transform.right * 5);

        agent.SetDestination(strafeDest);
        while (Vector3.Distance(strafeDest, transform.position) > 1)
        {
            yield return 1;
        }
        agent.SetDestination(pos);
        while (Vector3.Distance(pos, transform.position) > 1)
        {
            yield return 1;
        }

        //yield return new WaitForSeconds(1f);
    }

    void OnTriggerEnter()
    {

    }

    void strafe()
    {
        if(!isStrafing)
        StartCoroutine(startStrafe());
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Bullet")
        {
            isStrafing = false;
            StopAllCoroutines();
            agent.Warp(spawn);
            transform.position = spawn;
            targetUpdate();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("trigger");
        if(col.tag == "Bullet")
        strafe();
    }

    void shoot()
    {
        if (currentTarget == null)
            return;
        if (lastShoot + shootCooldown > Time.time)
            return;
        
        RaycastHit hit;
        Vector3 dir = currentTarget.transform.position - transform.position;
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            //Debug.Log("ray " + hit.collider.name);
            if (hit.collider.tag == currentTarget.tag)
            {                
                GameObject spawnedBullet = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
                spawnedBullet.transform.LookAt(currentTarget.transform.position + (currentTarget.GetComponent<Rigidbody>().velocity));
                spawnedBullet.GetComponent<bulletScript>().launcherName = "TeamWill";
                Physics.IgnoreCollision(GetComponent<BoxCollider>(), spawnedBullet.GetComponent<CapsuleCollider>());
                lastShoot = Time.time;                
            }
            else
            {
                isStrafing = false;
                StopAllCoroutines();
            }
            
        }

    }
}
