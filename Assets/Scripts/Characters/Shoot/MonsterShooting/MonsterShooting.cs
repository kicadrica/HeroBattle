
public class MonsterShooting : BaseShooting
{
    protected override void MakeShoot()
    {
        if (GameController.IsGameOver) return;
        if (transform.position.y > ScreenBorders.MaxY) return;
        
        var bullet = Pool.GetFromPool<Bullet>(TypeOfPool.MonsterBullet);
        ShootBullet(bullet);
        
        Animator.SetTrigger("Attack");
        
        AudioManager.Instance.PlaySound(TypeOfSound.MonsterShooting);
    }
  
}
