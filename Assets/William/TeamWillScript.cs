using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamWillScript : MonoBehaviour {

    List<Will> members;
    public List<GameObject> ennemis;

    // Use this for initialization
    void Awake () {
        members = new List<Will>(GetComponentsInChildren<Will>());
        ennemis = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        ennemis.Remove(members[0].gameObject);
        if (members.Count > 1)
        {
            ennemis.Remove(members[1].gameObject);
            ennemis.Remove(members[2].gameObject);
        }
        
    }
	
}
