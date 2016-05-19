using UnityEngine;
using System.Collections;
using System;

namespace benjamin
{
    public class BulletDetected : ITransition
    {
        AbstractState nextState = new MoveToState();
        public bool Check()
        {
            // Do sphere cast and detect if there is an enemy bullet in it
            Collider[] cols = Physics.OverlapSphere(AgentLefevre.instance.gameObject.transform.position, 5f);
            foreach (Collider col in cols)
                if (col.GetComponent<bulletScript>())
                    return true;

            return false;
        }

        public AbstractState GetNextState()
        {
            return nextState;
        }
    }

    public class SeeTarget : ITransition
    {
        AbstractState nextState = new ShootState();
        public bool Check()
        {
            AgentLefevre agent = AgentLefevre.instance;

            // Do sphere cast and detect if there is an enemy bullet in it
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Target"))
            {
                RaycastHit hit;
                Vector3 direction = obj.transform.position - agent.transform.position;
                direction.y = 0;
                direction.Normalize();
                Vector3 right = Vector3.Cross(direction.normalized, Vector3.up);
                Vector3 left = -right;
                //if (Physics.CapsuleCast(agent.transform.position + direction.normalized, agent.target.transform.position, 1f, direction.normalized, out hit))
                if (Physics.Raycast(agent.transform.position, direction, out hit))
                {
                    if (hit.transform.gameObject == obj)

                    {
                        if (Physics.Raycast(agent.transform.position+right*0.2f, direction, out hit))
                        {
                            if (hit.transform.gameObject == obj)
                            {
                                if (Physics.Raycast(agent.transform.position+left*0.2f, direction, out hit))
                                {
                                    if (hit.transform.gameObject == obj)
                                        return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public AbstractState GetNextState()
        {
            return nextState;
        }
    }

    public class LostTarget : ITransition
    {
        AbstractState nextState = new MoveToState();
        public bool Check()
        {
            AgentLefevre agent = AgentLefevre.instance;

            // Do sphere cast and detect if there is an enemy bullet in it
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Target"))
            {
                RaycastHit hit;
                Vector3 direction = obj.transform.position - agent.transform.position;
                if (Physics.Raycast(agent.transform.position + agent.transform.forward, direction.normalized, out hit))
                {
                    if (hit.transform.gameObject == obj)
                        return false;
                }
            }
            return true;
        }

        public AbstractState GetNextState()
        {
            return nextState;
        }
    }

}


