using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public WeaponData weaponData;
    public string key;
    public int currentAmmo;
    public bool reLoading;
    // Start is called before the first frame update
    public void Start()
    {
        weaponData = Resources.Load<WeaponData>("WeaponData/"+ key);
        currentAmmo = weaponData.maxAmmo;
        reLoading = false;
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            Shoot();
            
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
            reloadTimer -= Time.deltaTime;// 
        }

        currentAmmo = weaponData.maxAmmo;
        reLoading = false;
    }
    // 여기까지 보면 수정할것
    public virtual bool Shoot()
    {
        
        if(currentAmmo <= 0)
        {
            return false;
        }
        if (reLoading == true)
            return false;
        currentAmmo--;
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
        return true;
    }
}
