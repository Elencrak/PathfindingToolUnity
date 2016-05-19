using UnityEngine;
using System.Collections;
using System;

namespace benjamin
{
    public class MoveToState : AbstractState {
        AgentLefevre agent;
        

        public override void Init()
        {
            Debug.Log("InitMoveToState");
            AddTransition(new SeeTarget());


            agent = AgentLefevre.instance;

            //Select your pathfinding
            agent.graph = new Pathfinding();
            agent.graph.Load("benPath");
            agent.graph.setNeighbors();
            //
            
            float dist = Mathf.Infinity;
            GameObject[] test = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject obj in test)
            {
                if (obj == agent.gameObject)
                    continue;
                float tmp = Vector3.Distance(agent.transform.position, obj.transform.position);
                if (tmp < dist)
                {
                    dist = tmp;
                    agent.target = obj;
                }
                agent.targets.Add(obj);

            }
            agent.road = PathfindingManager.GetInstance().GetRoad(agent.transform.position, agent.target.transform.position, agent.graph);
            agent.InvokeRepeating("UpdateRoad", 0.5f, 0.5f);
        }

        // Update is called once per frame
        public override void StateUpdate ()  {
            Debug.Log("currentState = MoveToState");
            if (agent.road.Count > 0)
            {
                agent.currentTarget = agent.road[0];
                if (Vector3.Distance(agent.transform.position, agent.currentTarget) < agent.closeEnoughRange)
                {
                    agent.road.RemoveAt(0);
                    agent.currentTarget = agent.road[0];
                }
                else
                {
                    agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.currentTarget+Vector3.up, agent.speed * Time.deltaTime);
                }
            }
            else
            {
                agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.target.transform.position, agent.speed * Time.deltaTime);
            }
        }
        
    }
}
