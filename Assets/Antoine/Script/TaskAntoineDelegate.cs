using UnityEngine;
using System.Collections;

public delegate bool ExecuteDelagate();

public class TaskAntoineDelegate: TaskAntoine
{
    public ExecuteDelagate theDelegate;

    public TaskAntoineDelegate(ExecuteDelagate aDelegate)
    {
        theDelegate = aDelegate;
    }

    public override bool Execute()
    {
        return theDelegate();
    }

}
