using UnityEngine;
public class WeaponParameters : MonoBehaviour 
{
    [System.Serializable]
    public struct WeaponStandardParameters
    {
        public string name;
        public Sprite weaponSpr;
        public int bulletType, damage;
        public Vector2 shootPos;
      //  public int shootParticleType;
        public float fireRate, spreadMax, spreadMin;
    }
    public int currentWeaponType, firstWeaponType, secondWeaponType;
    public WeaponStandardParameters[] weapon;
}
