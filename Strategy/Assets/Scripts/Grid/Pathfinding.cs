using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    [HideInInspector] public Transform production;
    [HideInInspector] public Transform target;
    public static Pathfinding Instance;
    private Grid _grid;

    void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void FollowThePath()
    {
        StartCoroutine(FollowThePathEnum());
    }

    IEnumerator FollowThePathEnum()
    {
        var targetTransform = target;
        var productionPos = production;
        foreach (var currentNote in FindPath(productionPos.position,
                     new Vector3(targetTransform.position.x + 1, targetTransform.position.y,
                         targetTransform.position.z)))
        {
            var currentPos = currentNote.worldPosition;
            currentPos.z = -1;
            currentPos.x -= -.5f;
            currentPos.y -= -.5f;

            for (int i = 0; i < 10; i++)
            {
                productionPos.position = Vector3.Lerp(productionPos.position, currentPos, 1f / 10f * i);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }
    }

    List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
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
                    if (openSet[i].hCost < node.hCost)
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

        return pathNodes;
    }

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

        _grid.Path = path;
        return path;
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