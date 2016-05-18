using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateWill : MonoBehaviour {

    public List<TransitionWill> transition;
    public abstract void execute();
    protected abstract void check();
}
