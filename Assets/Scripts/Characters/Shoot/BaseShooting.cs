using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShooting : MonoBehaviour, ICharacterComponent
{
    protected class BulletsData
    {
        public List<Transform> ShootTransforms = new();
        public List<Bullet> Bullets = new();
    }
    
    [SerializeField] private float Force = 10f;
    [SerializeField] private float HitRate = 4f;
    [SerializeField] protected float Damage = 10f;
    
    protected Animator Animator;
    private List<Bullet> _bulletsList = new ();
    private List<Transform> _shootTransformsList = new ();

    private void OnEnable()
    {
        Animator = GetComponent<Animator>();
        StartCoroutine(ShootCoroutine());
    }
    
    private IEnumerator ShootCoroutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1 / HitRate);
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

        for (int i = 0; i < _bulletsList.Count; i++)
        {
            _bulletsList[i].Setup(Damage, tag);
        
            _bulletsList[i].transform.position = new Vector3(_shootTransformsList[i].position.x, _shootTransformsList[i].position.y);
            _bulletsList[i].Launch(_shootTransformsList[i].up * Force);
        }
    }

    protected abstract BulletsData RequestBulletData();
    
    public void TurnOff()
    {
        StopAllCoroutines();
    }
}
