using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public enum WeaponType
{
    AR,
    SMG,
    MG,
    RF,
    SR,
    SG,
    HG,
    SP,
    Count,
}

public enum WeaponSlotType
{
    Main,
    Sub,
    Special
}

public class Weapon : MonoBehaviour
{
    public string key;
    public int idx;
    public int pellets;
    public int maxAmmo;
    public float rpm;
    public float fireInterval; // 발사 간격(초)
    public float nextFireTime; // 마지막 발사시간
    public float reloadTimer; // 재장전까지 남은 시간
    public float maxReloadTime = 3f;
    public bool reLoading;
    public bool auto;
    public bool spreadShot;
    public Transform shotPoint;
    public WeaponData weaponData;
    public UserAmmo userAmmo; 
    public UserWeapon userWeapon;
    public WeaponAbility weaponAbility;
    public WeaponType weaponType;
    public WeaponSlotType weaponSlotType;
    public List<Bullet> bulletPool = new List<Bullet>();
    
    public float Damage
    {
        get
        {
            return weaponData.damage + weaponAbility.weaponTypeDamageData.value;
        }
    }


    public void Awake()
    {
        weaponData = Resources.Load<WeaponData>("WeaponData/"+ key);
        weaponType = weaponData.weaponType;
        reLoading = false;
        spreadShot = weaponData.spreadShot;
        auto = weaponData.auto;
        rpm = weaponData.rpm;
        pellets = weaponData.pellets;
        weaponType = weaponData.weaponType;
        maxAmmo = weaponData.maxAmmo;
        fireInterval = 60f / rpm;
        nextFireTime = 0f;
        maxReloadTime = weaponData.reloadTime;
        shotPoint = transform.Find("Weapon/ShotPoint");
    }
    
    public virtual void Update()
    {
        if (InventoryCanvas.Instance.canInteraction == false)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        nextFireTime += Time.deltaTime;
    }


    public void Equipped(UserWeapon uWeapon)
    {
        userWeapon = uWeapon;
        weaponAbility = uWeapon.weaponAbility;
        userAmmo = UserManager.instance.GetUserAmmo(weaponType);
    }

    public virtual void Reload()
    {
        if (InventoryCanvas.Instance.canInteraction == false)
            return;
        if (reLoading)
            return;
        if(userAmmo.count <= 0) 
            return;
        if (userWeapon.ammoCount < maxAmmo)
            StartCoroutine(CoReload1());
        else if (userWeapon.ammoCount == maxAmmo)//일부총기 한정 약실시스템
            StartCoroutine(CoReload2());
        else if (userWeapon.ammoCount >= maxAmmo + 1)
            return;
            
    }
    
    public virtual IEnumerator CoReload1()//탄창변경
    {
        reLoading = true;
        reloadTimer = maxReloadTime;

        while (true) //reloadTimer가 0일때까지 기다리는 코드
        {
            if (reloadTimer <= 0)
                break;

            if(Player.Instance.currentWeapon != this)
            {
                reLoading = false;
                yield break;
            }

            yield return null;
            reloadTimer -= Time.deltaTime;
        }
        UserManager.instance.Reload(this, userWeapon);
        userAmmo.count -= (maxAmmo - userWeapon.ammoCount);
        userWeapon.ammoCount += (maxAmmo - userWeapon.ammoCount);
            reLoading = false;
    }
    public virtual IEnumerator CoReload2()//약실장전
    {
        reLoading = true;
        reloadTimer = maxReloadTime/2;

        while (true) //reloadTimer가 0일때까지 기다리는 코드
        {
            if (reloadTimer <= 0)
                break;

            yield return null;
            reloadTimer -= Time.deltaTime;
        }
        UserManager.instance.Reload(this,userWeapon);
        userWeapon.ammoCount++;
        userAmmo.count--;

        reLoading = false;
    }

    public virtual bool CanFire()
    {
        if (userWeapon.ammoCount <= 0) //총알 없으면 발사 불가
            return false;
        if (reLoading == true) //재장전 중이면 발사 불가
            return false;
        if(nextFireTime < fireInterval)
            return false;
        if (Player.Instance.currentWeapon != this)
            return false;

        return true;
    }

    public virtual void MouseDown()
    {
        
    }

    public virtual bool MouseOn()
    {
        if (!CanFire())
            return false;
        
        userWeapon.ammoCount--;//사용중인 총알
        nextFireTime = 0;
        Debug.Log("Weapon_Shoot");
            
        // 기준 위치(총구/상체)
        Vector2 origin = Player.Instance.upperTransform.position;
        Vector2 mouseWorld;
        // 마우스 월드 좌표
        if(Player.Instance.aimTrigger == false)
        {
            mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            mouseWorld =CamaraManager.Instance.zoomCamera.transform.position;
        }
        // 발사/조준 방향
        Vector2 dir = (mouseWorld - origin).normalized;
        
        // 명중률(0~1). 1이면 완전 정확, 0이면 많이 퍼짐
        float accuracy = userWeapon.GetWeaponData().accuracy; // 예: 0.0~1.0
        float maxSpreadRang = 6f; //임의값
        //float maxSpreadRang = userWeapon.GetWeaponData().spreadRange; //능력치 적용시
        // 명중률 높을수록 각도 감소
        float spreadAngle = maxSpreadRang * (1f - Mathf.Clamp01(accuracy));
        float randomAngle = Random.Range(-spreadAngle, spreadAngle);
        Vector2 shotDir = Rotate2D(dir, randomAngle);
        // 반동
        float stability = userWeapon.GetWeaponData().stability;
        
        // 조준 회전 (스프라이트 기본 방향 보정이 -90인 기존 로직 유지)
        float aimAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(aimAngle - 90, Vector3.forward);
        
        #region 오브젝트풀링
        //활성상태 체크
        bool allActive = true;
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].gameObject.activeSelf == false)
            {
                allActive = false;
                break;
            }
        }
        //선후 생성
        if (bulletPool.Count <= 0 || allActive == true)
        {
            Bullet bullet = Instantiate(weaponData.bulletPrefab, shotPoint.position, Quaternion.identity);
            bulletPool.Add(bullet);
            bullet.Shoot(shotDir, this);
            Debug.Log("총알 새로 생성");
        }
        else
        {
            for (int i = 0; i < bulletPool.Count; i++)
            {
                if (bulletPool[i].gameObject.activeSelf == false)
                {
                    bulletPool[i].gameObject.SetActive(true);
                    bulletPool[i].transform.position = shotPoint.position;
                    bulletPool[i].Shoot(shotDir, this);
                    break;
                }
            }
        }
        #endregion
        
        UserManager.instance.Save();
        return true;
    }

    public virtual void MouseUp()
    {
        
    }
    
    public Vector2 Rotate2D(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}
