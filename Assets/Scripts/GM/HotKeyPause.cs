using UnityEngine;

public class HotKeyPause : MonoBehaviour
{
    [SerializeField] private KeyCode Key;

    private void Update()
    {
        if (Input.GetKeyDown(Key)) {
            Debug.Break();
        }
    }
}
