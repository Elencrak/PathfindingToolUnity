using UnityEngine;
using System.Collections;
using System;

public abstract class TaskReloadBenoitV : NodeBenoitV {

    public override  bool Execute()
    {
        Debug.Log("Reload");
        return true;
    }
}
