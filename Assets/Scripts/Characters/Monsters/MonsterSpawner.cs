using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    private const float MobsDelay = 3f;
    private const float Offset = 0.3f;
    
    public static event Action OnLevelComplete;

    [SerializeField] private Transform[] Points;

    private int _coinsForLevel = 0; //Temporary variable
    private void Start()
    {
        SceneManager.sceneLoaded += ReturnToPool;
        
        StartCoroutine(SpawnWaves());
        
        for (var waveIndex = 0; waveIndex < LevelManager.CurLevel.Waves.Length; waveIndex++) { //Temporary for count coins per level
            var wave = LevelManager.CurLevel.Waves[waveIndex];
            foreach (var stage in wave.Stages) {
                _coinsForLevel += stage.Coins;
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ReturnToPool;
    }
    
    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(MobsDelay);

        for (var waveIndex = 0; waveIndex < LevelManager.CurLevel.Waves.Length; waveIndex++) {
            var wave = LevelManager.CurLevel.Waves[waveIndex];
            foreach (var stage in wave.Stages) {
                yield return WaitNextStage(stage.Delay);
                SpawnStages(stage);
            }
            
            while (MonsterController.ActiveMonsters.Count > 0) yield return null;
        }
        
        OnLevelComplete?.Invoke();
    }

    private IEnumerator WaitNextStage(float delay)
    {
        while (delay > 0 && MonsterController.ActiveMonsters.Count > 0) {
            delay -= Time.deltaTime;
            yield return null;
        }
    }

    private void SpawnStages(WaveSO.StageInfo stage)
    {
        for (var i = 0; i < stage.MonstersCount; i++) {
            var monster = Pool.GetFromPool<MonsterController>(stage.MonsterType);

            var randomPoint = Random.Range(0, Points.Length);
            var randomOffset = Random.Range(-Offset, Offset);
            var pos = Points[randomPoint].position + Vector3.one * randomOffset;
            monster.transform.position = pos;

            monster.transform.localScale = Vector3.one * stage.ScaleCoef;
            monster.Health.Coef = stage.HealthCoef;
            
            if (i == stage.MonstersCount - 1) {
                //Calculate last monster in stage.
                monster.CoinsForMonster = stage.Coins;
            } else {
                monster.CoinsForMonster = 0;
            }
            
        }
    }
    
    private void ReturnToPool(Scene arg0 = default, LoadSceneMode arg1 = default)
    {
        StopAllCoroutines();
    }
}

    

