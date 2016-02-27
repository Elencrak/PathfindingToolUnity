using UnityEditor;
using UnityEngine;
public class Pathfinding : EditorWindow
{
    GameObject node;
    GameObject edge;
    bool addNode = false;
    bool createEdge = false;
    GameObject FirstNodeOfEdge;
    // Add menu item named "My Window" to the Window menu
    [MenuItem("AI/PathfindingEditor")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(Pathfinding));
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        node = (GameObject)EditorGUILayout.ObjectField("Node prefab", node, typeof(GameObject));
        edge = (GameObject)EditorGUILayout.ObjectField("Edge prefab", edge, typeof(GameObject));
        addNode = EditorGUILayout.Toggle("AddNodeMode", addNode);
    }
    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
    }
    void SceneGUI(SceneView sceneView)
    {
        if(addNode)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            Event cur = Event.current;
            if (cur.type == EventType.MouseDown && cur.button == 0)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(cur.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.tag == "Node")
                    {
                        createEdge = true;
                        FirstNodeOfEdge = hit.transform.gameObject;
                    }
                }
            }
            if (cur.type == EventType.MouseUp && cur.button == 0)
            {
            
                if (createEdge)
                {
                    Ray ray = HandleUtility.GUIPointToWorldRay(cur.mousePosition);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit))
                    {
                        // if it hit a node
                        if (hit.transform.tag == "Node")
                        {
                            // if this node is not the same than the first
                            if(hit.transform.gameObject != FirstNodeOfEdge)
                            {
                                AddEdge(FirstNodeOfEdge, hit.transform.gameObject);
                            }
                        }
                    }
                }
                else
                {
                    AddNode();
                }
                ResetEdgeCreation();

            }
            if (cur.type == EventType.KeyDown && cur.keyCode == KeyCode.R)
            {
                Reset();
            }
        }
    }

    private void ResetEdgeCreation()
    {
        Debug.Log("Reset edge creation");
        FirstNodeOfEdge = null;
        createEdge = false;
    }

    private void AddEdge(GameObject FirstNode, GameObject SecondNode)
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(FirstNode.transform.position, (SecondNode.transform.position - FirstNode.transform.position), out hit))
        {
            if(hit.transform.tag == "Node")
            {
                GameObject instance = Instantiate(edge, FirstNode.transform.position, Quaternion.identity) as GameObject;
                instance.transform.parent = GameObject.Find("Edges").transform;
                Edge currentEdge = instance.GetComponent<Edge>();
                currentEdge.firstNode = FirstNode.GetComponent<Node>();
                currentEdge.secondNode = SecondNode.GetComponent<Node>();
                currentEdge.distance = Vector3.Distance(FirstNode.transform.position, SecondNode.transform.position);
                Node first = FirstNode.GetComponent<Node>();
                Node second = SecondNode.GetComponent<Node>();
                FirstNode.GetComponent<Node>().ConnectTo(second, currentEdge);
                SecondNode.GetComponent<Node>().ConnectTo(first, currentEdge);
                Selection.activeGameObject = instance.transform.parent.parent.gameObject;
            }
        }
        Debug.Log("Reset edge creation");
        createEdge = false;
        FirstNodeOfEdge = null;
    }

    private void Reset()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject obj in nodes)
        {
            DestroyImmediate(obj);
        } 
        GameObject[] edges = GameObject.FindGameObjectsWithTag("Edge");
        foreach (GameObject obj in edges)
        {
            DestroyImmediate(obj);
        }
    }
    void AddNode()
    {
        Debug.Log("addNode");
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            GameObject instance = Instantiate(node, hit.point, Quaternion.identity) as GameObject;
            instance.transform.parent = GameObject.Find("Nodes").transform;
            Selection.activeGameObject = instance.transform.parent.parent.gameObject;
        }

    }

}