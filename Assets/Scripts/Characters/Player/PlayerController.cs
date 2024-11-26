using System;
using UnityEngine;

public class PlayerController : AbstractCharacter
{
    public static Transform PlayerTransform { get; private set; }
    public static event Action OnPlayerDead;
    
    [SerializeField] private GameObject playerAttributes;

    protected override void Awake()
    {
        base.Awake();
        PlayerTransform = transform;
        Health = new Health(UpgradeManager.GetUpgradeInfo(UpgradeType.Health).GetCurrentUpgradeValue());
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        AudioManager.Instance.PlaySound(TypeOfSound.PlayerHurt);
    }
    protected override void Die()
    {
        base.Die();
        
        playerAttributes.SetActive(false);
        Animator.SetBool("IsDead", true);
        
        OnPlayerDead?.Invoke();
        
        AudioManager.Instance.PlaySound(TypeOfSound.PlayerFall);
        Destroy(gameObject, Global.PlayerDestroyTime);
    }
    
}
