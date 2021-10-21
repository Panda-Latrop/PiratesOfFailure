using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtomQuite : MainMenuButtom
{
   public int loadScreen;
    public override void Action()
    {
        GameSettingManager.Instance.SLP.mainMenuLoadScreen = loadScreen;
        (MMWC as MenuWindowController).BackToMenu();
    }
}
