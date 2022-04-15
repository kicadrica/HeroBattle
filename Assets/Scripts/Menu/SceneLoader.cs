using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>SceneManager.LoadScene("MainScene"));    
    }
}
