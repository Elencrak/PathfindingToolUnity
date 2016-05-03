using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamWillScript : MonoBehaviour {

    List<Will_IA_m> members;
    public List<GameObject> ennemis;

    // Use this for initialization
    void Awake () {
        members = new List<Will_IA_m>(GetComponentsInChildren<Will_IA_m>());
        ennemis = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        ennemis.Remove(members[0].gameObject);
        ennemis.Remove(members[1].gameObject);
        ennemis.Remove(members[2].gameObject);
    }
	
}
