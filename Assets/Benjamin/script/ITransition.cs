using UnityEngine;
using System.Collections;

namespace benjamin
{

    public interface ITransition  {

        bool Check();

        AbstractState GetNextState();
        
    }
}
