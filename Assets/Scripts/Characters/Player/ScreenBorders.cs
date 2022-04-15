using UnityEngine;

public class ScreenBorders : MonoBehaviour
{
    public static float MaxY { get; private set; }
    public static float MinY { get; private set; }
    public static float MaxX { get; private set; }
    public static float MinX { get; private set; }
    

    private void Start()
    {
        SetBorders();
    }

    private void SetBorders()
    {
        var screenCoords = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        MaxY = screenCoords.y;
        MinY = -screenCoords.y;
        MaxX = screenCoords.x;
        MinX = -screenCoords.x;
    }
}
