using UnityEngine;
using System.Collections;

public abstract class JordanNode {

    protected GameObject target;
    protected Transform transform;

	public JordanNode()
    {
    }

    public JordanNode(GameObject tar, Transform tran)
    {
        target = tar;
        transform = tran;
    }

    public void setTarget(GameObject tar)
    {
        if (tar != null)
            target = tar;
    }

    public void setTransform(Transform tran)
    {
        if (tran != null)
            transform = tran;
    }

    public abstract bool execute();
}
