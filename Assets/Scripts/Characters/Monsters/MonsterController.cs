using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterController : AbstractCharacter, IPoolItem
{
    [HideInInspector] public int CoinsForMonster = 30;
    public TypeOfPool MonsterType;
    public Transform PointOfDeath;

    // This is the time it takes for the death animation to play out.
    [SerializeField] private float DeathAnimTime; 
    public static event Action<MonsterController> OnMonsterDie;
    
    public static List<MonsterController> ActiveMonsters = new List<MonsterController>();

    private Collider2D _col;
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ReturnToPool;
        ActiveMonsters.Add(this);
        Animator.SetBool("IsDead", false);
        Animator.SetBool("IsHit", false);
    }
    protected override void Awake()
    {
        base.Awake();
        _col = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ReturnToPool;
        ActiveMonsters.Remove(this);
        TurnOffAllComponents();
    }

    public override void TakeDamage(float damageAmount)
    {
        Animator.SetBool("IsHit", true);
        _rb.AddForce(Vector2.up * 1.3f, ForceMode2D.Impulse);
        base.TakeDamage(damageAmount);
    }

    protected override void Die()
    {
        base.Die();
        _col.enabled = false;
        _rb.velocity = Vector2.zero;

        StartCoroutine(Animation_OnDie());
    }
    
    private IEnumerator Animation_OnDie()
    {
        Animator.SetBool("IsDead", true);
        
        yield return new WaitForSeconds(DeathAnimTime);
        
        var explosionEffect = Pool.GetFromPool<ImpactEffectController>(TypeOfPool.MonsterExplosion);
        explosionEffect.transform.position = PointOfDeath.position;
        AudioManager.Instance.PlaySound(TypeOfSound.Explosion);
        OnMonsterDie?.Invoke(this);
        Pool.PutToPool(MonsterType, this);
    }
    
    private void ReturnToPool(Scene arg0 = default, LoadSceneMode arg1 = default)
    {
        Pool.PutToPool(MonsterType, this);
    }
    
    public void ResetPoolItem()
    {
        _col.enabled = true;
        IsDead = false;
        Health.Restore(float.MaxValue);
        Animator.SetBool("IsDead", false);
        Animator.Rebind();
        Animator.Update(0f);
    }
}
