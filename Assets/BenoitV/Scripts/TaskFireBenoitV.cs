using UnityEngine;
using System.Collections;
using System;

public class TaskFireBenoitV : NodeBenoitV {

    public override bool Execute()
    {
        Debug.Log("Fire");
        return true;
    }
}
