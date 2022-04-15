using UnityEngine;

public class MapController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic(TypeOfSound.MenuMusic);
    }
    
}
