using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PathfindingManager  {

    public static PathfindingManager instance = null;
    


    public Pathfinding currentPathfinding;

    public static PathfindingManager GetInstance()
    {
        if(instance == null)
        {
            instance = new PathfindingManager();
        }
        return instance;
    }

    



    
}
