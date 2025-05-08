using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        return obj is Position pos && pos.X == X && pos.Y == Y;
    }

    public override int GetHashCode()
    {
        return X * 73856093 ^ Y * 19349663;
    }
}
