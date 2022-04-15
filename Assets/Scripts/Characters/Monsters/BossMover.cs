using DG.Tweening;
using UnityEngine;

public class BossMover : MonoBehaviour, IPoolItem
{
    private const float WalkDuration = 3f;
    private readonly Vector3 _newPos = new Vector3(0, 4, 0);
    
    private Vector3 _startPos;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private void Awake()
    {
        _startPos = transform.position;
        _animator = GetComponent<Animator>();
    }

    private void Move()
    {
        transform.DOMove(_newPos, WalkDuration).SetEase(Ease.Linear).OnComplete( ()=> _animator.SetBool("IsWalking", false));
        _animator.SetBool(IsWalking, true);
    }

    private void OnDisable()
    {
        transform.position = _startPos;
        transform.DOKill();
    }
    public void ResetPoolItem()
    {
        Move();
    }
}
