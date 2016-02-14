using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{

    public Hashtable friendsNodes = new Hashtable(10);
    public List<GameObject> listOfNode = new List<GameObject>(10);
    public float numberOfNodes = 0;
    public CustomList list = new CustomList();
    // Use this for initialization
    void Start()
    {
        if (Application.isPlaying)
            return;
        RaycastHit hit;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject obj in objs)
        {
            if (gameObject == obj)
                continue;
            if (Physics.Raycast(transform.position, (obj.transform.position - transform.position), out hit))
            {
                if (hit.transform.tag == "Node")
                {
                    AddNode(obj, Vector3.Distance(obj.transform.position, transform.position));
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
        Gizmos.color = Color.blue;
        foreach (DictionaryEntry entry in friendsNodes)
        {
            GameObject obj = entry.Key as GameObject;
            Gizmos.DrawLine(transform.position, obj.transform.position);
        }
    }

    public void AddNode(GameObject n, float dist)
    {
        if (!friendsNodes.ContainsKey(n) && n != gameObject)
        {
            friendsNodes.Add(n, dist);
            if (!listOfNode.Contains(n))
                listOfNode.Add(n);
            numberOfNodes++;
        }
        Node node = n.GetComponent<Node>();
        if (!node.friendsNodes.ContainsKey(gameObject))
        {
            node.friendsNodes.Add(gameObject, dist);
            if (!node.listOfNode.Contains(gameObject))
                node.listOfNode.Add(gameObject);
            node.numberOfNodes++;
        }
    }

}