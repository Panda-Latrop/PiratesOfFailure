using UnityEngine;
using UnityEngine.EventSystems;
public class MainMenuButtomScreen : MainMenuButtom
{
    public int nextScreen;
    public override void Action()
    {
        MMWC.ChancgeScreen(nextScreen);
    }
}
