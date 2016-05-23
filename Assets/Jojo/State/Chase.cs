using UnityEngine;
using System.Collections;
using System;

namespace JojoKiller
{
    // Prendre la décision
    public class Chase : IStateAgent
    {
        public Chase(Member currentAgent)
        {
            member = currentAgent;
        }

        public override IState execution()
        {
            IState temp = base.execution();
            if (temp == this)
            {
                Debug.Log("Fire");
                //member.shoot();
            }
            return temp;
        }
    }
}
