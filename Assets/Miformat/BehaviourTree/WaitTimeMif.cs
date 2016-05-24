using UnityEngine;
using System.Collections;

public class WaitTimeMif : NodeMif 
{
	public float WTime = 2;
	float timer = 0;

	public override bool Execute()
	{
		timer += Time.deltaTime;
		if (timer > WTime)
		{
			Debug.Log ("Time is ok");
			return true;
		}
		return false;
	}
}
