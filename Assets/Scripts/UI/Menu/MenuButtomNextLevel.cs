using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtomNextLevel : MainMenuButtom
{
    public override void Action()
    {
        if (GameSettingManager.Instance.SLP.currentLoadLevel + 1 >= LevelList.Instance.levels.Length)
        {
            GameSettingManager.Instance.SLP.mainMenuLoadScreen = 2;
            (MMWC as MenuWindowController).BackToMenu();
        }
        else
            MMWC.StartLevel(GameSettingManager.Instance.SLP.currentLoadLevel + 1);
    }
}
