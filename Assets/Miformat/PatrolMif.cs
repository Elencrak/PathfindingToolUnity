using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolMif : StateMif 
{
	
	/*public override void Init (List<TransitionMif> AllT)
	{
		AllTransition = AllT;
	}*/

	public override void Execute()
	{
		//Debug.Log ("Ca marche");
	}

	/*public override StateMif Check ()
	{
		foreach (TransitionMif T in AllTransition) 
		{
			StateMif S = T.Check ();
			if (S != null) {return S;}
		}
		return this;
	}*/

	public override void Finish()
	{
		
	}

}
