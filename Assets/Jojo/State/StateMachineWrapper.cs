using UnityEngine;
using System.Collections;


namespace JojoKiller { 
    public class StateMachineWrapper : IState
    {

        public StateMachine stateMachine;

        public StateMachineWrapper(StateMachine p_stateMachine)
        {
            stateMachine = p_stateMachine;
        }

        public override IState execution()
        {
            IState temp = base.execution();
            if (temp == this)
            {
                stateMachine.execution();
            }

            return temp;
        }
    }
}
