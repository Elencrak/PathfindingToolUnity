using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class PathfindingManager  {

    //Static members
    public static PathfindingManager instance = null;
    
    public static PathfindingManager GetInstance()
    {
        if(instance == null)
        {
            instance = new PathfindingManager();
        }
        return instance;
    }

    // protected Class comparer
    protected class DuplicateKeyComparer<TKey>: 
        IComparer<TKey> where TKey : IComparable
    {
        #region IComparer<TKey> Members
        public int Compare(TKey x,TKey y)
        {
            int result = x.CompareTo(y);
            if (result == 0)
                return 1;
            else
                return result;
        }
        #endregion
    }


    static private DuplicateKeyComparer<float> Comparer = new DuplicateKeyComparer<float>();
    public int test = 0;
    public Pathfinding currentPathfinding;


    public void ResetHeuristic(List<Node> nodesToReset)
    {
        foreach(Node node in nodesToReset)
        {
            node.heurystic = 9999f;
            node.distance = 9999f;
            node.cumule = 0f;
            node.previousNode = null;

        }
    }

    public List<Vector3> GetRoad(Vector3 startPosition, Vector3 destination, Pathfinding path)
    {
        List<Vector3> road = new List<Vector3>();
        Node startNode = FindNearNode(startPosition,path);
        startNode.distance = 0;
        Node endNode = FindNearNode(destination,path);

        List<Vector3> resultPath = FindPathFromNode(startNode, endNode);
        if (startPosition != startNode.getPosition())
            resultPath.Insert(0, startPosition);
        if (destination != endNode.getPosition())
            resultPath.Add(destination);
        return resultPath;
    }

    public List<Vector3> SmoothRoad(List<Vector3> road)
    {
        Vector3 up = new Vector3(0, 1f, 0);
        List<Vector3> r = new List<Vector3>(road.Count);
        IEnumerator<Vector3> it = road.GetEnumerator();
        if (!it.MoveNext())
            return road;
        Vector3 currentNode = it.Current;
        r.Add(currentNode);
        if (!it.MoveNext())
            return road;
        Vector3 formerNode = it.Current;
        int layerMask = 1 << 20;
        while (it.MoveNext())
        {
            if (Physics.Raycast(currentNode + up, it.Current - currentNode, Vector3.Distance(it.Current, currentNode), layerMask))
            {
                currentNode = formerNode;
                r.Add(formerNode);
            }
            formerNode = it.Current;
        }
        r.Add(it.Current);
        return r;
    }

    private Node FindNearNode(Vector3 position, Pathfinding path)
    {
        Node closestNode = null;
        float distMin = 9999f;
        foreach(Node node in path.nodes)
        {
            float dist = Vector3.Distance(position, node.getPosition());
            if (dist < distMin)
            {
                distMin = dist;
                closestNode = node;
            }
        }
        return closestNode;
    }

    private List<Vector3> FindPathFromNode(Node startNode, Node endNode)
    {
        List<Node> nodesToReset = new List<Node>();
        List<Node> checkNode = new List<Node>();

        SortedList<float, Node> tryNode = new SortedList<float, Node>(Comparer);

        float cumule = 0f;

        float currentDist;
        float currentHeurystic;

        Node currentNode = null;
        Node previousNode = startNode;
        List<Node> listNode;
        tryNode.Add(Vector3.Distance(startNode.getPosition(), endNode.getPosition()), startNode);
        while((currentNode != endNode)&& tryNode.Count > 0)
        {
            currentNode = tryNode.ElementAt(0).Value;
            tryNode.RemoveAt(0);
            listNode = currentNode.neighborsNode;
            foreach (Node node in listNode)
            {
                if(!checkNode.Contains(node))
                {
                    currentDist = Vector3.Distance(previousNode.getPosition(), node.getPosition()) + previousNode.distance;
                    currentHeurystic = Vector3.Distance(node.getPosition(), endNode.getPosition());

                    cumule = currentDist + currentHeurystic;
                    if(cumule < (node.heurystic+node.distance))
                    {
                        node.heurystic = currentHeurystic;
                        node.distance = currentDist;
                        node.cumule = currentDist + currentHeurystic;
                        node.previousNode = currentNode;

                        tryNode.Add(node.cumule, node);
                        nodesToReset.Add(node);
                    }
                }
            }
            checkNode.Add(currentNode);

        }
        if(currentNode == endNode)
        {
            List<Vector3> tmp = ReturnRoad(currentNode);
            ResetHeuristic(nodesToReset);
            return tmp;
        }
        else
        {
            return null;
        }
    }

    private List<Vector3> ReturnRoad(Node currentNode)
    {
        List<Vector3> tmpList = new List<Vector3>();
        do
        {
            tmpList.Insert(0, currentNode.getPosition());
            currentNode = currentNode.previousNode;
        }
        while (currentNode != null);
        return tmpList;
    }





}
