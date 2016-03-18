using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("Node")]
public class Node
{
    [XmlAttribute("positionX")]
    public float positionX;
    [XmlAttribute("positionY")]
    public float positionY;
    [XmlAttribute("positionZ")]
    public float positionZ;
    [XmlAttribute("nodeId")]
    public int nodeId;
    public float distance = 9999;
    public float heurystic = 9999;
    public Node previousNode = null;
    public float cumule = 0;
    // Use this for initialization

    public Node()
    {
        positionX = 0;
        positionY = 0;
        positionZ = 0;
    }

    public Node(Vector3 position,int Id)
    {
        positionX = position.x;
        positionY = position.y;
        positionZ = position.z;
        nodeId = Id;
    }

    public void Serialize(string path)
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Node));
        FileStream stream = new FileStream(path, FileMode.Append);
        serializer.Serialize(stream, this);
        stream.Close();
        Debug.Log("saved");
    }
    public void Deserialize(string path)
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Node));
        FileStream stream = new FileStream(path, FileMode.Open);
        copy(serializer.Deserialize(stream) as Node);
        stream.Close();
        Debug.Log("saved");
    }

    void copy(Node nodeToCopy)
    {
        positionX = nodeToCopy.positionX;
        positionY = nodeToCopy.positionY;
        positionZ = nodeToCopy.positionZ;
        nodeId = nodeToCopy.nodeId;
    }

    public Vector3 getPosition()
    {
        return new Vector3(positionX, positionY, positionZ);
    }

}