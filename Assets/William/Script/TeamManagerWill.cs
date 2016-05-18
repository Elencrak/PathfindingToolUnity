using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamManagerWill : MonoBehaviour {
    public static TeamManagerWill instance;
    public List<Will_IA_M2> members;
    public List<GameObject> ennemis;
    public GameObject mainTarget;
    GameObject mainEnnemie;

    // Use this for initialization
    void Awake () {
        instance = this;
        members = new List<Will_IA_M2>(GetComponentsInChildren<Will_IA_M2>());
        ennemis = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        ennemis.Remove(members[0].gameObject);
        if (members.Count > 1)
        {
            ennemis.Remove(members[1].gameObject);
            ennemis.Remove(members[2].gameObject);
        }
        
    }
	
}
