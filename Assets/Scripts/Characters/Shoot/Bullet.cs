using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    [SerializeField] private TypeOfPool BulletType;
    [SerializeField] private TypeOfPool HitType;
    private float _bulletDamage = 10;
    private float _timeToDestroy = 5f;
    
    private Rigidbody2D _rb;

    public void Setup(float damage, string creatorTag)
    {
        _bulletDamage = damage;
        tag = creatorTag;
    }
    public void Launch(Vector3 direction)
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(direction, ForceMode2D.Impulse);
        
        StartCoroutine(DestroyYourselfIfNoTrigger());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(tag)) return;
        var target = col.GetComponent<IDamagable>();
        
        if (target == null) return;
        target.TakeDamage(_bulletDamage);
        
        var hitEffect = Pool.GetFromPool<ImpactEffectController>(HitType);
        hitEffect.transform.position = transform.position;
        ReturnToPool();
        
        AudioManager.Instance.PlaySound(TypeOfSound.BulletHit);
    }

    private IEnumerator DestroyYourselfIfNoTrigger()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        ReturnToPool();
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += ReturnToPool;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ReturnToPool;
    }
    private void ReturnToPool(Scene arg0 = default, LoadSceneMode arg1 = default)
    {
        Pool.PutToPool(BulletType, this);
    }
}
