using System.Collections;
using UnityEngine;

public abstract class BaseShooting : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private float Force = 10f;
    [SerializeField]private float HitRate = 4f;
    [SerializeField]protected float Damage = 10f;
    
    protected Animator Animator;

    protected virtual void OnEnable()
    {
        Animator = GetComponent<Animator>();
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true) {
            yield return new WaitForSeconds(1 / HitRate);
            MakeShoot();

        }
    }
    protected abstract void MakeShoot();
    
    protected void ShootBullet(Bullet bullet, Transform shootPoint = null) 
    {
        bullet.Setup(Damage, tag);
        
        if (shootPoint == null) {
            shootPoint = ShootPoint;
        }
        
        bullet.transform.position = new Vector3(shootPoint.position.x, shootPoint.position.y);

        bullet.Launch(shootPoint.up * Force);
    }
    public void TurnOff()
    {
        StopAllCoroutines();
    }
}
