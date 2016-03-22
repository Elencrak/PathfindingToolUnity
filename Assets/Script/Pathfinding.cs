using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

[XmlRoot("Pathfinding")]
public class Pathfinding {

    [XmlArray("Nodes")]
    [XmlArrayItem("Node")]
    public List<Node> nodes;
    [XmlArray("Edges")]
    [XmlArrayItem("Edge")]
    public List<Edge> edges;
	// Use this for initialization
	
    public Pathfinding()
    {
        
        nodes = new List<Node>();
        edges = new List<Edge>();
        PathfindingManager.GetInstance().currentPathfinding = this;
    }
    public void Save(string name)
    {
        string path = Application.dataPath + "/Save/" + name + ".txt";

        XmlSerializer serializer = new XmlSerializer(typeof(Pathfinding));
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        serializer.Serialize(stream, this);
        stream.Close();
        Debug.Log("saved at "+path);
    }
    public void Load(string name)
    {
        string path = Application.dataPath + "/Save/" + name + ".txt";
        XmlSerializer serializer = new XmlSerializer(typeof(Pathfinding));
        FileStream stream = new FileStream(path, FileMode.Open);
        copy(serializer.Deserialize(stream) as Pathfinding);
        stream.Close();
        SetNodesOnEdge();
        
    }

    void copy(Pathfinding pathToCopy)
    {
        nodes = pathToCopy.nodes;
        edges = pathToCopy.edges;
    }

    public void setNeighbors()
    {
        for(int i = 0;i<edges.Count;++i)
        {
            if (!edges[i].firstNode.neighborsNode.Contains(edges[i].secondNode))
                edges[i].firstNode.neighborsNode.Add(edges[i].secondNode);
            if (!edges[i].secondNode.neighborsNode.Contains(edges[i].firstNode))
                edges[i].secondNode.neighborsNode.Add(edges[i].firstNode);

        }

    }

    void SetNodesOnEdge()
    {
        foreach(Edge edge in edges)
        {
            edge.firstNode = nodes[edge.firstNodeId];
            edge.secondNode = nodes[edge.secondNodeId];
        }
    }
}
