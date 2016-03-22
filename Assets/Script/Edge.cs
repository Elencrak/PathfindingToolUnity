using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("Edge")]
public class Edge {
    [XmlIgnore]
    public Node firstNode;
    [XmlAttribute("firstNodeId")]
    public int firstNodeId;
    [XmlIgnore]
    public Node secondNode;
    [XmlAttribute("secondNodeId")]
    public int secondNodeId;

    public Edge()
    {
        firstNode = null;
        secondNode = null;
    }

    public Edge(Node n1, Node n2)
    {
        firstNode = n1;
        firstNodeId = n1.nodeId;
        secondNode = n2;
        secondNodeId = n2.nodeId;

    }

    public void Serialize(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Edge));
        FileStream stream = new FileStream(path, FileMode.Append);
        serializer.Serialize(stream, this);
        stream.Close();
    }

    public void Deserialize(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Edge));
        FileStream stream = new FileStream(path, FileMode.Open);
        copy(serializer.Deserialize(stream) as Edge);
        stream.Close();
        
    }

    void copy(Edge edgeToCopy)
    {
        firstNodeId = edgeToCopy.firstNodeId;
        secondNodeId = edgeToCopy.secondNodeId;
    }
}
