using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtomPause : MainMenuButtom
{
    public override void Action()
    {
        (MMWC as MenuWindowController).Pause();
    }
}
