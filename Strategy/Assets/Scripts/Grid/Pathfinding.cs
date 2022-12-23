using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public Transform seeker, target;
    public static Pathfinding instance;
    Grid grid;

    private void OnEnable()
    {
        if (instance ==null)
        {
            instance = this;
        }
    }

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    public void GoThePosition()
    {
        StartCoroutine(GoThePositionenum());
    }

    IEnumerator GoThePositionenum()
    {
        var targetPos = target;
        var seekerPos = seeker;
        foreach (var VARIABLE in FindPath(seekerPos.position,new Vector3( targetPos.position.x+1, targetPos.position.y ,targetPos.position.z)))
        {
            var obj = VARIABLE.worldPosition;
            obj.z = -1;
            obj.x -= -.5f;
            obj.y -= -.5f;

            for (int i = 0; i < 10; i++)
            {
                seekerPos.position = Vector3.Lerp(seekerPos.position, obj, 1f / 10f * i);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }
    }

    List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        List<Node> Nodes = new List<Node>();

        
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);
            if (node == targetNode)
            {
                Nodes = RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return Nodes;

    }

    private List<Node> Nodes;
    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
        return path;
        Nodes = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}