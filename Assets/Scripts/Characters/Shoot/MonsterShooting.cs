using UnityEngine;
using UnityEngine.Serialization;

public class MonsterShooting : BaseShooting
{
    [FormerlySerializedAs("ShootPoint")] 
    [SerializeField] private Transform shootTransform;
    
    protected override BulletsData RequestBulletData()
    {
        if (GameController.IsGameOver) return null;
        if (transform.position.y > ScreenBorders.MaxY) return null;
        
        var bulletsData = new BulletsData();
        
        var bullet = Pool.GetFromPool<Bullet>(TypeOfPool.MonsterBullet);
        bulletsData.Bullets.Add(bullet);
        
        bulletsData.ShootTransforms.Add(shootTransform);
        
        return bulletsData;
    }

    protected override void PlayShootEffects()
    {
        animator.SetTrigger("Attack");
        AudioManager.Instance.PlaySound(TypeOfSound.MonsterShooting);
    }
}
