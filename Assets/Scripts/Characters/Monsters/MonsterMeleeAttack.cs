using UnityEngine;

public class MonsterMeleeAttack: MonoBehaviour
{
    [SerializeField] private float Damage = 20;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (GameController.IsGameOver) return;
        if (col.gameObject.CompareTag(tag)) return;
        var target = col.collider.GetComponent<PlayerController>();
        
        if (target == null) return;
        target.TakeDamage(Damage);
        _animator.SetTrigger("Attack");
        transform.position += Vector3.up;
    }
    
}
