using UnityEngine;

public class MonsterMeleeAttack: MonoBehaviour
{
    [SerializeField] private float damage = 20;
    [SerializeField] private Animator animator;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (GameController.IsGameOver) return;
        if (col.gameObject.CompareTag(tag)) return;
        var target = col.collider.GetComponent<PlayerController>();
        
        if (target == null) return;
        target.TakeDamage(damage);
        animator.SetTrigger("Attack");
        transform.position += Vector3.up;
    }
    
}
