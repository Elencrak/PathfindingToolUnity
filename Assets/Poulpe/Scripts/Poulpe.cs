using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poulpe : MonoBehaviour
{
    List<PoulpeState> firstStates;
    List<PoulpeState> secondStates;

    PoulpeStateMachine firstStateMachine;
    PoulpeStateMachine secondStateMachine;

    Vector3 begin;

    void Start()
    {
        begin = transform.position;
        PoulpeMove move = new PoulpeMove(this.GetComponent<NavMeshAgent>());
        secondStates.Add(move);
        secondStateMachine = new PoulpeStateMachine(secondStates, move);
        firstStates.Add(secondStateMachine);
        firstStates.Add(new PoulpeDogge(this.gameObject));
        firstStates.Add(new PoulpeShoot(this.gameObject));
        firstStateMachine = new PoulpeStateMachine(firstStates, move);
    }

	void Update ()
    {
        firstStateMachine.Step();
	}


    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                GetComponent<NavMeshAgent>().Warp(begin);
                break;
            case "Target":
                break;
        }
    }
}