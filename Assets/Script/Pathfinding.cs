using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Pathfinding {

    
    public List<Node> nodes;
    public List<Edge> edges;
	// Use this for initialization
	
    public Pathfinding()
    {
        nodes = new List<Node>();
        edges = new List<Edge>();
    }
    public void Save(string name)
    {
        string path = Application.dataPath + "/Save/" + name + ".txt";

        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.CreateNew);
        stream.Close();
        foreach (Node node in nodes)
        {
            node.Serialize(path);
        }
        foreach (Edge edge in edges)
        {
            edge.Serialize(path);
        }
    }
}
