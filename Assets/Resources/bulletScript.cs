using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

    public float speed = 40f;
    public string launcherName;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 12);
	}
	
    void FixedUpdate()
    {
        transform.position = transform.position + transform.forward * Time.fixedDeltaTime * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
