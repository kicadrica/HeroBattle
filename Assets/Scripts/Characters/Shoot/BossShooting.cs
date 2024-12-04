using System;
using System.Collections;
using UnityEngine;

public class BossShooting : MonoBehaviour, ICharacterComponent
{
    private const float AnimationDelay = 0.5f;

    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private float bulletDamage = 10f;
    
    [SerializeField] private Animator animator;
    
    [Serializable]
    public class ShootStage
    {
        public float stageDelay;
        public Transform[] shootTransforms;
    }

    [SerializeField] private ShootStage[] shootStages;
    
    private void OnEnable()
    {
        StartCoroutine(MassShoot());
    }

    private IEnumerator MassShoot()
    {
        while (true) {

            for (var i = 0; i < shootStages.Length; i++) {
                var delay = shootStages[i].stageDelay;
                yield return new WaitForSeconds(delay - 0.5f);
                
                animator.SetBool("IsBossAttack", true);
                
                yield return new WaitForSeconds(AnimationDelay);
                animator.SetBool("IsBossAttack", false);
                
                foreach (var shootPoint in shootStages[i].shootTransforms) {
                    var bullet = Pool.GetFromPool<Bullet>(TypeOfPool.MonsterBullet);
                    ShootBullet(bullet, shootPoint);
                    AudioManager.Instance.PlaySound(TypeOfSound.MonsterShooting);
                }
            }
        }
    }
    
    private void ShootBullet(Bullet bullet, Transform shootPoint)
    {
        bullet.Setup(bulletDamage, tag);

        bullet.transform.position = new Vector3(shootPoint.position.x, shootPoint.position.y);

        bullet.Launch(shootPoint.up * bulletForce);
    }

    public void TurnOff()
    {
        StopAllCoroutines();
    }
}
