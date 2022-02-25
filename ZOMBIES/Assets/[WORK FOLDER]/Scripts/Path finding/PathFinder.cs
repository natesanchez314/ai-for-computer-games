using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private static PathFinder instance;
    PathNode[] pathNodes;
    bool createdGraph;

    private PathFinder()
    {
        pathNodes = Object.FindObjectsOfType<PathNode>();
        createdGraph = false;
    }

    public static List<PathNode> FindPath(PathNode start, PathNode goal)
    {
        if (instance == null)
            instance = new PathFinder();
        List<PathNode> path = new List<PathNode>();
        path.Add(start);
        PathNode currNode = start;
        PathNode lastNode = null;
        while (currNode != goal)
        {
            currNode = FindNextBestNode(start, currNode, lastNode, goal);
            path.Add(currNode);
        }
        path.Add(goal);
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

