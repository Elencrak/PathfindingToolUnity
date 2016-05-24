using UnityEngine;
using System.Collections;

public class AgentFunctions : MonoBehaviour {
    public int _id;
    public Transform _target;

    NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Move()
    {
        if (_id == 1)
        {
            _agent.ResetPath();
            _agent.SetDestination(_target.position);
        }
    }

    public void StandBy()
    {
        _agent.ResetPath();
        _agent.SetDestination(transform.position);
    }
}
