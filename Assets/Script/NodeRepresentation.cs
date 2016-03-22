using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[ExecuteInEditMode]
public class NodeRepresentation : MonoBehaviour {

    public Node node;

    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
    }
}