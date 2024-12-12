using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    private const float TimeToDestroy = 5f;
    
    [SerializeField] private TypeOfPool bulletType;
    [SerializeField] private TypeOfPool hitType;
    [SerializeField] private Rigidbody2D rb;
    
    private float _bulletDamage = 10;
    
    public void Setup(float damage, string creatorTag)
    {
        _bulletDamage = damage;
        tag = creatorTag;
    }
    
    public void Launch(Vector3 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
        
        StartCoroutine(DestroyYourselfIfNoTrigger());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (transform.position.y > ScreenBorders.MaxY) return;
        if (col.gameObject.CompareTag(tag)) return;
        
        var target = col.GetComponent<IDamagable>();
        if (target == null) return;
        
        target.TakeDamage(_bulletDamage);
        
        var hitEffect = Pool.GetObject<ImpactEffectController>(hitType);
        hitEffect.transform.position = transform.position;
        ReturnToPool();
        
        AudioManager.Instance.PlaySound(TypeOfSound.BulletHit);
    }

    private IEnumerator DestroyYourselfIfNoTrigger()
    {
        yield return new WaitForSeconds(TimeToDestroy);
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
        Pool.ReturnObject(bulletType, this);
    }
}
