using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShooting : MonoBehaviour, ICharacterComponent
{
    protected class BulletsData
    {
        public readonly List<Transform> ShootTransforms = new();
        public readonly List<Bullet> Bullets = new();
    }
    
    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private float shotsPerSecond = 4f;
    [SerializeField] protected float bulletDamage = 10f;
    [SerializeField] protected Animator animator;
    
    private List<Bullet> _bulletsList = new ();
    private List<Transform> _shootTransformsList = new ();

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
        var bulletData = RequestBulletData();
        if (bulletData == null) return;
        
        _bulletsList.Clear();
        _shootTransformsList.Clear();

        _bulletsList = bulletData.Bullets;
        _shootTransformsList = bulletData.ShootTransforms;

        for (var i = 0; i < _bulletsList.Count; i++)
        {
            _bulletsList[i].Setup(bulletDamage, tag);
        
            _bulletsList[i].transform.position = new Vector3(_shootTransformsList[i].position.x, _shootTransformsList[i].position.y);
            _bulletsList[i].Launch(_shootTransformsList[i].up * bulletForce);
        }
    }

    protected abstract BulletsData RequestBulletData();
    
    public void TurnOff()
    {
        StopAllCoroutines();
    }
}
