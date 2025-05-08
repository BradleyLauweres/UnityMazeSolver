using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int X;
    public int Y;
    public bool Visited = false;
    public bool[] Walls = { true, true, true, true };

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }
}

