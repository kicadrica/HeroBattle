using System.Collections;
using Pathfinding;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class MonsterPathController : MonoBehaviour, ICharacterComponent
{
    private const float NextWayPointDistance = 0.3f;
    private const float UpdateRate = 2f;
    private const float BorderOffset = 0.5f;
    
    [SerializeField] private float Speed = 70f;
    [SerializeField] private Vector3 Offset = Vector3.zero;
    
    private Path _path;
    private bool _pathIsEnded;

    private Seeker _seeker;
    private Rigidbody2D _rb;
    private int _currentWayPoint;
    private Transform _transform;
    private bool _isCharacterDead;
    
    private void OnEnable()
    {
        _isCharacterDead = false;
        _seeker = GetComponent<Seeker>();
        StartCoroutine(UpdatePath()); 
    }
    private void Start()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isCharacterDead) return;
        if (_path == null) return;
        if(_currentWayPoint >= _path.vectorPath.Count)
        {
            if (_pathIsEnded) return;
            _pathIsEnded = true;
            return;
        }

        _pathIsEnded = false;

        Vector2 dir = (_path.vectorPath[_currentWayPoint] - _transform.position).normalized;
        dir *= Speed * Time.fixedDeltaTime;
        _rb.velocity = dir;

        var dist = Vector3.Distance(_transform.position, _path.vectorPath[_currentWayPoint]);
        if (dist < NextWayPointDistance)
        {
            _currentWayPoint++;
        }
    }

    private IEnumerator UpdatePath()
    {
        if (PlayerController.PlayerTransform == null) yield break;

        var newPos = PlayerController.PlayerTransform.position + Offset;
        if (newPos.y > ScreenBorders.MaxY - BorderOffset) {
            newPos.y = ScreenBorders.MaxY - BorderOffset;
        }
        _seeker.StartPath(transform.position, newPos, OnPathComplete);

        yield return new WaitForSeconds(1f / UpdateRate);

        StartCoroutine(UpdatePath());
    }

    private void OnPathComplete(Path p)
    {
        if (p.error) return;
        _path = p;
        _currentWayPoint = 0;
    }
    public void TurnOff()
    {
        _isCharacterDead = true;
        if (!(_rb is null)) _rb.velocity = Vector2.zero;
        StopAllCoroutines();
    }
}
