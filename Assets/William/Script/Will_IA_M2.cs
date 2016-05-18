using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Will_IA_M2 : MonoBehaviour {
    [Range(0,3)]
    public int id;

    [Header("Walk")]
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    public  string graphName ="will";

    [Header("Shoot")]
    public GameObject bullet;
    public float shootSpeed=1;


    StateMachineWill machine;
    

	void Start () {
        List<StateWill> listState = new List<StateWill>();

        List<TransitionWill> listTrans= new List<TransitionWill>();

        SW_Shoot shoot = new SW_Shoot(id, bullet, shootSpeed);

        listTrans.Add(new TW_DistanceTarget(shoot, false, 4));
        
        listState.Add(new SW_Walk(id,speed, closeEnoughRange, graphName, listTrans));
        listState.Add(shoot);
        machine = new StateMachineWill(listState);
    }
	
	void Update () {
        machine.execute();
	}
}
