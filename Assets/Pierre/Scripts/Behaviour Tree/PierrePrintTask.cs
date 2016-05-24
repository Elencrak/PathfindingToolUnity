using UnityEngine;
using System.Collections;

public class PierrePrintTask : PierreTask {

    string message;

    public PierrePrintTask(string s)
    {
        message = s;
    }

    public override bool Execute()
    {
        Debug.Log(message);
        return true;
    }
}
