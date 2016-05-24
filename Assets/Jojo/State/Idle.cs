using UnityEngine;
using System.Collections;

namespace JojoKiller {

    public class Idle : IStateAgent{

        public Idle(Member currentAgent)
        {
            member = currentAgent;
        }
        void Awake()
        {
        
        }

        public override IState execution()
        {
            
            IState temp = base.execution();
            if(temp == this)
            {
                Debug.Log("Idle");
                member.idle();
            }                        
            return temp;           
            /* if (needToChangeTarget || nextMove <= 0)
             {
                 targetPosition = stateTargets[0];
                 foreach (Transform g in stateTargets)
                 {
                     if (g.GetInstanceID() != gameObject.GetInstanceID())
                     {
                         Vector3 relativePosition;
                         Vector3 relativePositionTarget;
                         relativePosition = g.position - transform.position;
                         relativePositionTarget = targetPosition.position - transform.position;
                         if (relativePositionTarget.magnitude > relativePosition.magnitude)
                         {
                             targetPosition = g;
                         }
                     }
                 }
                 needToChangeTarget = false;
                 nextMove = move;

                 currentNavMeshAgent.SetDestination(targetPosition.position);
             }
             else
             {
                 nextMove -= Time.deltaTime;
             }*/
        }
    }
}