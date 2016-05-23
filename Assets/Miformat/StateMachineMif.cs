using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class StateMachineMif : StateMif {

	StateMif currentState;

	public StateMachineMif(StateMif CS)
	{
		currentState = CS;
	}

	public override void Execute ()
	{
		currentState = currentState.Check ();
		currentState.Execute ();
	}

	public override void Finish (){}

}
