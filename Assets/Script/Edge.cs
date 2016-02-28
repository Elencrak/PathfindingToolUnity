using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("Edge")]
public class Edge {

    [XmlAttribute("firstNode")]
    public Node firstNode;
    [XmlAttribute("secondNode")]
    public Node secondNode;

    public Edge()
    {
        firstNode = null;
        secondNode = null;
    }

    public Edge(Node n1, Node n2)
    {
        firstNode = n1;
        secondNode = n2;
    }

    public void Serialize(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Edge));
        FileStream stream = new FileStream(path, FileMode.Append);
        serializer.Serialize(stream, this);
        stream.Close();
        Debug.Log("saved");
    }
}
