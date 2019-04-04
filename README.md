# Simple grid-based A* Pathfinding in Unity
![](https://i.imgur.com/Fr0LxR2.gif)

A simple implementation of the **A\* Pathfinding algorithm** that makes use of **Priority Queues**, applied to a grid.

## Credits
This implementation integrates:
- [Amit Patel's studies on Pathfinding][tech1]
- The `SimplePriorityQueue` from [BlueRaja's High Speed PriorityQueue for C#][tech2].

I only imported what I needed for the `SimplePriorityQueue` to work, so please refer to BlueRaja's Git repository for additional options and documentation.

## How to test it like in the GIF above
In your Unity Scene:
- Create a new empty `GameObject`, rename it **Pathfinder** (for ease of use)
- Attach the `Pathfinder` Component to it
- Select it in the Hierarchy
- Start altering the `public` variables available in the Inspector.

## Implementation
### Introduction
As it is, the current implementation considers an 8-direction movement and two kinds of obstacles:
- Walls - nodes that **cannot** be crossed
- Forests - nodes that **"take longer"** to cross

Movement cost is `1`. However, if the node to be crossed is a Forest node, it costs `2`.

### Create a new GridGraph
To initialize a new `GridGraph` that has differently weighted nodes:
```cs
// Initialize a new GridGraph of a given width and height
GridGraph map = new GridGraph(10, 10);
```
This will fill the grid with `Node`s.

Now it's time to initialize the Lists of `Vector2` that will store positions of Walls and Forests on the `GridGraph`.
Of course you can do this using either `public` Lists of `Vector2` or `private` ones.

```cs
// Define the List of Vector2 to be considered walls
map.Walls = _walls;

// Define the List of Vector2 to be considered forests
map.Forests = _forests;
```

### Find a path
Then, to define a Start and a Goal you have to define a `Vector2` for each point, so to use it to return a List of `Node`s with the shortest path between them.
```cs
// The position of Start and Goal nodes
Vector2 StartNodePosition = new Vector2(0, 0);
Vector2 GoalNodePosition = new Vector2(9, 9);

int x1 = (int)StartNodePosition.x;
int y1 = (int)StartNodePosition.y;
int x2 = (int)GoalNodePosition.x;
int y2 = (int)GoalNodePosition.y;

// Find the path from StartNodePosition to GoalNodePosition
List<Node> path = AStar.Search(map, map.Grid[x1, y1], map.Grid[x2, y2]);
```
Parameters `map.Grid[x1, y1]` and `map.Grid[x2, y2]` refer to the `Node`s in the `GridGraph` that are located at `[x1, y1]` and `[x2, y2]`

Again, you can declare `StartNodePosition` and `GoalNodePosition` to be either `public` or `private`.

## Customization
### Modify movement costs
You can alter this value in the `Cost(Node b)` method contained in the `Graph.cs` script:

```cs
public int Cost(Node b)
{
    // If Node 'b' is a Forest return 2, otherwise 1
    if (Forests.Contains(b.Position)) return 2;
    else return 1;
}
```

### Add additional obstacles
To include a new obstacle you first have to declare a new `List<Vector2>`, at the top of the `Graph.cs` script, for each obstacle you have in mind

```cs
public List<Node> Forests;
public List<Node> Pits;
```

Then modify the `Cost(Node b)` method accordingly:

```cs
public int Cost(Node b)
{
    if (Forests.Contains(b.Position)) return 2; // If Node 'b' is a Forest return 2
    else if (Pits.Contains(b.Position)) return 3; // If Node 'b' is a Pit return 3
    else return 1; // Otherwise return 1
}
```

   [tech1]: <https://www.redblobgames.com/pathfinding/a-star/introduction.html>
   [tech2]: <https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp>
