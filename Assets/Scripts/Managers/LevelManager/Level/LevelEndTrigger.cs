using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour 
{
    public bool active = true;
    public int toScreen;

    public string targetTag;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (active && coll.gameObject.tag == targetTag)
        {
            LevelManager.Instance.LRew.LevelFinished();
            MenuWindowController MWC =  GameObject.Find("Menu").GetComponent<MenuWindowController>();
            MWC.ControllerActive(false);
            MWC.ChancgeScreen(toScreen);
            active = false;       
        }
    }
}
