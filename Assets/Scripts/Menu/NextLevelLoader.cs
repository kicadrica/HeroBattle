using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelLoader : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener( ()=> {
            LevelManager.SetNextLevel();
            SceneManager.LoadScene("Game");
        });
    }
    
}
