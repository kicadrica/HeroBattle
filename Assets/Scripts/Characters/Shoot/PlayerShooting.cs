using System;
using UnityEngine;

public class PlayerShooting : BaseShooting
{
    [SerializeField] private Transform[] ShootPoints;

    private int _damage;
    private int _spreadCount;
    
    private void Start()
    {
        _damage = UpgradeManager.GetUpgradeInfo(UpgradeType.Damage).GetCurrentUpgradeValue();
        _spreadCount = UpgradeManager.GetUpgradeInfo(UpgradeType.BulletSpread).GetCurrentUpgradeValue();
        Damage = _damage / _spreadCount;
    }

    protected override BulletsData RequestBulletData()
    {
        if (GameController.IsGameOver) return null;
        if (MonsterController.ActiveMonsters.Count == 0) return null;
        
        var startIndex = 0;
        var endIndex = _spreadCount;
        if (_spreadCount % 2 == 0) {
            startIndex++;
            endIndex++;
        }
        
        var bulletsData = new BulletsData();
        
        for (int i = startIndex; i < endIndex; i++)
        {
            var bullet = Pool.GetFromPool<Bullet>(TypeOfPool.PlayerBullet);
            bulletsData.Bullets.Add(bullet);
            bulletsData.ShootTransforms.Add(ShootPoints[i]);
        }

        return bulletsData;
    }
}
