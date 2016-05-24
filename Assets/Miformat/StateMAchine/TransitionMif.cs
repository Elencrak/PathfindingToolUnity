using UnityEngine;
using System.Collections;

public delegate bool check();

public class TransitionMif
{
	StateMif next;
	check condition;

	public TransitionMif (StateMif nextState, check _cond)
	{
		next = nextState;
		condition = _cond;
	}

	public StateMif Check ()
	{
		if (condition()) {return next;} 
		else {return null;}
	}

}
