using DG.Tweening;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    private const float RotationZ = 5.162f;
    private const float FlightTime = 2f;
    private const float FallDuration = 0.7f;
    private const float FallPosY = -0.3f;
    private const float ScaleCoefficient = 0.3f;
    
    
    [SerializeField] private Transform PlayButton;
    [SerializeField] private Transform PlayText;
    
    [SerializeField] private Transform Player;
    [SerializeField] private GameObject PlayerShadow;

    [SerializeField] private Transform[] Points;
    
    private void Awake()
    {
        PlayButton.localRotation = new Quaternion(0, 0, -RotationZ, 90);
        PlayText.localRotation = new Quaternion(0, 0, RotationZ, 90);
        Player.transform.localScale = Vector3.one * ScaleCoefficient;
    }
    private void Start()
    {
        StartAnim();
        AudioManager.Instance.PlayMusic(TypeOfSound.MenuMusic);
    }
    
    private void StartAnim()
    {
        var pathPoints = new Vector3[Points.Length];
        for (int i = 0; i < pathPoints.Length; i++) {
            pathPoints[i] = new Vector3(Points[i].position.x, Points[i].position.y);
        }
        
        PlayerShadow.SetActive(false);
        DOVirtual.DelayedCall(FlightTime, () => {
            PlayerShadow.SetActive(true);
        });
        
        AudioManager.Instance.PlaySound(TypeOfSound.PlayerFly);
        
        Player.DOScale(Vector3.one, FlightTime).SetEase(Ease.Linear);
        Player.DOPath(pathPoints, FlightTime, PathType.CatmullRom).SetEase(Ease.OutSine)
            .OnComplete(() => {
                PlayButton.DORotate(new Vector3(0, 0, 0), FallDuration).SetEase(Ease.OutBounce);
                AudioManager.Instance.PlaySound(TypeOfSound.FallPlank);
                Player.DOMoveY(FallPosY, FallDuration).SetRelative().SetEase(Ease.OutBounce);
                }
            );
    }
    
}
