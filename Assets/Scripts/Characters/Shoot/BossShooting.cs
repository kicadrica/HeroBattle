using System;
using System.Collections;
using UnityEngine;

public class BossShooting : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private float Force = 10f;
    [SerializeField] protected float Damage = 10f;
    
    private Animator _animator;
    
    [Serializable]
    public class ShootStage
    {
        public float StageDelay;
        public Transform[] Points;
    }

    [SerializeField] private ShootStage[] ShootStages;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        StartCoroutine(MassShoot());
    }

    private IEnumerator MassShoot()
    {
        while (true) {

            for (int i = 0; i < ShootStages.Length; i++) {
                var delay = ShootStages[i].StageDelay;
                yield return new WaitForSeconds(delay - 0.5f);
                
                _animator.SetBool("IsBossAttack", true);
                
                yield return new WaitForSeconds(0.5f);
                _animator.SetBool("IsBossAttack", false);
                
                foreach (var shootPoint in ShootStages[i].Points) {
                    var bullet = Pool.GetFromPool<Bullet>(TypeOfPool.MonsterBullet);
                    ShootBullet(bullet, shootPoint);
                    AudioManager.Instance.PlaySound(TypeOfSound.MonsterShooting);
                }
            }
        }
    }
    
    protected void ShootBullet(Bullet bullet, Transform shootPoint)
    {
        bullet.Setup(Damage, tag);

        bullet.transform.position = new Vector3(shootPoint.position.x, shootPoint.position.y);

        bullet.Launch(shootPoint.up * Force);
    }

    public void TurnOff()
    {
        StopAllCoroutines();
    }
}
