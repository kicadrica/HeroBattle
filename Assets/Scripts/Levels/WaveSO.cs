using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Wave", fileName = "Wave")]
public class WaveSO : ScriptableObject
{
    [Serializable]
    public class StageInfo
    {
        public TypeOfPool MonsterType;
        public int MonstersCount;
        public float HealthCoef = 1f;
        public float ScaleCoef = 1f;
        public int Coins;
        
        public float Delay;
    }

    public StageInfo[] Stages;
}
