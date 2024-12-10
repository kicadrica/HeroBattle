using UnityEngine;

public class AbstractCharacter : MonoBehaviour, IDamagable
{
    public Health Health;
    
    [SerializeField] private float baseHealth = 100;
    protected Animator Animator;
    
    protected bool IsDead;
    
    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Health = new Health(baseHealth);
    }
    
    public virtual void TakeDamage(float damageAmount)
    {
        if (IsDead) return;
        
        Health.Damage(damageAmount);

        if (Health.Current <= 0) {
            Die();
        }
    }

    protected virtual void Die()
    {
        IsDead = true;
        TurnOffAllComponents();
    }

    protected void TurnOffAllComponents()
    {
        foreach (var component in GetComponentsInChildren<ICharacterComponent>()) {
            component.TurnOff();
        }
    }
}
