using UnityEngine.UI;
using UnityEngine;

public class LevelResource : MonoBehaviour
{
    public Text scoreText, goldText, diamondsText;

    public void Start()
    {
        GameSettingManager.Instance.Res.score = 0;
        scoreText.text = GameSettingManager.Instance.Res.score.ToString();
        goldText.text = GameSettingManager.Instance.Res.golds.ToString();
        diamondsText.text = GameSettingManager.Instance.Res.diamonds.ToString();
    }
    public void AddResource(int resType, int value)
    {
        switch (resType)
        {
            case 0:
                {
                    GameSettingManager.Instance.Res.score += value;
                    scoreText.text = GameSettingManager.Instance.Res.score.ToString();
                    break;
                }
            case 1:
                {
                    GameSettingManager.Instance.Res.golds += value;
                    goldText.text = GameSettingManager.Instance.Res.golds.ToString();
                    break;
                }
            case 2:
                {
                    GameSettingManager.Instance.Res.diamonds += value;
                    diamondsText.text = GameSettingManager.Instance.Res.diamonds.ToString();
                    break;
                }
            default:
                break;
        }
    }
}
