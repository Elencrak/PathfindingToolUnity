using UnityEngine;
using System.Collections;

public class rulesCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            rules.getInstance().score(gameObject, collision.gameObject);
        }
    }
}
