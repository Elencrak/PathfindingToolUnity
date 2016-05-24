using UnityEngine;
using System.Collections;

public class PierreGetInputTask : PierreTask{

    KeyCode code;

    public PierreGetInputTask(KeyCode c)
    {
        code = c;
    }

    public override bool Execute()
    {
        Debug.Log("key "+code+" "+ Input.GetKey(code));
        return Input.GetKey(code);
    }
}
