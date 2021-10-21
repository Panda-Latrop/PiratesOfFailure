using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSwitchButtom : MonoBehaviour,IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData)
    {
        groupManager.Instance.SwitchWeapon();
    }
}
