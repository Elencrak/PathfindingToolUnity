using UnityEngine;
using System.Collections;

public class reviveWill : MonoBehaviour {
    Vector3 start;
	// Use this for initialization
	void Start () {
        start = transform.position;

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Bullet")
        {
            transform.position = start;
        }
    }
}
