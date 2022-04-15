using DG.Tweening;
using UnityEngine;

public class PlayerMover : MonoBehaviour, ICharacterComponent
{
    private const float TweeningDelay = 1.2f;
    private Vector2 _touchPos;
    private float _offset = 0.7f;
    private bool _isCharacterDead;

    private Collider2D _collider2D;
    
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
        if (newPos.x > ScreenBorders.MaxX - _offset) {
            newPos.x = ScreenBorders.MaxX - _offset;
        }
        if (newPos.x < ScreenBorders.MinX + _offset) {
            newPos.x = ScreenBorders.MinX + _offset;
        }
        if (newPos.y > ScreenBorders.MaxY - _offset) {
            newPos.y = ScreenBorders.MaxY - _offset;
        }
        if (newPos.y < ScreenBorders.MinY + _offset) {
            newPos.y = ScreenBorders.MinY + _offset;
        }

        transform.position = newPos;
    }
    
    private void MoveWhenLevelComplete()
    {
        GameController.IsGameOver = true;
       _collider2D.enabled = false;
       
        transform.DOMoveY(15, Global.WinDelay).SetDelay(TweeningDelay + 0.3f);
        DOVirtual.DelayedCall(TweeningDelay, () => {
            AudioManager.Instance.PlaySound(TypeOfSound.PlayerFly);
        });
    }
    
    public void TurnOff()
    {
        _isCharacterDead = true;
    }
}
