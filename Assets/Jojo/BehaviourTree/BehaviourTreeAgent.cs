using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using JojoBehaviourTree;
public class BehaviourTreeAgent : MonoBehaviour {


    [Header("Internal")]
    bool doOnce = true;
    private List<Transform> targets = new List<Transform>();
    public Transform currentTarget;
    private Selector rootSelector;

    // Use this for initialization
    void Start () {

        rootSelector = new Selector();
        Sequence seeSequence = new Sequence();

        SeeOpponent seeOpponent = new SeeOpponent(this);
        Shoot shoot = new Shoot(this);
        Move move = new Move(this);

        seeSequence.addElementIncomposite(seeOpponent);
        seeSequence.addElementIncomposite(shoot);

        rootSelector.addElementIncomposite(seeSequence);
        rootSelector.addElementIncomposite(move);


    }
	
	// Update is called once per frame
	void Update () {
        //Récupération des target
        if (doOnce)
        {
            doOnce = false;
            GameObject[] tempArray;
            tempArray = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject g in tempArray)
            {
                if (g.GetInstanceID() != gameObject.GetInstanceID())
                {
                    targets.Add(g.transform);
                }
            }
        }
        rootSelector.execute();
    }
}
