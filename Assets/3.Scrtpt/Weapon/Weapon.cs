using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string key;
    public int idx;
    public float atkLmit;
    public float atkSpeed;
    public bool reLoading;
    public WeaponData weaponData;
    public UserAmmo userAmmo;
    
    public void Awake()
    {
        weaponData = Resources.Load<WeaponData>("WeaponData/"+ key);
        
        reLoading = false;
    }

    public void AmmoMatch()//다른이름 생각해보기
    {
        //총알 매치
        userAmmo = UserManager.Instance.GetUserAmmo(key); 
    }
    
    public virtual void Update()
    {
        atkSpeed += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Player.instance.currentWeapon == this && weaponData.auto == false)
        {
            Shoot();
        }
        else if (Input.GetMouseButton(0) && Player.instance.currentWeapon == this && weaponData.auto == true)
        {
            if (atkSpeed >= atkLmit)
            {
                Shoot();
                atkSpeed = 0;
            }
        }
    }

    public void Reload()
    {
        if (reLoading == true)
            return;
        StartCoroutine(CoReload());
    }
    // 여기서 부터
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
        
        if(userAmmo.count <= 0)
        {
            return false;
        }
        if (reLoading == true)
            return false;
        userAmmo.count--;
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (touchPos - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = q;
        //Debug.Log("화면클릭");
        Vector2 screenPoint = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 directtion = worldPoint - (Vector2)transform.position;



        Bullet bullet = Instantiate(weaponData.bulletPrefab);
        bullet.gameObject.transform.position = transform.position;
        bullet.Shoot(directtion.normalized, this);
        UserManager.Instance.Shooted();
        return true;
    }
}
