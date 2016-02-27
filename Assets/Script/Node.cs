using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{

    // <FriendNode,EdgeToNode>
    public Hashtable friendsNodes = new Hashtable(10);
    public List<Node> listOfNode = new List<Node>(10);
    public float numberOfNodes = 0;
    // Use this for initialization
    void Start()
    {
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
    }

   
    public void ConnectTo(Node node, Edge edge)
    {
        if (!friendsNodes.ContainsKey(node) && node != this)
        {
            friendsNodes.Add(node, edge);
            if (!listOfNode.Contains(node))
                listOfNode.Add(node);
            numberOfNodes++;
        }
        if (!node.friendsNodes.ContainsKey(this))
        {
            node.friendsNodes.Add(this, edge);
            if (!node.listOfNode.Contains(this))
                node.listOfNode.Add(this);
            node.numberOfNodes++;
        }
    }

}