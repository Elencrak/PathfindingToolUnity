using UnityEditor;
using UnityEngine;

public class PathfindingEditor : EditorWindow
{
    GameObject nodeRepresentation;
    GameObject edgeRepresentation;
    int nodeId = 1;
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

    // fonction qui permet l'affichage de la fenêtre du tool
    void OnGUI()
    {
        // Pour faire un Label
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        // Pour faire un champ de selection ( de GameObject ici)
        nodeRepresentation = (GameObject)EditorGUILayout.ObjectField("Node prefab", nodeRepresentation, typeof(GameObject), true);
        edgeRepresentation = (GameObject)EditorGUILayout.ObjectField("Edge prefab", edgeRepresentation, typeof(GameObject), true);
        // Pour faire une checkbox ( Boolean )
        addNode = EditorGUILayout.Toggle("AddNodeMode", addNode);
        // Pour faire un champ de texte
        pathfindingNameToSave = EditorGUILayout.TextField("Pathfinding save name : ", pathfindingNameToSave);
        // Pour faire des boutons
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

    // Fonction qui s'exécute comme le Awake() ou le Start()
    void OnEnable()
    {
        // Ajoute la fonction SceneGUI au fonction appelé chaque frame 
        SceneView.onSceneGUIDelegate += SceneGUI;
        // Load Resources pour l'éditor dans le dossier "Editor Default Resources"
        nodeRepresentation = EditorGUIUtility.Load("Prefabs/Node.prefab") as GameObject;
        edgeRepresentation = EditorGUIUtility.Load("Prefabs/Edge.prefab") as GameObject;
        
    }

    // Fonction qui sert d'update
    void SceneGUI(SceneView sceneView)
    {
        if(addNode)
        {
            // petite ligne pour modifier le control de l'éditeur en "Passif" (selectionne pas des objets au clic)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            
            // L'évent courant qui prend en compte les clics et les appuis de touche
            Event cur = Event.current;
            if (cur.type == EventType.MouseDown && cur.button == 0)
            {
                // Fait un raycast depuis la fenêtre scene
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
            // Remove a node
            if (cur.type == EventType.MouseDown && cur.button == 1)
            {
                // Fait un raycast depuis la fenêtre scene
                Ray ray = HandleUtility.GUIPointToWorldRay(cur.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Node")
                    {
                        RemoveNode(hit.transform.GetComponent<NodeRepresentation>().node);
                            

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
        PlayerPrefs.SetString("CurrentPath", pathName);
    }

    private void LoadPathfinding(string pathName)
    {
        PathfindingManager.GetInstance().currentPathfinding = new Pathfinding();
        PathfindingManager.GetInstance().currentPathfinding.Load(pathName);

        Reset();
        RebuildPath();
        PathfindingManager.GetInstance().currentPathfinding.setNeighbors();

    }

    private void Reset()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject obj in nodes)
        {
            // DestroyImmediate c'est comme Destroy dans les MonoBehavior
            DestroyImmediate(obj);
        }
        nodeId = 1;
        GameObject[] edges = GameObject.FindGameObjectsWithTag("Edge");
        foreach (GameObject obj in edges)
        {
            DestroyImmediate(obj);
        }
    }

    public void RemoveNode(Node node)
    {

        PathfindingManager.GetInstance().currentPathfinding.nodes.Remove(node);
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject obj in nodes)
        {
            // DestroyImmediate c'est comme Destroy dans les MonoBehavior
            if(obj.GetComponent<NodeRepresentation>().node == node)
            {
                DestroyImmediate(obj);
            }
        }
        GameObject[] edges = GameObject.FindGameObjectsWithTag("Edge");
        foreach (GameObject obj in edges)
        {
            Edge edge = obj.GetComponent<EdgeRepresentation>().edge;
            if(edge.firstNode == node)
            {
                PathfindingManager.GetInstance().currentPathfinding.edges.Remove(edge);
                DestroyImmediate(obj);
                continue;
            }
            if (edge.secondNode == node)
            {
                PathfindingManager.GetInstance().currentPathfinding.edges.Remove(edge);
                DestroyImmediate(obj);
            }
        }
    }

    private void RebuildPath()
    {
        Pathfinding current = PathfindingManager.GetInstance().currentPathfinding;
        foreach(Node node in current.nodes)
        {
            GameObject instance = Instantiate(nodeRepresentation, node.getPosition(), Quaternion.identity) as GameObject;
            instance.GetComponent<NodeRepresentation>().node = node;
            instance.name = "Node "+node.nodeId;
            instance.transform.parent = GameObject.Find("Nodes").transform;
            if (nodeId <= node.nodeId)
                nodeId = node.nodeId + 1;
        }
        foreach (Edge edge in current.edges)
        {
            GameObject instance = Instantiate(edgeRepresentation, edge.firstNode.getPosition(), Quaternion.identity) as GameObject;
            instance.transform.parent = GameObject.Find("Edges").transform;
            instance.GetComponent<EdgeRepresentation>().edge = edge;
        }
        // Permet de choisir soi-même le gameobject a sélectionner
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
                Edge currentEdge = new Edge(firstNode, secondNode);
                instance.GetComponent<EdgeRepresentation>().edge = currentEdge;
                PathfindingManager.GetInstance().currentPathfinding.edges.Add(currentEdge);
                Selection.activeGameObject = instance.transform.parent.parent.gameObject;

                if (!firstNode.neighborsNode.Contains(secondNode))
                {
                    firstNode.neighborsNode.Add(secondNode);
                }
                if (!secondNode.neighborsNode.Contains(firstNode))
                {
                    secondNode.neighborsNode.Add(firstNode);
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
            GameObject instance = Instantiate(nodeRepresentation, hit.point+Vector3.up*0.5f, Quaternion.identity) as GameObject;
            Node newNode = new Node(hit.point, nodeId++);
            instance.GetComponent<NodeRepresentation>().node = newNode;
            instance.name = "Node " + newNode.nodeId;
            PathfindingManager.GetInstance().currentPathfinding.nodes.Add(newNode);
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