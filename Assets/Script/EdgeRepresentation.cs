using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class EdgeRepresentation : MonoBehaviour
{

    public Node firstNode;
    public Node secondNode;

   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(firstNode.getPosition(), secondNode.getPosition());
    }
}
