using UnityEngine;
using System.Collections;

public class Poulpe3 : MonoBehaviour
{
    private Vector3 begin;
    // Use this for initialization
    void Start()
    {
        begin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetComponent<NavMeshAgent>().Warp(begin);
        }
    }
}