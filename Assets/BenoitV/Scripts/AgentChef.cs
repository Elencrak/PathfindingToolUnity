using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentChef : MonoBehaviour {

    public Animator anim;
    Vector3 spawnPosition;
    public List<GameObject> targets;
    NavMeshAgent myAgent;
    public Vector3 myTarget;
    public GameObject myTargetShoot;
    float distanceMin;
    float currentDistance;
    public Transform[] points;
    public List<Vector3> pointOfInterest;
    int index;
    string myTeamName;

    float coolDown = 1.0f;
    public float currentCoolDown;


    void Start()
    {

        index = 0;
        coolDown = 1.0f;
        currentCoolDown = 1.0f;
        spawnPosition = transform.position;
        distanceMin = Mathf.Infinity;
        myAgent = GetComponent<NavMeshAgent>();
        myTeamName = transform.parent.GetComponent<TeamNumber>().teamName;


        pointOfInterest = new List<Vector3>();
        for (int i = 0; i < points.Length; ++i)
        {
            pointOfInterest.Add(points[i].position);
        }
        myTarget = pointOfInterest[0];

        FindTargets();
        InvokeRepeating("MoveToTarget", 0.1f, 0.1f);
        InvokeRepeating("FindTarget", 0.1f, 0.1f);
        InvokeRepeating("SwitchPosition", 0.1f, 0.1f);
    }

    void Update()
    {
        
    }

    void FindTargets()
    {
        GameObject[] tempTargets;
        tempTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in tempTargets)
        {
            if (target.gameObject != this.gameObject && !target.name.Contains("BenoitV")/* && target.transform.parent.GetComponent<TeamNumber>().teamName != myTeamName*/)
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
        if (otherCollider.gameObject.tag == "Bullet")
        {
            myAgent.Warp(spawnPosition);

            //transform.position = spawnPosition;
            myTargetShoot = null;
        }
    }

    void MoveToTarget()
    {
        myAgent.SetDestination(myTarget);
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
                if (_hit.transform.gameObject == targets[i])
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
            if (currentCoolDown >= coolDown)
            {

                Shoot(myTargetShoot);

            }
            currentCoolDown += 0.1f;
        }


    }

    void Shoot(GameObject _target)
    {

        RaycastHit _hit;
        Physics.Raycast(transform.position, myTargetShoot.transform.position - transform.position, out _hit);
        if (_hit.collider.gameObject == myTargetShoot)
        {
            transform.LookAt(new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z));

            GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward * 2.0f, Quaternion.identity) as GameObject;
            bullet.GetComponent<bulletScript>().launcherName = "TeamTank";
            float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
            float ratio = distanceToTarget / 40f;
            Vector3 _velocity = _target.GetComponent<NavMeshAgent>().velocity;

            bullet.transform.LookAt(_target.transform.position + _velocity * ratio);
            Debug.DrawLine(transform.position, _target.transform.position + _velocity * ratio, Color.red, 0.5f);
            currentCoolDown = 0;
        }
        else
        {
            myTargetShoot = null;
        }
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
        if (Vector3.Distance(myTarget, transform.position) < 1.5f )
        {
            anim.SetBool("Patrouille", true);
            anim.SetBool("Deplacement", false);

            if (index == points.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            myTarget = pointOfInterest[index];
        }
    }
}
