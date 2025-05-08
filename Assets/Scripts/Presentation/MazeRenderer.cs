using UnityEngine;
using System.Collections.Generic;

public class MazeRenderer : MonoBehaviour
{
    public GameObject wallPrefab;
    public float cellSize = 1f;

    private List<GameObject> wallInstances = new();

    public void Render(Maze maze)
    {
        Clear();

        for (int x = 0; x < maze.Width; x++)
        {
            for (int y = 0; y < maze.Height; y++)
            {
                Cell cell = maze.Cells[x, y];
                Vector3 cellPos = new Vector3(x * cellSize, 0, y * cellSize);

                if (cell.Walls[0])
                    SpawnWall(cellPos + new Vector3(0, 0, cellSize / 2), Quaternion.identity);

                if (cell.Walls[1])
                    SpawnWall(cellPos + new Vector3(cellSize / 2, 0, 0), Quaternion.Euler(0, 90, 0));

                if (cell.Walls[2])
                    SpawnWall(cellPos + new Vector3(0, 0, -cellSize / 2), Quaternion.identity);

                if (cell.Walls[3])
                    SpawnWall(cellPos + new Vector3(-cellSize / 2, 0, 0), Quaternion.Euler(0, 90, 0));
            }
        }
    }

    private void SpawnWall(Vector3 pos, Quaternion rot)
    {
        GameObject wall = Instantiate(wallPrefab, pos, rot, transform);
        wallInstances.Add(wall);
    }

    public void Clear()
    {
        foreach (var wall in wallInstances)
        {
            Destroy(wall);
        }
        wallInstances.Clear();
    }
}
