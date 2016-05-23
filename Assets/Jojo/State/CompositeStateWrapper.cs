using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Solution trouver avec Stéphane pour pouvoir avoir un 
// etat qui peux influencer plusieurs Type d'agent.
// En effet le on ajoute des states lier avec un agent 
// qu'on ajoute ensuite dans le CompositeStateWrapper

namespace JojoKiller { 
    public class CompositeStateWrapper : IState {


        public List<IState> listToExecuteState;

        public CompositeStateWrapper()
        {

        }

        public CompositeStateWrapper(List<IState> p_ListOfState)
        {
            listToExecuteState = p_ListOfState;
        }

        public override IState execution()
        {
            IState temp = base.execution();
            if (temp == this) { 
                foreach (IState s in listToExecuteState) {
                    s.execution();
                }
            }
            return temp;
        }   

        public void addListToExecuteState(IState p_StateToAdd)
        {
            listToExecuteState.Add(p_StateToAdd);
        }
    }
}
