using UnityEngine;
using System.Collections;

public class JordanCanShootTask : JordanNode {

    public float delayShot, startDelay;
    public float distance;

    public JordanCanShootTask()
    {
        delayShot = 1.0f;
        startDelay = 0.0f;
        distance = 7.0f;
    }

    public override bool execute()
    {

        if (startDelay + delayShot < Time.time && Vector3.Distance(transform.position, target.transform.position) < distance)
            return true;

        return false;
    }
}
