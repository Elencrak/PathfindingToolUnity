using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class StateMachineMif : StateMif {

	StateMif currentState;

	public StateMachineMif(StateMif CS)
	{
		currentState = CS;
	}

	//public override void Init (List<TransitionMif> AllT){}

	public override void Execute ()
	{
		currentState.Execute ();
		currentState = currentState.Check ();
	}

	/*public override StateMif Check()
	{
		return currentState.Check ();
	}*/

	public override void Finish (){}

}
