using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelReward : MonoBehaviour 
{
    
    public Image[] coins;
     
    public float enemyKilled =0,collectedItems = 0;
    bool[] preReward = new bool[3];
    int levelId,count = 0;
    bool showReward;
    float timeAdd = 0;

    public void Start()
    {
        levelId = GameSettingManager.Instance.SLP.currentLoadLevel;
        for (int i = 0; i < 3; i++)
        {
            if (LevelList.Instance.levels[levelId].rewards[i].complete)
            {
                preReward[i] = true;
                coins[i].color = new Color(255, 255, 225, 255);
            }
            else
            {
                preReward[i] = false;
                coins[i].color = new Color(255, 255, 225, 0);
            }
        }
    }
    public void LevelFinished()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (LevelList.Instance.levels[levelId].rewards[i].reward)
            {
                case RewardType.finishLevel:
                    {
                        LevelList.Instance.levels[levelId].rewards[i].complete = true;
                        break;
                    }
                case RewardType.getScore:
                    {
                        if (GameSettingManager.Instance.Res.score >= LevelList.Instance.levels[levelId].rewards[i].rewardNeed)
                        {
                            LevelList.Instance.levels[levelId].rewards[i].complete = true;
                        }
                        break;
                    }
                case RewardType.getAllItems:
                    {
                        if (collectedItems >= LevelList.Instance.levels[levelId].rewards[i].rewardNeed)
                        {
                            LevelList.Instance.levels[levelId].rewards[i].complete = true;
                        }
                        break;
                    }
                case RewardType.killEnemy:
                    {
                        if (enemyKilled >= LevelList.Instance.levels[levelId].rewards[i].rewardNeed)
                        {
                            LevelList.Instance.levels[levelId].rewards[i].complete = true;
                        }
                        break;
                    }
                case RewardType.finishInTime:
                    {
                        if (Time.time < LevelList.Instance.levels[levelId].rewards[i].rewardNeed)
                        {
                            LevelList.Instance.levels[levelId].rewards[i].complete = true;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }
        showReward = true;
    }

    public void Update()
    {
        if (showReward)
        {
            if (count == 3)
            {
                showReward = false;
                return;
            }
            if (preReward[count] != LevelList.Instance.levels[levelId].rewards[count].complete)
            {
                if (timeAdd > 0.5f)
                {
                    coins[count].color = new Color(255, 255, 225, 255);
                    timeAdd = 0;
                    count++;
                }
            }
            else
            {
                timeAdd = 0;
                count++;
            }
            timeAdd += Time.deltaTime;
        }
    }
}
