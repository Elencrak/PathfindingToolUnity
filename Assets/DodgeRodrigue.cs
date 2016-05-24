using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DodgeRodrigue : MonoBehaviour {
    public List<GameObject> listOfBullets = new List<GameObject>();
    public List<GameObject> listOfFriends = new List<GameObject>();
    RodrigueAgent parent;
    // Use this for initialization
    void Start () {
        parent = transform.parent.GetComponent<RodrigueAgent>();
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider parOther)
    {
        if(parOther.gameObject.transform.parent != null && parOther.gameObject.transform.parent.GetComponent<RodrigueAgent>() && parOther.gameObject.transform.parent.GetComponent<RodrigueAgent>().teamName == "RektByRodrigue")
        {
            listOfFriends.Add(parOther.gameObject.transform.parent.gameObject);
        }

        if (parOther.tag == "Bullet" && parOther.GetComponent<bulletScript>().launcherName != "RektByRodrigue")
        {
            listOfBullets.Add(parOther.gameObject);
            Vector3 bulletForward = parOther.transform.forward;            
            RaycastHit hit;
            if (Physics.Raycast(parOther.transform.position, bulletForward, out hit, 100))
            {
               if(hit.transform.name != transform.parent.name)
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
        yield return new WaitForSeconds(.5f);
        parent.navMeshAgent.Resume();
        parent.isDodging = false;
    }

    void OnTriggerExit(Collider parOther)
    {
        if (parOther.tag == "Bullet" && parOther.GetComponent<bulletScript>().launcherName != "RektByRodrigue")
        {
            listOfBullets.Remove(parOther.gameObject);
        }

        if (parOther.gameObject.transform.parent != null && parOther.gameObject.transform.parent.GetComponent<RodrigueAgent>() && parOther.gameObject.transform.parent.GetComponent<RodrigueAgent>().teamName == "RektByRodrigue")
        {
            listOfFriends.Remove(parOther.gameObject.transform.parent.gameObject);
        }
    }
}
