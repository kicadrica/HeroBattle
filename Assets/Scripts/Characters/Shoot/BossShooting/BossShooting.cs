using System;
using System.Collections;
using UnityEngine;

public class BossShooting : BaseShooting
{
    [Serializable]
    public class ShootStage
    {
        public float StageDelay;
        public Transform[] Points;
    }

    [SerializeField] private ShootStage[] ShootStages;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(MassShoot());
    }
    protected override void MakeShoot()
    {
        //Not used
    }

    private IEnumerator MassShoot()
    {
        while (true) {

            for (int i = 0; i < ShootStages.Length; i++) {
                var delay = ShootStages[i].StageDelay;
                yield return new WaitForSeconds(delay - 0.5f);
                
                Animator.SetBool("IsBossAttack", true);
                
                yield return new WaitForSeconds(0.5f);
                Animator.SetBool("IsBossAttack", false);
                
                foreach (var shootPoint in ShootStages[i].Points) {
                    var bullet = Pool.GetFromPool<Bullet>(TypeOfPool.MonsterBullet);
                    ShootBullet(bullet, shootPoint);
                    AudioManager.Instance.PlaySound(TypeOfSound.MonsterShooting);
                }
            }
        }
    }

}
