using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    [HideInInspector] public Transform production;
    [HideInInspector] public Transform target;
    public static Pathfinding Instance;
    private Grid _grid;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Awake()
    {
        _grid = GetComponent<Grid>(); 
    }


    /// <summary>
    /// Returns the shortest path from the start position to the target position.
    /// </summary>
    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        List<Node> pathNodes = new List<Node>();

        Node startNode = _grid.NodeFromWorldPoint(startPos);
        Node targetNode = _grid.NodeFromWorldPoint(targetPos);

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
                    if (openSet[i].HCost < node.HCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);
            if (node == targetNode)
            {
                pathNodes = RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in _grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return pathNodes;
    }

    /// <summary>
    ///  Returns the list of nodes of the route, following the path between the given start and destination nodes.
    /// </summary>
    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        _grid.Path = path;
        return path;
    }


    /// <summary>
    ///   Returns the distance based on the nodeA and nodeB values of the two nodes.
    /// </summary>
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}