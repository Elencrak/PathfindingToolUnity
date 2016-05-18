using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DodgeRodrigue : MonoBehaviour {
    public List<GameObject> listOfBullets = new List<GameObject>();
    RodrigueAgent parent;
    // Use this for initialization
    void Start () {
        parent = GameObject.Find("RodrigueAgent").GetComponent<RodrigueAgent>();
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider parOther)
    {
        if (parOther.tag == "Bullet" && parOther.GetComponent<bulletScript>().launcherName != "RektByRodrigue")
        {
            listOfBullets.Add(parOther.gameObject);
            Vector3 bulletForward = parOther.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(parOther.transform.position, bulletForward, out hit, 100))
            {
               if(hit.transform.name != "RodrigueAgent")
                {
                    StartCoroutine(Dodge());
                }
            }
        }
    }

    IEnumerator Dodge()
    {
        parent.isDodging = true;
        parent.navMeshAgent.Stop();
        yield return new WaitForSeconds(.3f);
        parent.navMeshAgent.Resume();
        parent.isDodging = false;
    }

    void OnTriggerExit(Collider parOther)
    {
        if (parOther.tag == "Bullet" && parOther.GetComponent<bulletScript>().launcherName != "RektByRodrigue")
        {
            listOfBullets.Remove(parOther.gameObject);
        }
    }
}
