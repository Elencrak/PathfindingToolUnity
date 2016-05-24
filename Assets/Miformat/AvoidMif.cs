using UnityEngine;
using System.Collections;

public class AvoidMif : StateMif 
{
	GameObject agent;

	public override void Execute()
	{
		Avoid ();
	}

	public override void Finish()
	{

	}

	void Avoid()
	{
		Debug.Log ("Avoid");
		if (agent) 
		{
			Vector3 newDest = agent.transform.position;
			if (agent.transform.position.x > agent.transform.position.x) {newDest += agent.transform.right*2;}
			else if(agent.transform.position.x < agent.transform.position.x) {newDest -= agent.transform.right*2;}
			
			if (agent.transform.position.z > agent.transform.position.z) {newDest += agent.transform.forward;}
			else if(agent.transform.position.z < agent.transform.position.z) {newDest -= agent.transform.forward;}
		}
	}
}
