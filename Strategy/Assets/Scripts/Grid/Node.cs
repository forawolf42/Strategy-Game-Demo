using UnityEngine;

public class Node
{
    public readonly bool walkable;
    public Vector3 WorldPosition;
    public readonly int gridX;
    public readonly int gridY;

    public int GCost;
    public int HCost;
    public Node Parent;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        WorldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get { return GCost + HCost; }
    }
}