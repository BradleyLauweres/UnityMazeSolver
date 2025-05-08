using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfinder
{
    List<Position> FindPath(Maze maze, Position start, Position end);
}
