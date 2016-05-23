using UnityEngine;
using System.Collections;

public class CoverStateBenoitV : StateBenoitV {

    public override void Execute(GameObject parAgent)
    {
        parAgent.GetComponent<MyAgentGuardBenoitV>().CoverBoss();
        Debug.Log(parAgent + "Cover");
    }
}
