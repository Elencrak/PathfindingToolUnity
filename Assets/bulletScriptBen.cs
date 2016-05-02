using UnityEngine;
using System.Collections;

public class bulletScriptBen : MonoBehaviour {

    float Speed = 8;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Speed * Time.deltaTime;
	}
    void OnCollisionEnter(Collision col)
    {

    }
}
