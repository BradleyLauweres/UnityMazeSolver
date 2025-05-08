using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    public int Width;
    public int Height;
    public Cell[,] Cells;

    public Maze(int width, int height)
    {
        Width = width;
        Height = height;
        Cells = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cells[x, y] = new Cell(x, y);
            }
        }
    }
}

