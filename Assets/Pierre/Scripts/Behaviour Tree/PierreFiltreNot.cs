using UnityEngine;
using System.Collections;

public class PierreFiltreNot : PierreFiltre {

    public override bool Execute()
    {
        return !node.Execute();
    }
}
