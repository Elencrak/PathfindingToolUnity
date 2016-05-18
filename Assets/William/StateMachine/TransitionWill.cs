using UnityEngine;
using System.Collections;

public abstract class TransitionWill : MonoBehaviour {
    StateWill nextState;
    public abstract StateWill check();
}
