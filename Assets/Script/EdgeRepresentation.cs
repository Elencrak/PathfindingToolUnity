using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class EdgeRepresentation : MonoBehaviour
{
    public Edge edge;

   
    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(edge.firstNode.getPosition(), edge.secondNode.getPosition());
    }
}
