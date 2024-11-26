using DG.Tweening;
using UnityEngine;

public class PlayerMover : MonoBehaviour, ICharacterComponent
{
    private const float Offset = 0.7f;
    private const float MainAnimDelay = 1.2f;
    private const float ExtraAnimDelay = 0.3f;
    
    private Vector2 _touchPos;
    private Collider2D _collider2D;
    
    private bool _isCharacterDead;
    
    private void OnEnable()
    {
        _isCharacterDead = false;
    }
    
    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
       _collider2D.enabled = true;
        TouchController.OnTouch += SavePos;
        TouchController.OnDragMethod += MovePlayer;
        MonsterSpawner.OnLevelComplete += MoveWhenLevelComplete;
    }

    private void OnDestroy()
    {
        TouchController.OnTouch -= SavePos;
        TouchController.OnDragMethod -= MovePlayer;
        MonsterSpawner.OnLevelComplete -= MoveWhenLevelComplete;
    }
    
    private void SavePos()
    {
        _touchPos = transform.position;
    }
    
    private void MovePlayer(Vector2 direction)
    {
        if (_isCharacterDead) return;
        if (GameController.IsGameOver) return;
        
        var newPos = _touchPos + direction;
        if (newPos.x > ScreenBorders.MaxX - Offset) {
            newPos.x = ScreenBorders.MaxX - Offset;
        }
        if (newPos.x < ScreenBorders.MinX + Offset) {
            newPos.x = ScreenBorders.MinX + Offset;
        }
        if (newPos.y > ScreenBorders.MaxY - Offset) {
            newPos.y = ScreenBorders.MaxY - Offset;
        }
        if (newPos.y < ScreenBorders.MinY + Offset) {
            newPos.y = ScreenBorders.MinY + Offset;
        }

        transform.position = newPos;
    }
    
    private void MoveWhenLevelComplete()
    {
        GameController.IsGameOver = true;
       _collider2D.enabled = false;
       
        transform.DOMoveY(15, Global.WinDelay).SetDelay(MainAnimDelay + ExtraAnimDelay);
        DOVirtual.DelayedCall(MainAnimDelay, () => {
            AudioManager.Instance.PlaySound(TypeOfSound.PlayerFly);
        });
    }
    
    public void TurnOff()
    {
        _isCharacterDead = true;
    }
}
