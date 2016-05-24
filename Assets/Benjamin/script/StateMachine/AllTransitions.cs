using UnityEngine;
using System.Collections;
using System;

namespace benjamin
{
    /*public class BulletDetected : ITransition
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
    }*/

    public class SeeTarget : ITransition
    {
        AgentLefevre agent;
        GameObject controller;
        AbstractState nextState = new ShootState();
        public bool Check()
        {
            agent = controller.GetComponent<AgentLefevre>();
            agent.RefreshTargets();
            // Do sphere cast and detect if there is an enemy bullet in it
            foreach(GameObject obj in agent.targets)
            {
                RaycastHit hit;
                Vector3 direction = obj.transform.position - agent.transform.position;
                direction.Normalize();
                Vector3 right = Vector3.Cross(direction.normalized, Vector3.up);
                Vector3 left = -right;
                if (Physics.Raycast(agent.transform.position + direction, direction, out hit))
                {
                    if (hit.transform.gameObject == obj)

                    {
                        if (Physics.Raycast(agent.transform.position + direction + right*0.5f, direction, out hit))
                        {
                            if (hit.transform.gameObject == obj)
                            {
                                if (Physics.Raycast(agent.transform.position + direction + left*0.5f, direction, out hit))
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

        public void SetController(GameObject control)
        {
            controller = control;
        }
        public GameObject GetController()
        {
            return controller;
        }
    }

    public class LostTarget : ITransition
    {
        AbstractState nextState = new MoveToState();
        AgentLefevre agent;
        GameObject controller;
        public bool Check()
        {
            agent = controller.GetComponent<AgentLefevre>();
            agent.RefreshTargets();
            // Do sphere cast and detect if there is an enemy bullet in it
            foreach (GameObject obj in agent.targets)
            {
                RaycastHit hit;
                Vector3 direction = obj.transform.position - agent.transform.position;
                direction.Normalize();
                Vector3 right = Vector3.Cross(direction.normalized, Vector3.up);
                Vector3 left = -right;
                if (Physics.Raycast(agent.transform.position+direction, direction, out hit))
                {
                    if (hit.transform.gameObject == obj)

                    {
                        if (Physics.Raycast(agent.transform.position + direction + right *0.5f, direction, out hit))
                        {
                            if (hit.transform.gameObject == obj)
                            {
                                if (Physics.Raycast(agent.transform.position + direction + left * 0.5f, direction, out hit))
                                {
                                    if (hit.transform.gameObject == obj)
                                        return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        public AbstractState GetNextState()
        {
            return nextState;
        }

        public void SetController(GameObject control)
        {
            controller = control;
        }

        public GameObject GetController()
        {
            return controller;
        }
    }

}


