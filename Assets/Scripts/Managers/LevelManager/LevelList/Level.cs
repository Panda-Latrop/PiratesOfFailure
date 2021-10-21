using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RewardType
{
    finishLevel = 0,
    getScore = 1,
    getAllItems = 2,
    killEnemy = 3,
    finishInTime = 4
}

public class Level : MonoBehaviour 
{
    [System.Serializable]
    public struct LevelReward
    {
        public RewardType reward;
        public float rewardNeed;
        public bool complete;
    }
    public LevelReward[] rewards;
    [HideInInspector]
    public List<LevelProperty> Segments = new List<LevelProperty>();
}
