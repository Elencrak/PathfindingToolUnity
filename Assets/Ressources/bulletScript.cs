using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

    float speed = 80f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 12);

        
	}
	
    void FixedUpdate()
    {
        transform.position += (transform.forward*speed*Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
