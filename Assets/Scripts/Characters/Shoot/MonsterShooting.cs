using System.Collections.Generic;

public class MonsterShooting : BaseShooting
{
    protected override List<Bullet> GetBulletsList()
    {
        if (GameController.IsGameOver) return null;
        if (transform.position.y > ScreenBorders.MaxY) return null;

        var bulletsList = new List<Bullet>();

        for (var i = 0; i < shootTransforms.Length; i++)
        {
            var bullet = Pool.GetObject<Bullet>(TypeOfPool.MonsterBullet);

            var bulletTransform = bullet.transform;
            bulletTransform.position = shootTransforms[i].position;
            bulletTransform.up = shootTransforms[i].up;
            
            bulletsList.Add(bullet);
        }

        return bulletsList;
    }

    protected override void PlayShootEffects()
    {
        animator.SetTrigger("Attack");
        AudioManager.Instance.PlaySound(TypeOfSound.MonsterShooting);
    }
}
