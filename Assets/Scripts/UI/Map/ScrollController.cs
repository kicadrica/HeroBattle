using UnityEngine;
public class ScrollController : MonoBehaviour
{
    [SerializeField] private RectTransform ViewRT;
    [SerializeField] private RectTransform ContentRT;
    private void Start()
    {
        var halfHeight = ViewRT.rect.height / 2;
        var starPosY = GetCurrentStar().anchoredPosition.y;
        var anchoredPos = ContentRT.anchoredPosition;
        anchoredPos.y = -(starPosY - halfHeight);
        ContentRT.anchoredPosition = anchoredPos;
    }

    private RectTransform GetCurrentStar()
    {
        var allStars = ContentRT.GetComponentsInChildren<LevelSelection>();
      
        foreach (var star in allStars) {
            if (star.LevelNumber == LevelManager.CurLevel.NumberOfLevel) {
                return star.GetComponent<RectTransform>();
            }
        }

        return allStars[allStars.Length - 1].GetComponent<RectTransform>();
    }
    

}
