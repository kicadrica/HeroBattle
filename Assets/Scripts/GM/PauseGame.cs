using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool IsPause;
    private void Awake()
    {
        Resume();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        IsPause = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsPause = false;
    }
}
