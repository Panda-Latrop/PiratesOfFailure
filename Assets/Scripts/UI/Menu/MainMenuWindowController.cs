using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuWindowController : MonoBehaviour
{
    public int currentScreen;
    [System.Serializable]
    public struct MainMenuScreens
    {
        public int backScreen;
        public GameObject screen;
    }
    public MainMenuScreens[] screens;
    public virtual void Start()
    {
        ChancgeScreen(GameSettingManager.Instance.SLP.mainMenuLoadScreen);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }
    public virtual void StartLevel(int levelId)
    {
        GameSettingManager.Instance.WP.currentWeaponType = GameSettingManager.Instance.WP.firstWeaponType;
        GameSettingManager.Instance.SLP.mainMenuLoadScreen = -1;
        if (levelId != -1)
            GameSettingManager.Instance.SLP.currentLoadLevel = levelId;
        SceneManager.LoadScene(1);
    }
    public virtual void ChancgeScreen(int screenId)
    {
        screens[screenId].screen.SetActive(true);
        currentScreen = screenId;
        for (int i = screenId + 1; i < screens.Length; i++)
        {
            screens[i].screen.SetActive(false);
        }
        for (int i = screenId - 1; i >= 0; i--)
        {
            screens[i].screen.SetActive(false);
        }
    }
    public virtual void Back()
    {
        if (currentScreen > 0)
        {
            ChancgeScreen(screens[currentScreen].backScreen);
        }
        else
        {
            Application.Quit();
        }
    }
}
