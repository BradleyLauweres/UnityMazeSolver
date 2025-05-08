using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraPathfinder : IPathfinder
{
    public List<Position> FindPath(Maze maze, Position start, Position end)
    {
        int width = maze.Width;
        int height = maze.Height;

        var dist = new Dictionary<Position, int>();
        var prev = new Dictionary<Position, Position?>();
        var queue = new List<Position>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pos = new Position(x, y);
                dist[pos] = int.MaxValue;
                prev[pos] = null;
                queue.Add(pos);
            }
        }

        dist[start] = 0;

        while (queue.Count > 0)
        {
            queue.Sort((a, b) => dist[a].CompareTo(dist[b]));
            Position current = queue[0];
            queue.RemoveAt(0);

            if (current.Equals(end)) break;

            foreach (var neighbor in GetNeighbors(maze, current))
            {
                int alt = dist[current] + 1;
                if (alt < dist[neighbor])
                {
                    dist[neighbor] = alt;
                    prev[neighbor] = current;
                }
            }
        }

        var path = new List<Position>();
        Position? step = end;
        while (step != null && prev.ContainsKey(step.Value))
        {
            path.Add(step.Value);
            step = prev[step.Value];
        }

        path.Reverse();
        return path;
    }

    private List<Position> GetNeighbors(Maze maze, Position pos)
    {
        var neighbors = new List<Position>();
        var cell = maze.Cells[pos.X, pos.Y];

        if (!cell.Walls[0] && pos.Y < maze.Height - 1) neighbors.Add(new Position(pos.X, pos.Y + 1));
        if (!cell.Walls[1] && pos.X < maze.Width - 1) neighbors.Add(new Position(pos.X + 1, pos.Y)); 
        if (!cell.Walls[2] && pos.Y > 0) neighbors.Add(new Position(pos.X, pos.Y - 1));
        if (!cell.Walls[3] && pos.X > 0) neighbors.Add(new Position(pos.X - 1, pos.Y));

        return neighbors;
    }
}
