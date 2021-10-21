using UnityEngine;

public class GameSettingManager : Singleton<GameSettingManager>
{
    public SceneLoadParameters SLP;
    public UnitParameters UP;
    public WeaponParameters WP;
    public Resources Res;
}
//GameSettingManager.Instance.WP