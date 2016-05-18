using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PoulpeState
{
    public GameObject[] targets;
    public GameObject target;

    public abstract void Step();
}
