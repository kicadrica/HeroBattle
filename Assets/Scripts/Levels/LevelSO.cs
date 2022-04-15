using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level", fileName = "Level")]
public class LevelSO : ScriptableObject
{
    public WaveSO[] Waves;
    public int NumberOfLevel;
}
