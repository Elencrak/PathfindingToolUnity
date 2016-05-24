using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour {

    void Update()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, 20.0f);
        foreach(Collider col in c)
        {
            if (col != null && col.tag == "Bullet" && col.GetComponent<bulletScript>() != null && col.GetComponent<bulletScript>().launcherName != transform.parent.GetComponent<TeamNumber>().teamName)
            {
                
                Vector3 v = col.transform.right;
                if (Random.Range(0f, 1f) <= 0.5f)
                    v = -Vector3.right;
                GetComponent<AgentAntoine>().Dodge(col.transform.position, v, col.transform.forward);
            }
        }
    }

	/*void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet" && other.GetComponent<bulletScript>().launcherName != transform.parent.transform.parent.GetComponent<TeamNumber>().teamName)
        {
            Vector3 v = Vector3.right;
            if (Random.Range(0f, 1f) <= 0.5f)
                v = -Vector3.right;
            transform.parent.GetComponent<AgentAntoine>().Dodge(other.transform.position, v);
        }
    }*/
}
