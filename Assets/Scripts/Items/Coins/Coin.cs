using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    [SerializeField] private Transform CoinGraphic;
    private float _coinLifeTime = 1.8f;
    private float _speed = 15f;
    private float _distance = 0.3f;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ReturnToPool;
        StartCoroutine(CollectionCoin());
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ReturnToPool;
    }
    private void ReturnToPool(Scene arg0, LoadSceneMode arg1)
    {
        Pool.ReturnObject(TypeOfPool.Coin, this);
    }
    private IEnumerator CollectionCoin()
    {
        yield return new WaitForSeconds(_coinLifeTime);
        
        while (PlayerController.PlayerTransform && Vector3.Distance(transform.position, PlayerController.PlayerTransform.position) > _distance)
        {
            var dir = PlayerController.PlayerTransform.position - transform.position;
            dir.Normalize();
            transform.position += dir * Time.deltaTime * _speed;
            
            yield return null;
        }
        
        Pool.ReturnObject(TypeOfPool.Coin, this);
        AudioManager.Instance.PlaySound(TypeOfSound.CoinsAttraction);
    }

    public void Show()
    {
        CoinGraphic.localPosition = new Vector3(0, 1, 0);
        CoinGraphic.DOLocalMoveY(0, 1).SetEase(Ease.OutBounce);
        var randomPos = transform.position + (Vector3)Random.insideUnitCircle * 0.75f;
        transform.DOMove(randomPos, 0.5f).SetEase(Ease.OutBack);
        
        AudioManager.Instance.PlaySound(TypeOfSound.CoinsDrop);
    }
}
