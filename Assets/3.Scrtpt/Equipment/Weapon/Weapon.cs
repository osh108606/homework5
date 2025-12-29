using System.Collections;
using UnityEngine;

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
    public bool SpreadShot;   
    public WeaponData weaponData;
    public UserAmmo userAmmo; 
    public UserWeapon userWeapon;
    public WeaponAbility weaponAbility;
    public WeaponType weaponType;
    public WeaponSlotType weaponSlotType;

   

    public void Awake()
    {
        weaponData = Resources.Load<WeaponData>("WeaponData/"+ key);
        weaponType = weaponData.weaponType;
        reLoading = false;
        SpreadShot = weaponData.SpreadShot;
        auto = weaponData.auto;
        rpm = weaponData.RPM;
        pellets = weaponData.Pellets;
        weaponType = weaponData.weaponType;
        maxAmmo = weaponData.maxAmmo;
        fireInterval = 60f / rpm;
        nextFireTime = 0f;
        maxReloadTime = weaponData.reloadTime;
        weaponAbility.weaponTypeDamageData.weaponType = weaponType;
        weaponAbility.weaponSubElementData.weaponSubElement = WeaponManager.Instance.GetWeaponTypeElementData(weaponType).fixWeaponSubElement;
        if(weaponAbility.grade == 0)
        {
            weaponAbility.weaponSubElementData.weaponSubElement = WeaponSubElement.Null;
            weaponAbility.weaponSubElementDatas.Clear();
            weaponAbility.weaponTelent.Clear();
        }
    }
    
    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime && Player.instance.currentWeapon == this)
        {
            Player.instance.animator.SetLayerWeight(idx, 1);
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }


    public void Equipped(UserWeapon userWeapon)
    {
        this.userWeapon = userWeapon;
        userAmmo = UserManager.instance.GetUserAmmo(weaponType);
    }

    public virtual void Reload()
    {
        if (reLoading == true)
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

            if(Player.instance.currentWeapon != this)
            {
                reLoading = false;
                yield break;
            }

            yield return null;
            reloadTimer -= Time.deltaTime;
        }
        UserManager.instance.Relord(this, userWeapon);
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
        UserManager.instance.Relord(this,userWeapon);
        userWeapon.ammoCount++;
        userAmmo.count--;

        reLoading = false;
    }

   
    public virtual bool Shoot()
    {
        if (userWeapon.ammoCount <= 0) //총알 없으면 발사 불가
            return false;

        if (reLoading == true) //재장전 중이면 발사 불가
            return false;

        userWeapon.ammoCount--;//사용중인 총알

        // 마우스 방향으로 조준 회전
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDir = (mouseWorld - (Vector2)transform.position).normalized;

        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(aimAngle - 90, Vector3.forward);

        transform.rotation = q;

        Debug.Log("화면클릭");
        Vector2 screenPoint = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 directtion = worldPoint - (Vector2)transform.position;


        //Player.instance.animator.SetTrigger("Fire");
        int idx = Player.instance.animator.GetLayerIndex("UpperAim");
        Player.instance.animator.Play("UP_fire light front",idx,0);
        Bullet bullet = Instantiate(weaponData.bulletPrefab, transform.position, Quaternion.identity);
        bullet.Shoot(directtion.normalized, this);
        UserManager.instance.Save();
        return true;
    }

    
}
