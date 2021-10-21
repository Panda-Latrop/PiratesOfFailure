using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groupManager : DestroyableSingleton<groupManager>
{
    public List<unit_movement> group = new List<unit_movement>();
    public int maxCount;
    public Vector2 spawnPoint;
    public float timeToSpawn = 0f;
    internal bool coroutineStart = false;

   // public int unitCurrentXv, unitCurrentJump;
    public float unitCurrentLook;
    public bool unitLook,unitShoot;

    void Update()
    {
        if (groupManager.Instance.group.Count == 0 && !coroutineStart)
           StartCoroutine(WaitTimeToSpawn());
    }
    public void SpawnUnits(int unitTag)
    {
        UnityPoolObject temp;
        if (group.Count == 0)
        {
            temp = UnityPoolManager.Instance.Pop<UnityPoolObject>(unitTag);
            temp.SetTransform(spawnPoint, Quaternion.identity, Vector3.one);
            group.Add(temp.GetComponent<unit_movement>());
            group[0].idInArray = 0;
            group[0].commander = null;

            ResetUnitParameters(ref group[0].jumpHeight, ref group[0].minCommDis, ref group[0].spreadMax, ref group[0].spreadMin);
            SetWeapon(GameSettingManager.Instance.WP.currentWeaponType, group[0].armSpr, ref group[0].bulletType, group[0].shootPoint, ref group[0].fireRate, ref group[0].spreadMax, ref group[0].spreadMin);
        }
        for (int i = group.Count; i < maxCount; i++)
        {
            temp = UnityPoolManager.Instance.Pop<UnityPoolObject>(unitTag);
            if (group.Contains(temp.GetComponent<unit_movement>()))
            {
                Debug.LogWarning("Add twice");
                i--;
                continue;
            }
            temp.SetTransform(spawnPoint, Quaternion.identity, Vector3.one);
            group.Add(temp.GetComponent<unit_movement>());
            group[i].idInArray = i;
            group[i].commander = group[i - 1].gameObject;
            group[i].randomFireRate = Random.Range(0, 0.1f * (maxCount - 1));

            ResetUnitParameters(ref group[i].jumpHeight, ref group[i].minCommDis, ref group[i].spreadMax, ref group[i].spreadMin);
            SetWeapon(GameSettingManager.Instance.WP.currentWeaponType, group[i].armSpr, ref group[i].bulletType, group[i].shootPoint, ref group[i].fireRate, ref group[i].spreadMax, ref group[i].spreadMin);
        }
    }
    public void UnitsDeath(unit_movement gj)
    {
        group.Remove(gj);
        if (group.Count != 0)
        {
            group[0].idInArray = 0;
            group[0].commander = null;
            group[0].randomFireRate = 0;
            for (int i = 1; i < group.Count; i++)
            {
                group[i].idInArray = i;
                group[i].commander = group[i - 1].gameObject;
                group[i].randomFireRate = Random.Range(0, 0.1f * (group.Count - 1)); 
            }
        }
    }
    public void SetMoveX(int x_v)
    {
        if (group.Count != 0)
            group[0].SetMoveX(x_v);
    }
    public void SetJump(int type)
    {
        if (group.Count != 0)
            for (int i = 0; i < group.Count; i++)
                group[i].Jump(type);
    }
    public void SwitchWeapon()
    {
        int type = GameSettingManager.Instance.WP.currentWeaponType;
        if (type == GameSettingManager.Instance.WP.firstWeaponType)
            type = GameSettingManager.Instance.WP.secondWeaponType;
        else
            type = GameSettingManager.Instance.WP.firstWeaponType;
        if (group.Count != 0)
            for (int i = 0; i < group.Count; i++)
                SetWeapon(type, group[i].armSpr, ref group[i].bulletType, group[i].shootPoint, ref group[i].fireRate, ref group[i].spreadMax, ref group[i].spreadMin);
    }
    public void ResetUnitParameters(ref float _jumpHeight, ref float _minCommDis, ref float _spreadMax, ref float _spreadMin)
    {
        _jumpHeight = Random.Range(GameSettingManager.Instance.UP.standardUnitParameters.jumpHeightMin, GameSettingManager.Instance.UP.standardUnitParameters.jumpHeightMax);
        _minCommDis = Random.Range(GameSettingManager.Instance.UP.standardUnitParameters.minCommDisMin, GameSettingManager.Instance.UP.standardUnitParameters.minCommDisMax);

        _spreadMax = GameSettingManager.Instance.WP.weapon[GameSettingManager.Instance.WP.currentWeaponType].spreadMax;
        _spreadMin = GameSettingManager.Instance.WP.weapon[GameSettingManager.Instance.WP.currentWeaponType].spreadMin;
    }
    public void SetWeapon(int type, SpriteRenderer _weaponSpr_r, ref int _refbulletType, Transform _shootPos, ref float _reffireRate, ref float _spreadMax, ref float _spreadMin)
    {
        GameSettingManager.Instance.WP.currentWeaponType = type;
        _weaponSpr_r.sprite = GameSettingManager.Instance.WP.weapon[type].weaponSpr;
        _shootPos.localPosition = new Vector2(GameSettingManager.Instance.WP.weapon[type].shootPos.x * (int)Mathf.Sign(_shootPos.localPosition.x), GameSettingManager.Instance.WP.weapon[type].shootPos.y);
        _refbulletType = GameSettingManager.Instance.WP.weapon[type].bulletType;
        _reffireRate = GameSettingManager.Instance.WP.weapon[type].fireRate;
        _spreadMax = GameSettingManager.Instance.WP.weapon[GameSettingManager.Instance.WP.currentWeaponType].spreadMax;
        _spreadMin = GameSettingManager.Instance.WP.weapon[GameSettingManager.Instance.WP.currentWeaponType].spreadMin;
    }
   public IEnumerator WaitTimeToSpawn()
    {
        coroutineStart = true;
        yield return new WaitForSeconds(timeToSpawn);
        SpawnUnits(0);
        coroutineStart = false;
    }
}