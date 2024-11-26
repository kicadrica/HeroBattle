using DG.Tweening;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    private const float RotationZ = 5.162f;
    private const float FlightTime = 2f;
    private const float FallDuration = 0.7f;
    private const float FallPosY = -0.3f;
    private const float ScaleCoefficient = 0.3f;
    
    
    [SerializeField] private Transform playButton;
    [SerializeField] private Transform playText;
    
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject playerShadow;

    [SerializeField] private Transform[] points;
    
    private void Awake()
    {
        playButton.localRotation = new Quaternion(0, 0, -RotationZ, 90);
        playText.localRotation = new Quaternion(0, 0, RotationZ, 90);
        playerTransform.transform.localScale = Vector3.one * ScaleCoefficient;
    }
    private void Start()
    {
        StartAnim();
        AudioManager.Instance.PlayMusic(TypeOfSound.MenuMusic);
    }
    
    private void StartAnim()
    {
        var pathPoints = new Vector3[points.Length];
        for (int i = 0; i < pathPoints.Length; i++) {
            pathPoints[i] = new Vector3(points[i].position.x, points[i].position.y);
        }
        
        playerShadow.SetActive(false);
        DOVirtual.DelayedCall(FlightTime, () => {
            playerShadow.SetActive(true);
        });
        
        AudioManager.Instance.PlaySound(TypeOfSound.PlayerFly);
        
        playerTransform.DOScale(Vector3.one, FlightTime).SetEase(Ease.Linear);
        playerTransform.DOPath(pathPoints, FlightTime, PathType.CatmullRom).SetEase(Ease.OutSine)
            .OnComplete(() => {
                playButton.DORotate(new Vector3(0, 0, 0), FallDuration).SetEase(Ease.OutBounce);
                AudioManager.Instance.PlaySound(TypeOfSound.FallPlank);
                playerTransform.DOMoveY(FallPosY, FallDuration).SetRelative().SetEase(Ease.OutBounce);
                }
            );
    }
    
}
