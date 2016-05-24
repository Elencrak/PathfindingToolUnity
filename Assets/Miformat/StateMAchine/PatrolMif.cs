using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolMif : StateMif 
{
	GameObject agent;
	Vector3 destination;
	bool isGo;
	int state;

	public override void Execute()
	{
		Patrol ();
	}
		
	public override void Finish()
	{
		
	}

	void Patrol()
	{
		//Debug.Log ("Patrol");
		if (agent) 
		{
			if (state == 0){state = 1;} 
			else {state = 0;}
			agent.transform.position = destination;
			isGo = true;
		}
	}

}
