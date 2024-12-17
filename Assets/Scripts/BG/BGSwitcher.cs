using UnityEngine;
using UnityEngine.UI;

public class BGSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite[] backgroundSprites;
    
    private Image _image;
    
    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];
    }
    
}
