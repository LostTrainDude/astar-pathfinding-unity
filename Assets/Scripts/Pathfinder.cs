using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // GridGraph's dimensions
    public int GraphWidth;
    public int GraphHeight;

    // The position of Start and Goal nodes
    public Vector2 StartNodePosition;
    public Vector2 GoalNodePosition;

    // Lists of Walls (blocking) and Forests' (hurdles) positions
    public List<Vector2> Walls;
    public List<Vector2> Forests;

    // When Pathfinder GameObject is selected show the Gizmos
    private void OnDrawGizmosSelected()
    {
        // Initialize a new GridGraph of a given width and height
        GridGraph map = new GridGraph(GraphWidth, GraphHeight);
        
        // Define the List of Vector2 to be considered walls
        map.Walls = Walls;

        // Define the List of Vector2 to be considered forests
        map.Forests = Forests;

        int x1 = (int)StartNodePosition.x;
        int y1 = (int)StartNodePosition.y;
        int x2 = (int)GoalNodePosition.x;
        int y2 = (int)GoalNodePosition.y;

        // Find the path from StartNodePosition to GoalNodePosition
        List<Node> path = AStar.Search(map, map.Grid[x1, y1], map.Grid[x2, y2]);

        // Draw a Sphere on the Editor window for each Node of the Graph
        for (int y = 0; y < GraphHeight; y++)
        {
            for (int x = 0; x < GraphWidth; x++)
            {
                Gizmos.DrawSphere(new Vector2(x, y), 0.2f);
            }
        }

        // The Start node is BLUE
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(StartNodePosition, 0.2f);

        // The walls are BLACK
        Gizmos.color = Color.black;
        foreach (Vector2 wall in Walls)
        {
            Gizmos.DrawSphere(wall, 0.2f);
        }

        // The forests are GREEN
        Gizmos.color = Color.green;
        foreach (Vector2 forest in Forests)
        {
            Gizmos.DrawSphere(forest, 0.2f);
        }

        foreach (Node n in path)
        {
            // The Goal node is RED
            if (n.Position == GoalNodePosition)
            {
                Gizmos.color = Color.red;
            }
            // Every other node in the path is YELLOW
            else
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawSphere(n.Position, 0.2f);
        }
    }
}
