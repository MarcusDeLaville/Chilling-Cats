using UnityEngine;
using DG.Tweening;
public class CreateWay : MonoBehaviour
{
    [SerializeField] private Transform[] _path;
    [SerializeField] private Transform _pathObject;
    [SerializeField] private PathType _pathType;
    [SerializeField] private float _speed;
    private Tween _pathMoving;
    
    [ContextMenu("CreateWay")]
    public void CreatePath()
    {
        Vector3[] path = new Vector3[5];
        
        for (int i = 0; i < 5; i++)
        {
            path[i] = _path[i].position;
        }
        
        _pathMoving = _pathObject.DOLocalPath(path, _speed, _pathType).SetOptions(true);
        _pathMoving.SetEase(Ease.Linear).SetLoops(-1);
    }
}
