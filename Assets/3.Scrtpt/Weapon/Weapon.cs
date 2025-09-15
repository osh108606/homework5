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

    public void AmmoMatch()//�ٸ��̸� �����غ���
    {
        //�Ѿ� ��ġ
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
    // ���⼭ ����
    public float reloadTimer; // ���������� ���� �ð�
    public float maxReloadTime = 3f;
    IEnumerator CoReload()
    {
        reLoading = true;
        reloadTimer = maxReloadTime;

        while (true) //reloadTimer�� 0�϶����� ��ٸ��� �ڵ�
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
        //Debug.Log("ȭ��Ŭ��");
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
