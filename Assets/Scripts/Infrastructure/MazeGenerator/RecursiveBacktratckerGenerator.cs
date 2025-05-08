using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBacktrackerGenerator : IMazeGenerator
{
    private Maze maze;
    private Stack<Cell> stack = new Stack<Cell>();
    private System.Random rand = new System.Random();

    public Maze Generate(int width, int height)
    {
        maze = new Maze(width, height);
        stack.Clear();

        Cell start = maze.Cells[0, 0];
        start.Visited = true;
        stack.Push(start);

        while (stack.Count > 0)
        {
            var current = stack.Peek();
            var next = GetUnvisitedNeighbor(current);

            if (next != null)
            {
                next.Visited = true;
                RemoveWall(current, next);
                stack.Push(next);
            }
            else
            {
                stack.Pop();
            }
        }

        return maze;
    }

    private Cell GetUnvisitedNeighbor(Cell cell)
    {
        var neighbors = new List<Cell>();

        int x = cell.X;
        int y = cell.Y;

        if (x > 0 && !maze.Cells[x - 1, y].Visited) neighbors.Add(maze.Cells[x - 1, y]);
        if (y > 0 && !maze.Cells[x, y - 1].Visited) neighbors.Add(maze.Cells[x, y - 1]);
        if (x < maze.Width - 1 && !maze.Cells[x + 1, y].Visited) neighbors.Add(maze.Cells[x + 1, y]);
        if (y < maze.Height - 1 && !maze.Cells[x, y + 1].Visited) neighbors.Add(maze.Cells[x, y + 1]);

        if (neighbors.Count == 0) return null;

        return neighbors[rand.Next(neighbors.Count)];
    }

    private void RemoveWall(Cell current, Cell next)
    {
        int dx = next.X - current.X;
        int dy = next.Y - current.Y;

        if (dx == 1)
        {
            current.Walls[1] = false;
            next.Walls[3] = false;
        }
        else if (dx == -1)
        { 
            current.Walls[3] = false;
            next.Walls[1] = false;
        }
        else if (dy == 1)
        { 
            current.Walls[0] = false;
            next.Walls[2] = false;
        }
        else if (dy == -1)
        { 
            current.Walls[2] = false;
            next.Walls[0] = false;
        }
    }
}

