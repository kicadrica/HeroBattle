using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool IsGameOver;

    public static int CoinsPerLevel { get; private set; } = 0;
    private void Start()
    {
        IsGameOver = false;
        CoinsPerLevel = 0;
        AudioManager.Instance.PlayMusic(TypeOfSound.Music);
        
        MonsterController.OnMonsterDie += DropCoins;
        PlayerController.OnPlayerDead += GameOver;
        MonsterSpawner.OnLevelComplete += IncreaseTotalMoney;
    }

    private void OnDestroy()
    {
        MonsterController.OnMonsterDie -= DropCoins;
        PlayerController.OnPlayerDead -= GameOver;
        MonsterSpawner.OnLevelComplete -= IncreaseTotalMoney;
    }

    private void DropCoins(MonsterController monster)
    {
        if (monster.coinsForMonster == 0) return;
        
        for (int i = 0; i < CalculateShownCoins(monster.coinsForMonster); i++) {
            var coin = Pool.GetObject<Coin>(TypeOfPool.Coin);
            coin.transform.position = monster.transform.position;
            coin.Show();
        }
        
        CoinsPerLevel += monster.coinsForMonster;
    }

    private int CalculateShownCoins(int coinsForMonster)
    {
        if (coinsForMonster <= 30) {
            return 5;
        }
        if (coinsForMonster < 100) {
            return 8;
        }
        return 13;
    }
    
    private void IncreaseTotalMoney()
    {
        PlayerMoney.AddCoins(CoinsPerLevel);
        Debug.Log( PlayerMoney.TotalMoney);
    }
    
    private void GameOver()
    {
        IsGameOver = true;
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySound(TypeOfSound.GameOver);
    }
}
