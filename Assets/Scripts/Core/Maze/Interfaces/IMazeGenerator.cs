using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMazeGenerator
{
    Maze Generate(int width, int height);
}
