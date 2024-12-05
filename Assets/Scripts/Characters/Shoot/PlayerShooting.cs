using System;
using UnityEngine;

public class PlayerShooting : BaseShooting
{
    [SerializeField] private Transform[] shootTransforms;

    private int _damage;
    private int _bulletCount;
    
    private void Start()
    {
        _damage = UpgradeManager.GetUpgradeInfo(UpgradeType.Damage).GetCurrentUpgradeValue();
        _bulletCount = UpgradeManager.GetUpgradeInfo(UpgradeType.BulletSpread).GetCurrentUpgradeValue();
        
        Debug.Assert(_damage > 0, "Bullet damage was less than or equal to zero");
        Debug.Assert(_bulletCount > 0, "Bullet count was less than or equal to zero");
        
        bulletDamage = (float)_damage / _bulletCount;
    }

    protected override BulletsData RequestBulletData()
    {
        if (GameController.IsGameOver) return null;
        if (MonsterController.ActiveMonsters.Count == 0) return null;
        
        var startIndex = 0;
        var endIndex = _bulletCount;
        if (_bulletCount % 2 == 0) {
            startIndex++;
            endIndex++;
        }
        
        var bulletsData = new BulletsData();
        
        for (var i = startIndex; i < endIndex; i++)
        {
            var bullet = Pool.GetFromPool<Bullet>(TypeOfPool.PlayerBullet);
            bulletsData.Bullets.Add(bullet);
            bulletsData.ShootTransforms.Add(shootTransforms[i]);
        }

        return bulletsData;
    }

    protected override void PlayShootEffects()
    {
        //No effects
    }
}
