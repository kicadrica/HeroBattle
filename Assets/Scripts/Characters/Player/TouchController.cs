using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public static event Action OnTouch;
    public static event Action<Vector2> OnDragMethod;
    
    private Vector2 _startTouchPos;
    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _startTouchPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        OnTouch?.Invoke();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        var dragPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        var direction  = (Vector2)dragPos - _startTouchPos;
        OnDragMethod?.Invoke(direction);
    }
    
}
