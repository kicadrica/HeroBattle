using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolChecker : MonoBehaviour
{
    private static bool _isOneOfStartSceneLoaded;
    private PoolChecker _instance;
    
    private void Awake()
    {
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (sceneIndex == 1)
        {
            _isOneOfStartSceneLoaded = true;
        }
        
        if (sceneIndex == 2 && _isOneOfStartSceneLoaded == false)
        {
            throw new ArgumentException(
                "The object pool has not been loaded. Please start the game from the first scene.");
        }
    }
    
    
}
