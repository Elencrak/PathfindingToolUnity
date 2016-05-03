using UnityEngine;
using System.Collections;

public class Poulpe2 : MonoBehaviour
{
    private Vector3 begin;
    private GameObject leader;
	// Use this for initialization
	void Start ()
    {
        begin = transform.position;
        leader = transform.parent.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GetComponent<NavMeshAgent>().Warp(begin);
        }
    }
}
