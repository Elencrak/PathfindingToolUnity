using UnityEngine;
using System.Collections;

public class BulletTestScript : MonoBehaviour {

	Vector3 pos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		pos = this.gameObject.transform.position + Vector3.forward * Time.deltaTime * 8;
		this.gameObject.transform.position = pos;
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Destroy (this.gameObject);
		}
	}
}
