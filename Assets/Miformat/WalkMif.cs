using UnityEngine;
using System.Collections;

public class WalkMif : StateMif {

	int ID;
	int state;

	public override void Execute()
	{
		getVect (ID,state);
	}

	public override void Finish()
	{

	}

	Vector3 getVect(int MID, int MState)
	{
		Debug.Log ("WalkToPoint");
		if (MID == 0) 
		{
			if (MState == 0) {return  new Vector3 (-73, 1, -16);} 
			else {return new Vector3 (-65, 1, -16);}
		}
		else if (MID == 1) 
		{
			if (MState == 0) {return  new Vector3 (-52,1,-14);} 
			else {return new Vector3 (-52,1,-22);}
		}
		else if (MID == 2) 
		{
			if (MState == 0) {return  new Vector3 (-62,1,-8);} 
			else {return new Vector3 (-43,1,-18);}
		}
		return Vector3.zero;
	}
}
