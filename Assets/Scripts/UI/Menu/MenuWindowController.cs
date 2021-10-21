using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;


public class MenuWindowController : MainMenuWindowController
{

    public GameObject controlerConteiner;
    public Joistick_move jm;
    public Joistick_shoot js;
  /* public override void Start()
    {
        ChancgeScreen(-1);
    }*/

    public override void ChancgeScreen(int screenId)
    {
        if (screenId < 0)
        {
            Resume();
            return;
        }
        base.ChancgeScreen(screenId);
    }
    public override void Back()
    {
        if (currentScreen == 2)
            return;
        
        if (currentScreen >= 0)
        {
            ChancgeScreen(screens[currentScreen].backScreen);
        }
        else
        {
            Resume();
        }
    }
    public void Resume()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].screen.SetActive(false);
        }
        ControllerActive(true);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        ControllerActive(false);
        ChancgeScreen(0);
    }
    public  void BackToMenu()
    {
        //SceneLoadParametrs.Instance.mainMenuLoadScreen = 1;
        SceneManager.LoadScene(0);
    }
    public void ControllerActive(bool _flag)
    {
        jm.ResetJoistick();
        js.ResetJoistick();
        controlerConteiner.SetActive(_flag);
    }
}
