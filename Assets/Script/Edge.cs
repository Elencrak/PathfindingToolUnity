using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Edge : MonoBehaviour {

    public Node firstNode;
    public Node secondNode;
    public float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(firstNode.gameObject.transform.position, secondNode.gameObject.transform.position);
    }
}
