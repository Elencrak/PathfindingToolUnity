using UnityEngine;
using System.Collections;

public class JordanAimTask : JordanNode {

	public JordanAimTask()
    { }

    public override bool execute()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, target.transform.position, out hit);

        if (hit.collider.tag == "Target")
            return true;

        return false;
    }
}
