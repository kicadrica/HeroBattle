using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneLoader : MonoBehaviour
{
    [SerializeField] private bool SwitchToUpgrade;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=> {
            MapUpgradeSwitcher.ForceToUpgrade = SwitchToUpgrade;
            SceneManager.LoadScene("MainScene");
        });
    }

}
