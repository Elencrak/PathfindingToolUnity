using UnityEngine;
using System.Collections;

public abstract class TransitionWill : MonoBehaviour {
    public StateWill nextState;
    public abstract StateWill check();
}
