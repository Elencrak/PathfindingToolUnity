using UnityEngine;
using System.Collections;

public class WaitInputMif : NodeMif 
{
	public KeyCode KC = KeyCode.P;

	public override bool Execute()
	{
		if (Input.GetKeyDown(KC)) 
		{
			Debug.Log ("Input");
			return true;
		}
		return false;
	}
}
