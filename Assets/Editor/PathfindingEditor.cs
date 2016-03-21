using UnityEditor;
using UnityEngine;
public class PathfindingEditor : EditorWindow
{
    GameObject nodeRepresentation;
    GameObject edgeRepresentation;
    bool addNode = false;
    bool createEdge = false;
    GameObject FirstNodeOfEdge;
    string pathfindingNameToSave;
    string pathfindingNameToLoad;
    // Add menu item named "My Window" to the Window menu
    [MenuItem("AI/PathfindingEditor")]
    public static void ShowWindow()
    {
        //Show existing window GetInstance(). If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(PathfindingEditor));
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        nodeRepresentation = (GameObject)EditorGUILayout.ObjectField("Node prefab", nodeRepresentation, typeof(GameObject), true);
        edgeRepresentation = (GameObject)EditorGUILayout.ObjectField("Edge prefab", edgeRepresentation, typeof(GameObject), true);
        addNode = EditorGUILayout.Toggle("AddNodeMode", addNode);
        pathfindingNameToSave = EditorGUILayout.TextField("Pathfinding save name : ", pathfindingNameToSave);
        if (GUILayout.Button("Save pathfinding"))
        {
            SavePathfinding(pathfindingNameToSave);
        }
        if (GUILayout.Button("New Pathfinding"))
        {
            newPath();
        }
        pathfindingNameToLoad = EditorGUILayout.TextField("Pathfinding load name : ", pathfindingNameToLoad);
        if (GUILayout.Button("Load Pathfinding"))
        {
            LoadPathfinding(pathfindingNameToLoad);
        }
    }
    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
        nodeRepresentation = EditorGUIUtility.Load("Prefabs/Node.prefab") as GameObject;
        edgeRepresentation = EditorGUIUtility.Load("Prefabs/Edge.prefab") as GameObject;
        
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
        FirstNodeOfEdge = null;
        createEdge = false;
    }

    private void SavePathfinding(string pathName)
    {
        PathfindingManager.GetInstance().currentPathfinding.Save(pathName);
    }

    private void LoadPathfinding(string pathName)
    {
        PathfindingManager.GetInstance().currentPathfinding = new Pathfinding();
        PathfindingManager.GetInstance().currentPathfinding.Load(pathName);

        Reset();
        RebuildPath();

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

    private void RebuildPath()
    {
        Pathfinding current = PathfindingManager.GetInstance().currentPathfinding;
        foreach(Node node in current.nodes)
        {
            GameObject instance = Instantiate(nodeRepresentation, node.getPosition(), Quaternion.identity) as GameObject;
            instance.GetComponent<NodeRepresentation>().node = node;
            instance.transform.parent = GameObject.Find("Nodes").transform;
        }
        foreach (Edge edge in current.edges)
        {
            GameObject instance = Instantiate(edgeRepresentation, edge.firstNode.getPosition(), Quaternion.identity) as GameObject;
            instance.transform.parent = GameObject.Find("Edges").transform;
            instance.GetComponent<EdgeRepresentation>().firstNode = edge.firstNode;
            instance.GetComponent<EdgeRepresentation>().secondNode = edge.secondNode;
        }
        Selection.activeGameObject = GameObject.Find("Nodes").transform.parent.gameObject;
    }


    private void AddEdge(GameObject FirstNode, GameObject SecondNode)
    {
        if (PathfindingManager.GetInstance().currentPathfinding == null)
            PathfindingManager.GetInstance().currentPathfinding = new Pathfinding();
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(FirstNode.transform.position, (SecondNode.transform.position - FirstNode.transform.position), out hit))
        {
            if(hit.transform.tag == "Node")
            {
                GameObject instance = Instantiate(edgeRepresentation, FirstNode.transform.position, Quaternion.identity) as GameObject;
                instance.transform.parent = GameObject.Find("Edges").transform;
                Node firstNode = FirstNode.GetComponent<NodeRepresentation>().node;
                Node secondNode = SecondNode.GetComponent<NodeRepresentation>().node;
                instance.GetComponent<EdgeRepresentation>().firstNode = firstNode;
                instance.GetComponent<EdgeRepresentation>().secondNode = secondNode;
                Edge currentEdge = new Edge(firstNode, secondNode);
                PathfindingManager.GetInstance().currentPathfinding.edges.Add(currentEdge);
                Selection.activeGameObject = instance.transform.parent.parent.gameObject;

                if (!firstNode.neighborsNode.Contains(secondNode))
                {
                    firstNode.neighborsNode.Add(secondNode);
                    Debug.Log(firstNode.neighborsNode.Count);
                }
                if (!secondNode.neighborsNode.Contains(firstNode))
                {
                    secondNode.neighborsNode.Add(firstNode);
                    Debug.Log(secondNode.neighborsNode.Count);
                }
            }
        }
        createEdge = false;
        FirstNodeOfEdge = null;
    }

    void AddNode()
    {
        if (PathfindingManager.GetInstance().currentPathfinding == null)
            PathfindingManager.GetInstance().currentPathfinding = new Pathfinding();
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            GameObject instance = Instantiate(nodeRepresentation, hit.point, Quaternion.identity) as GameObject;
            instance.GetComponent<NodeRepresentation>().node = new Node(hit.point,PathfindingManager.GetInstance().currentPathfinding.nodes.Count);
            PathfindingManager.GetInstance().currentPathfinding.nodes.Add(instance.GetComponent<NodeRepresentation>().node);
            instance.transform.parent = GameObject.Find("Nodes").transform;
            Selection.activeGameObject = instance.transform.parent.parent.gameObject;
        }

    }

    void newPath()
    {
        Reset();
        PathfindingManager.GetInstance().currentPathfinding = new Pathfinding();
    }


}