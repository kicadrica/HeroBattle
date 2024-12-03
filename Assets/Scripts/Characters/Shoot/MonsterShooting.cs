using UnityEngine;
using UnityEngine.Serialization;

public class MonsterShooting : BaseShooting
{
    [FormerlySerializedAs("ShootPoint")] 
    [SerializeField] private Transform shootPoint;
    
    protected override BulletsData RequestBulletData()
    {
        if (GameController.IsGameOver) return null;
        if (transform.position.y > ScreenBorders.MaxY) return null;
        
        var bulletsData = new BulletsData();
        
        var bullet = Pool.GetFromPool<Bullet>(TypeOfPool.MonsterBullet);
        bulletsData.Bullets.Add(bullet);
        
        bulletsData.ShootTransforms.Add(shootPoint);

        Animator.SetTrigger("Attack");
        AudioManager.Instance.PlaySound(TypeOfSound.MonsterShooting);
        
        return bulletsData;
    }
}
