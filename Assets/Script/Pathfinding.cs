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
        Debug.Log("saved");
    }
    public void Load(string name)
    {
        string path = Application.dataPath + "/Save/" + name + ".txt";
        XmlSerializer serializer = new XmlSerializer(typeof(Pathfinding));
        FileStream stream = new FileStream(path, FileMode.Open);
        copy(serializer.Deserialize(stream) as Pathfinding);
        stream.Close();
        Debug.Log("saved");
        
    }

    void copy(Pathfinding pathToCopy)
    {
        nodes = pathToCopy.nodes;
        edges = pathToCopy.edges;
    }
}
