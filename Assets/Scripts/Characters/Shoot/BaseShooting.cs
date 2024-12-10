using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShooting : MonoBehaviour, ICharacterComponent
{
    [SerializeField] protected Animator animator;
    
    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private float shotsPerSecond = 4f;
    
    [SerializeField] protected float bulletDamage = 10f;
    [SerializeField] protected Transform[] shootTransforms;
    
    
    private void OnEnable()
    {
        StartCoroutine(ShootCoroutine());
    }
    
    private IEnumerator ShootCoroutine()
    {
        if (shotsPerSecond <= 0)
        {
            throw new Exception("Shots per second must be greater than zero.");
        }
        
        while (true) 
        {
            yield return new WaitForSeconds(1 / shotsPerSecond);
            Shoot();
        }
    }

    private void Shoot()
    {
        var bullets = GetBulletsList();
        if (bullets == null) return;
        

        foreach (var bullet in bullets)
        {
            bullet.Setup(bulletDamage, tag);
            bullet.Launch(bullet.transform.up * bulletForce);
        }
        
        PlayShootEffects();
    }

    protected abstract List<Bullet> GetBulletsList();

    protected virtual void PlayShootEffects(){}
    
    public void TurnOff()
    {
        StopAllCoroutines();
    }
}
