using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterController : AbstractCharacter, IPoolItem
{ 
    public static event Action<MonsterController> OnMonsterDie;
    public static readonly List<MonsterController> ActiveMonsters = new();
    
    [HideInInspector] public int coinsForMonster = 30;
    
    public TypeOfPool monsterType;
    public Transform pointOfDeath;
    
    [SerializeField] private float deathAnimTime = 0.7f;

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
        
        yield return new WaitForSeconds(deathAnimTime);
        
        var explosionEffect = Pool.GetFromPool<ImpactEffectController>(TypeOfPool.MonsterExplosion);
        explosionEffect.transform.position = pointOfDeath.position;
        AudioManager.Instance.PlaySound(TypeOfSound.Explosion);
        OnMonsterDie?.Invoke(this);
        Pool.PutToPool(monsterType, this);
    }
    
    private void ReturnToPool(Scene arg0 = default, LoadSceneMode arg1 = default)
    {
        Pool.PutToPool(monsterType, this);
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
