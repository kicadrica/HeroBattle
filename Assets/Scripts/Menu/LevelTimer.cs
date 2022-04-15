using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static float LevelTime;
    private bool _isTimerActive;
    
    private void Start()
    {
        MonsterSpawner.OnLevelComplete += StopTimer;
       
        LevelTime = 0;
        _isTimerActive = true;
    }

    private void OnDestroy()
    {
        MonsterSpawner.OnLevelComplete -= StopTimer;
    }

    private void Update()
    {
        if (!_isTimerActive) return;
        LevelTime += Time.deltaTime;
    }

    private void StopTimer()
    {
        _isTimerActive = false;
    }
}
