using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private int _firstLevel = 1;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadGame);
    }
    private void LoadGame()
    {
        if (LevelManager.CurLevel == null && PlayerPrefs.GetInt("Level") < 2) {
            LevelManager.SetCurLevel(_firstLevel);
            SceneManager.LoadScene("Game");
        }
        else {
            SceneManager.LoadScene("MainScene");
        }
        
        AudioManager.Instance.PlaySound(TypeOfSound.PressButton);
    }
}
