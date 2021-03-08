using UnityEngine;

public class PathWay : MonoBehaviour
{
    [SerializeField] private Transform[] _pathPoints;

    public Vector3[] PathPointsVector3()
    {
        Vector3[] pathPointsVectors = new Vector3[_pathPoints.Length];

        for (int i = 0; i < _pathPoints.Length; i++)
        {
            pathPointsVectors[i] = _pathPoints[i].position;
        }

        return pathPointsVectors;
    }
}
