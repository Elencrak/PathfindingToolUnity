using UnityEngine;
using System.Collections;

namespace benjamin
{

    public interface ITransition  {

        

        bool Check();

        AbstractState GetNextState();
        void SetController(GameObject control);
        GameObject GetController();

    }
}
