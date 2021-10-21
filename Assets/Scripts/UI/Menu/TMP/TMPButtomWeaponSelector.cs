using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TMPButtomWeaponSelector : MonoBehaviour, IPointerClickHandler
{
    public Image img;
    public int tup;
    public void OnPointerClick(PointerEventData eventData)
    {
        Action();
    }
    public virtual void Action()
    {
        if (tup == 0)
        {
            if (GameSettingManager.Instance.WP.firstWeaponType + 1 >= GameSettingManager.Instance.WP.weapon.Length)
            {
                GameSettingManager.Instance.WP.firstWeaponType = 0;
            }
            else
            {
                GameSettingManager.Instance.WP.firstWeaponType++;
            }

            img.sprite = GameSettingManager.Instance.WP.weapon[GameSettingManager.Instance.WP.firstWeaponType].weaponSpr;
        }
        else
        {
            {
                if (GameSettingManager.Instance.WP.secondWeaponType + 1 >= GameSettingManager.Instance.WP.weapon.Length)
                {
                    GameSettingManager.Instance.WP.secondWeaponType = 0;
                }
                else
                {
                    GameSettingManager.Instance.WP.secondWeaponType++;
                }

                img.sprite = GameSettingManager.Instance.WP.weapon[GameSettingManager.Instance.WP.secondWeaponType].weaponSpr;
            }
        }
    }
}
