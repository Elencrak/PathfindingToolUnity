using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamManagerWill : MonoBehaviour {
    public static TeamManagerWill instance;
    public List<Will_IA_M2> members;
    public List<GameObject> ennemis;
    public GameObject mainTarget;

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
        mainTarget = ennemis[0];
        InvokeRepeating("defineTarget", 0, 0.5f);
    }

    void defineTarget()
    {
        float smallestDist = dist(ennemis[0], members[0].gameObject);
        GameObject bestT = ennemis[0];
        foreach (GameObject target in ennemis)
        {
            foreach (Will_IA_M2 member in members)
            {
                float d = dist(member.gameObject, target);
                if (d < smallestDist)
                {
                    smallestDist = d;
                    bestT = target;
                }
            }
        }
        mainTarget = bestT;
    }

    float dist(GameObject obj1, GameObject obj2)
    {
        return Vector3.Distance(obj1.transform.position, obj2.transform.position);
    }

    public GameObject getTargetCanShoot(int id)
    {
        GameObject agent = members[id].gameObject;
        foreach (GameObject en in ennemis)
        {
            RaycastHit hit;
            Vector3 dir = en.transform.position - agent.transform.position;
            if (Physics.Raycast(agent.transform.position, dir, out hit))
            {
                if (hit.collider.gameObject == en)
                {
                    return en;
                }
            }
        }

        return null;
    }
	
}
