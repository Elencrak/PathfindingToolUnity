using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateMif
{
	public List<TransitionMif> AllTransition = new List<TransitionMif>();

	public void Init (List<TransitionMif> AllT)
	{
		AllTransition = AllT;
	}

	public abstract void Execute();

	public StateMif Check ()
	{
		foreach (TransitionMif T in AllTransition) 
		{
			StateMif S = T.Check ();
			if (S != null) {return S;}
		}
		return this;
	}

	public abstract void Finish();
}
