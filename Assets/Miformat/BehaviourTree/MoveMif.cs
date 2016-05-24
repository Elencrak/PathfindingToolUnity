using UnityEngine;
using System.Collections;

public class MoveMif : NodeMif 
{
	Vector3 oldPos;
	BehaviourMif BM;

	public MoveMif(BehaviourMif _BM)
	{
		BM = _BM;
		oldPos = BM.gameObject.transform.position;
	}

	public override bool Execute()
	{
		if (oldPos != BM.gameObject.transform.position) 
		{
			oldPos = BM.gameObject.transform.position;
			Debug.Log ("I'm moving");
			return true;
		}
		oldPos = BM.gameObject.transform.position;
		return false;
	}
}
