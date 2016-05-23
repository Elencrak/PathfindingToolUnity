﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamFourbeManager : MonoBehaviour {

    List<NewPierreAgent> agents = new List<NewPierreAgent>();
    
	// Use this for initialization
	void Start () {

	    foreach(NewPierreAgent agent in GetComponentsInChildren<NewPierreAgent>())
        {
            agents.Add(agent);

            agent.stateMachine.currentState = new PierreCamp(agent.stateMachine);
        }

        Invoke("BeginGame", 1);

        InvokeRepeating("CheckStrat", 30, 20);

	}
	
	// Update is called once per frame
	void Update () {

    }

    void BeginGame()
    {
        agents[0].stateMachine.currentState = new PierreOffensif(agents[0].stateMachine);
        agents[1].stateMachine.currentState = new PierreDefensif(agents[1].stateMachine);
        agents[2].stateMachine.currentState = new PierreRandom(agents[2].stateMachine);
    }

    void CheckStrat()
    {
        NewPierreAgent badAgent = agents[0], bestAgent = agents[0];

        foreach(NewPierreAgent a in agents)
        {
            if (a.nbTimeTouched > badAgent.nbTimeTouched)
            {
                badAgent = a;
            }
            else if(a.nbTimeTouched < bestAgent.nbTimeTouched)
            {
                bestAgent = a;
            }
        }

        badAgent.stateMachine.currentState = bestAgent.stateMachine.currentState;
    }
}
