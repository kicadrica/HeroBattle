using UnityEngine;
using UnityEngine.UI;

public class PrefsCleaner : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(DeleteAllPlayerPrefs);
    }

    private void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
