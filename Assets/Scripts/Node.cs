using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    public Vector2 Position;
    public float Priority;

    public Node(int x, int y)
    {
        Position = new Vector2(x, y);
    }

    public int CompareTo(Node other)
    {
        if (this.Priority < other.Priority) return -1;
        else if (this.Priority > other.Priority) return 1;
        else return 0;
    }
}
