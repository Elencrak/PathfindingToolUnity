using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

    float speed = 40f;
    public string launcherName;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 12);

        InvokeRepeating("Move", 0, .01f);
	}
	
    void Move()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
