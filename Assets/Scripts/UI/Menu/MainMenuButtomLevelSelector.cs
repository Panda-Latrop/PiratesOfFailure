using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtomLevelSelector : MainMenuButtom
{
   // [HideInInspector]
 // public  List<LevelProperty> Segments = new List<LevelProperty>();
    public int levelId;
  public override void Action()
  {
      MMWC.StartLevel(levelId);
  }
}
