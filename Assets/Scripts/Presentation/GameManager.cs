using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject wallPrefab;
    public GameObject pathMarkerPrefab;
    public GameObject startMarkerPrefab;
    public GameObject endMarkerPrefab;

    private MazeRenderer renderer;
    private SolverAnimator animator;
    private IMazeGenerator generator;
    private IPathfinder pathfinder;

    private List<GameObject> spawnedObjects = new();

    void Start()
    {
        generator = new RecursiveBacktrackerGenerator();
        pathfinder = new DijkstraPathfinder();

        renderer = GetComponent<MazeRenderer>();
        animator = GetComponent<SolverAnimator>();

        GenerateAndRenderMaze();
    }

    public void GenerateAndRenderMaze()
    {
        foreach (var obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();

        Maze maze = generator.Generate(width, height);

        renderer.wallPrefab = wallPrefab;
        renderer.Render(maze);

        animator.pathMarkerPrefab = pathMarkerPrefab;

        Position start = new Position(0, 0);
        Position end = new Position(width - 1, height - 1);

        spawnedObjects.Add(Instantiate(startMarkerPrefab, ToWorld(start), Quaternion.identity, transform));
        spawnedObjects.Add(Instantiate(endMarkerPrefab, ToWorld(end), Quaternion.identity, transform));

        var path = pathfinder.FindPath(maze, start, end);
        StartCoroutine(AnimateAndTrack(path));
    }


    private IEnumerator AnimateAndTrack(List<Position> path)
    {
        foreach (var pos in path)
        {
            Vector3 world = ToWorld(pos);
            GameObject marker = Instantiate(pathMarkerPrefab, world, Quaternion.identity, transform);
            spawnedObjects.Add(marker);
            yield return new WaitForSeconds(animator.delay);
        }
    }

    private Vector3 ToWorld(Position pos) => new Vector3(pos.X, 0.25f, pos.Y);
}

