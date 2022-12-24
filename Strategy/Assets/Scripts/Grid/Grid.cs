using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public List<Node> Path;
    public TileManager tileprefab;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    float _nodeDiameter;
    int _gridSizeX, _gridSizeY;
    Node[,] _grid;

    void Awake()
    {
        _nodeDiameter = nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];
        CheckWalkable();
        var isOffset = true;

        foreach (Node n in _grid)
        {
            var spawnedTile = Instantiate(tileprefab, n.worldPosition, Quaternion.identity);
            spawnedTile.name = $"Tile {n.worldPosition.x} {n.worldPosition.y}";
            spawnedTile.transform.parent = transform;
            spawnedTile.transform.localScale = Vector3.one * (_nodeDiameter);
            isOffset = !isOffset;
            spawnedTile.Init(isOffset);
        }
    }

    void CheckWalkable()
    {
        Vector3 worldBottomLeft =
            transform.position - Vector3.left * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.left * (x * _nodeDiameter + nodeRadius) +
                                     Vector3.up * (y * _nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                _grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                {
                    neighbours.Add(_grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        CheckWalkable();
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = _gridSizeX - Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
        return _grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));

        if (_grid != null)
        {
            foreach (Node n in _grid)
            {
                if (!n.walkable)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (_nodeDiameter - .1f));
                }

                if (Path != null)
                {
                    if (Path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(n.worldPosition, Vector3.one * (_nodeDiameter - .1f));
                    }
                }
            }
        }
    }
}