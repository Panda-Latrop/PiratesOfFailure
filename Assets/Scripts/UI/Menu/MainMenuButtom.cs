using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtom : MonoBehaviour, IPointerClickHandler
{
    public MainMenuWindowController MMWC;
    public void OnPointerClick(PointerEventData eventData)
    {
        Action();
    }
     public virtual void Action()
     {
         return;
     }
}
