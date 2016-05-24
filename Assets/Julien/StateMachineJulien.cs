using UnityEngine;
using System.Collections;

public class StateMachineJulien : StateJulien {

    StateJulien currentState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void patrol()
    {
        currentState.patrol();
    }
}
