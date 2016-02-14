using UnityEditor;
using UnityEngine;
public class Pathfinding : EditorWindow
{
    GameObject node;
    bool addNode = false;

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
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.R)
            {
                Reset();
            }
            if (Event.current.shift && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A)
            {
                AddNode();
            }
        }
    }

    private void Reset()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject obj in objs)
        {
            DestroyImmediate(obj);
        }
    }

    void AddNode()
    {
        Debug.Log("addNode");
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            GameObject instance = Instantiate(node, hit.point, Quaternion.identity) as GameObject;
            instance.transform.parent = GameObject.Find("Nodes").transform;
            Selection.activeGameObject = instance.transform.parent.gameObject;
        }

    }
}