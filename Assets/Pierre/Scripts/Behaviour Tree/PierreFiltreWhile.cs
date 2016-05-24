using UnityEngine;
using System.Collections;

public class PierreFiltreWhile : PierreFiltre {

    public override bool Execute()
    {
        bool b = true;

        do
        {
            b = node.Execute();
        }
        while (b);

        return b;
    }
}
