using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolverAnimator : MonoBehaviour
{
    public GameObject pathMarkerPrefab;
    public float delay = 0.05f;
    public float cellSize = 1f;

    public IEnumerator AnimatePath(List<Position> path)
    {
        foreach (var pos in path)
        {
            Vector3 worldPos = new Vector3(pos.X * cellSize, 0.25f, pos.Y * cellSize);
            Instantiate(pathMarkerPrefab, worldPos, Quaternion.identity, transform);
            yield return new WaitForSeconds(delay);
        }
    }
}

