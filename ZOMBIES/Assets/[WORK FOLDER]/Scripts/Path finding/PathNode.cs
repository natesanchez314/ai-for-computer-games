using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    float MAX_DIST = 15.0f;
    public List<PathNode> neighbors;

    public Vector3 GetPosition()
    {
        return this.gameObject.transform.position;
    }

    public void FindNeighbors()
    {
        PathNode[] allPathNodes = PathFinder.GetPathNodes();
        if (allPathNodes.Length > 0)
        {
            Vector3 thisPos = this.GetPosition();
            Vector3 otherPos;
            RaycastHit hit;
            LayerMask layerMask = 1 << 14;
            foreach (PathNode pathNode in allPathNodes)
            {
                otherPos = pathNode.GetPosition();
                float xDiff = thisPos.x - otherPos.x;
                float zDiff = thisPos.z - otherPos.z;
                float distSquared = (xDiff * xDiff) + (zDiff * zDiff);
                if (distSquared < MAX_DIST * MAX_DIST)
                {
                    Vector3 dir = otherPos - thisPos;
                    if (Physics.Raycast(thisPos, dir, out hit, MAX_DIST, layerMask))
                    {
                        if (otherPos == hit.transform.position)
                            neighbors.Add(pathNode);
                    }
                }
            }
        }
    }
}
