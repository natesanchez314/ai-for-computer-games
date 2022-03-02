using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    class PathHelper : IComparable<PathHelper>
    {
        public PathHelper prev;
        public PathNode node;
        public float cost;
        public float h;

        public PathHelper(PathNode node)
        {
            this.prev = null;
            this.node = node;
            this.cost = 0.0f;
        }

        public PathHelper(PathHelper prev, PathNode node)
        {
            this.prev = prev;
            this.node = node;
            this.cost = 0.0f;
        }

        public void SetCost(float cost)
        {
            this.cost = cost;
        }

        public void SetH(float h)
        {
            this.h = h;
        }

        public int CompareTo(PathHelper other)
        {
            if (this.cost < other.cost)
                return -1;
            else if (this.cost > other.cost)
                return 1;
            else
            {
                if (this.h < other.h)
                    return -1;
                else if (this.h > other.h)
                    return 1;
                else
                    return 0;
            }
        }

        public static int ComparePathHelpers(PathHelper a, PathHelper b)
        {
            return a.CompareTo(b);
        }
    }

    private static PathFinder instance;
    PathNode[] pathNodes;
    bool createdGraph;

    private PathFinder()
    {
        pathNodes = UnityEngine.Object.FindObjectsOfType<PathNode>();
        createdGraph = false;
    }

    public static List<PathNode> FindPath(PathNode start, PathNode goal)
    {
        if (instance == null)
            instance = new PathFinder();
        List<PathNode> path = new List<PathNode>(); // This is the path we return
        List<PathHelper> queue = new List<PathHelper>();
        HashSet<PathNode> visited = new HashSet<PathNode>();

        PathHelper curr = new PathHelper(start);
        visited.Add(start);
        while (curr.node != goal)
        {
            List<PathNode> neighbors = curr.node.neighbors;
            foreach (PathNode pathNode in neighbors)
            {
                if (!visited.Contains(pathNode))
                {
                    visited.Add(pathNode);
                    PathHelper ph = new PathHelper(curr, pathNode);
                    ph.SetCost(GetCost(curr.node, start, goal));
                    ph.SetH(GetH(curr.node, goal));
                    queue.Add(ph);
                }
            }
            queue.Sort(PathHelper.ComparePathHelpers);
            curr = queue[0];
            queue.RemoveAt(0);
           
        }

        Debug.Log("Found path!!!");

        // Add to our path
        while (curr.node != start)
        {
            path.Insert(0, curr.node);
            curr = curr.prev;
        }
        return path;
    }

    private static PathNode FindNextBestNode(PathNode start, PathNode currNode, PathNode lastNode, PathNode goal)
    {
        List<PathNode> neighbors = currNode.neighbors;
        if (neighbors.Count == 1)
            return neighbors[0];
        else
        {
            PathNode bestNode = null;
            float bestHeuristic = 0.0f;
            foreach (PathNode pathNode in neighbors)
            {
                Vector3 startPos = start.transform.position;
                Vector3 goalPos = goal.transform.position;
                Vector3 nodePos = pathNode.transform.position;
                float distToGoal = (nodePos - goalPos).magnitude;
                float distToStart = (nodePos - startPos).magnitude;
                float heuristic = distToStart - distToGoal;
                if (bestNode == null || heuristic > bestHeuristic)
                {
                    if (pathNode != lastNode)
                    {
                        bestNode = pathNode;
                        bestHeuristic = heuristic;
                    }
                }
            }
            return bestNode;
        }
    }

    private static float GetCost(PathNode node, PathNode startNode, PathNode endNode)
    {
        return GetG(node, startNode) + GetH(node, endNode);
    }

    private static float GetG(PathNode node, PathNode startNode)
    {
        float xDiff = node.transform.position.x - startNode.transform.position.x;
        float zDiff = node.transform.position.z - startNode.transform.position.z;
        return (xDiff * xDiff) + (zDiff * zDiff);
    }

    private static float GetH(PathNode node, PathNode endNode)
    {
        float xDiff = node.transform.position.x - endNode.transform.position.x;
        float zDiff = node.transform.position.z - endNode.transform.position.z;
        return (xDiff * xDiff) + (zDiff * zDiff);
    }

    public static PathNode FindClosestNodeToTarget(Vector3 target)
    {

        if (instance == null)
            instance = new PathFinder();
        PathNode closest = null;
        float closestDist = 0.0f;
        foreach (PathNode pathNode in instance.pathNodes)
        {
            Vector3 nodePos = pathNode.transform.position;
            Vector3 diff = nodePos - target;
            if (closest == null)
            {
                closest = pathNode;
                closestDist = diff.magnitude;
            }
            else
            {
                if (diff.magnitude < closestDist)
                {
                    closest = pathNode;
                    closestDist = diff.magnitude;
                }
            }
        }
        return closest;
    }

    public static void CreateGraph()
    {
        if (instance == null)
            instance = new PathFinder();
        if (!instance.createdGraph)
        {
            foreach (PathNode pathNode in instance.pathNodes)
                pathNode.FindNeighbors();
            instance.createdGraph = true;
        }
    }

    public static PathNode[] GetPathNodes()
    {
        if (instance == null)
            instance = new PathFinder();
        return instance.pathNodes;
    }
}

