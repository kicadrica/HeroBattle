using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelSO[] _levelList;
    public static LevelSO CurLevel { get; private set; }
    public static int CurLevelNumber {
        get => PlayerPrefs.GetInt("Level", 1);
        set {
            if (value > _instance._levelList.Length - 1) {
                value = _instance._levelList.Length - 1;
            }
            PlayerPrefs.SetInt("Level", value);
        }
    }

    private static LevelManager _instance;
    private void Awake()
    {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SetCurLevel(CurLevelNumber);
            
        } else {
            Destroy(gameObject);
        }
    }

    private LevelSO GetLevel(int levelNumber)
    {
        for (int i = 0; i < _levelList.Length; i++) {
            if (_levelList[i].NumberOfLevel == levelNumber) {
                return _levelList[i];
            }
        }
        return _levelList[_levelList.Length - 1];
    }

    public static void SetCurLevel(int levelNumber)
    {
        CurLevel = _instance.GetLevel(levelNumber);
    }

    public static void SetNextLevel()
    {
        var nextLevelNumber = CurLevel.NumberOfLevel + 1;
        var nextLevel = _instance.GetLevel(nextLevelNumber);
        if(nextLevel == null) return;
        CurLevel = nextLevel;

    }
}
