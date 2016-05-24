using UnityEngine;
using System.Collections;

public class BehaviourMif : MonoBehaviour {

	SelectorMif SelM;
	SequenceMif SeqM;

	// Use this for initialization
	void Start () 
	{
		SelM = new SelectorMif();
		SeqM = new SequenceMif();

		WaitTimeMif T1 = new WaitTimeMif();
		WaitInputMif T2 = new WaitInputMif();
		MoveMif T3 = new MoveMif(this);

		SeqM.AddElem(T1);
		SeqM.AddElem(T2);

		SelM.AddElem(SeqM);
		SelM.AddElem(T3);
	}
	
	// Update is called once per frame
	void Update () 
	{
		SelM.Execute();
	}
}
