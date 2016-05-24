using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class JordanComposite : JordanNode {

    protected List<JordanNode> listTask;

	public JordanComposite()
    {

    }

    public void setTasks(List<JordanNode> lTask)
    {
        if (lTask != null)
            listTask = lTask;
    }
}
