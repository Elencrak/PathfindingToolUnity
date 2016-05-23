using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentFunctions : MonoBehaviour {
    public int _id;
    public Transform _target;

    int index;
    NavMeshAgent _agent;
    public GameObject _boss;
    public GameObject _guard;

    public List<Transform> _listOfEnemies;

    string myTeamName;

    float coolDown = 1.0f;
    public float currentCoolDown;
    public Vector3 myTarget;
    public GameObject myTargetShoot;
    float distanceMin;
    float currentDistance;
    

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        FindTargets();
        InvokeRepeating("FindTarget", 0.1f, 0.1f);
    }

    public void Move()
    {
        _agent.ResetPath();
        if(_id == 1)
        {
            _target = _guard.GetComponent<AgentFunctions>()._target;
        }
        else if(_id == 2)
        {
            GetComponent<MyAgentGuardBenoitV>().cover = false;
            _boss.GetComponent<MyAgentChefBenoitV>().itsSecure = false;
            SwitchPosition(_target);
        }else if(_id == 3)
        {
            //FindTarget();
            _target = _listOfEnemies[Random.Range(0, _listOfEnemies.Count)];
        }
        _agent.SetDestination(_target.position);
    }

    public void StandBy()
    {
        if(_id==2)
        {
            GetComponent<MyAgentGuardBenoitV>().cover = false;
        }
        _agent.ResetPath();
        _agent.SetDestination(transform.position);
    }

    public float DistanceWithOtherAgent(GameObject parAgent1, Transform parAgent2)
    {
        return Vector3.Distance(parAgent1.transform.position, parAgent2.position);
    }

    void SwitchPosition(Transform parTarget)
    {
        if (Vector3.Distance(parTarget.position, transform.position) < 4.0f && Vector3.Distance(_boss.transform.position, transform.position) < 4.0f)
        {
            if (index == GetComponent<MyAgentGuardBenoitV>()._pointsOfInterest.Count - 1)
            {
                //index = 0;
            }
            else
            {
                index++;
                
            }
            _target = GetComponent<MyAgentGuardBenoitV>()._pointsOfInterest[index];
            GetComponent<AgentFunctions>()._boss.GetComponent<MyAgentChefBenoitV>().itsSecure = false;
        }
    }

    public void FindTargets()
    {
        GameObject[] tempTargets;
        tempTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in tempTargets)
        {
            if (target.gameObject != this.gameObject && !target.name.Contains("BenoitV")/* && target.transform.parent.GetComponent<TeamNumber>().teamName != myTeamName*/)
            {
                _listOfEnemies.Add(target.transform);
            }
        }

    }

    void FindTarget()
    {
        if (_listOfEnemies.Count > 0)
        {
            distanceMin = Mathf.Infinity;
            for (int i = 0; i < _listOfEnemies.Count; ++i)
            {
                Vector3 direction = _listOfEnemies[i].transform.position - transform.position;
                RaycastHit _hit;
                Physics.Raycast(transform.position, direction, out _hit);
                if (_hit.transform.gameObject == _listOfEnemies[i])
                {
                    currentDistance = Vector3.Distance(transform.position, _listOfEnemies[i].transform.position);
                    if (currentDistance < distanceMin)
                    {
                        myTargetShoot = _listOfEnemies[i].gameObject;
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
            currentCoolDown = 0;
        }
        else
        {
            myTargetShoot = null;
        }
    }

}
