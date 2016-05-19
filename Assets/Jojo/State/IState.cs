using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JojoKiller { 
    /// <summary>
    /// Cette partie est la zone de prise de décision de l'agent. 
    /// Du coup in instancie pas les éléments de gameplay
    /// mais plutôt prépare l'action comme check de la visibilité de la 
    /// cible, distance target ....
    /// </summary>
    [System.Serializable]
    public class IState {

        [HideInInspector]
        public static float coolDown = 2f;

        protected List<Transform> stateTargets;
        protected NavMeshAgent currentNavMeshAgent;
        protected Vector3 startPosition;
        protected Transform targetPosition;
        protected List<Transition> transition = new List<Transition>();

        public virtual IState execution()
        {          
            foreach (Transition tt in this.transition)
            {
                IState temp = tt.check();
                if (temp != null)
                {
                    return temp;
                }
            }
            return this;
        }

        public void addTransition(Transition p_transmission)
        {
            transition.Add(p_transmission);
        }

        public float getCoolDown()
        {
            return coolDown;
        }
    }

}