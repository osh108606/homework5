using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    HG,
    AR,
    SMG,
    MG,
    RF,
    SR,
    SG,
    SP
}

public enum WeaponSoltType
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
    public float rpm;
    public float fireInterval; // 발사 간격(초)
    public float nextFireTime; // 마지막 발사시간
    public bool reLoading;
    public bool auto;
    public bool SpreadShot;
    public WeaponData weaponData;
    public UserAmmo userAmmo;
    public WeaponType weaponType;
    public WeaponSoltType weaponSoltType;
    
    public void Awake()
    {
        weaponData = Resources.Load<WeaponData>("WeaponData/"+ key);
        reLoading = false;
        SpreadShot = weaponData.SpreadShot;
        auto = weaponData.auto;
        rpm = weaponData.RPM;
        pellets = weaponData.Pellets;
        weaponType = weaponData.weaponType;
        fireInterval = 60f / rpm;
        nextFireTime = 0f;
    }


    public void AmmoMatch()//다른이름 생각해보기
    {
        //총알 매치
        userAmmo = UserManager.instance.GetUserAmmo(weaponType); 
    }
    
    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time <= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    public void Reload()
    {
        if (reLoading == true)
            return;
        StartCoroutine(CoReload());
    }
    
    public float reloadTimer; // 재장전까지 남은 시간
    public float maxReloadTime = 3f;
    IEnumerator CoReload()
    {
        reLoading = true;
        reloadTimer = maxReloadTime;

        while (true) //reloadTimer가 0일때까지 기다리는 코드
        {
            if (reloadTimer <= 0)
                break;

            yield return null;
            reloadTimer -= Time.deltaTime;
        }

        userAmmo.count = weaponData.maxAmmo;
        reLoading = false;
    }
    
    public virtual bool Shoot()
    {
        if(userAmmo.count <= 0) //총알 없으면 발사 불가
            return false;
        if (reLoading == true) //재장전 중이면 발사 불가
            return false;
        userAmmo.count--; //총알 감소

        // 마우스 방향으로 조준 회전
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDir = (mouseWorld - (Vector2)transform.position).normalized;

        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(aimAngle - 90, Vector3.forward);

        transform.rotation = q;

        //Debug.Log("화면클릭");
        Vector2 screenPoint = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 directtion = worldPoint - (Vector2)transform.position;



        Bullet bullet = Instantiate(weaponData.bulletPrefab, transform.position, Quaternion.identity);
        bullet.Shoot(directtion.normalized, this);
        UserManager.instance.Shooted();
        return true;
    }

    
}
