using UnityEngine;
using UnityEngine.UI;

public class BGSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite[] BGSprites;
    private Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = BGSprites[Random.Range(0, BGSprites.Length)];
    }
    
}
