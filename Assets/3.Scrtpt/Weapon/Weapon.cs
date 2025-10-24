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
    public float fireInterval; // �߻� ����(��)
    public float nextFireTime; // ������ �߻�ð�
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


    public void AmmoMatch()//�ٸ��̸� �����غ���
    {
        //�Ѿ� ��ġ
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
        if(userAmmo.count <= 0) //�Ѿ� ������ �߻� �Ұ�
            return false;
        if (reLoading == true) //������ ���̸� �߻� �Ұ�
            return false;
        userAmmo.count--; //�Ѿ� ����

        // ���콺 �������� ���� ȸ��
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDir = (mouseWorld - (Vector2)transform.position).normalized;

        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(aimAngle - 90, Vector3.forward);

        transform.rotation = q;

        //Debug.Log("ȭ��Ŭ��");
        Vector2 screenPoint = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 directtion = worldPoint - (Vector2)transform.position;



        Bullet bullet = Instantiate(weaponData.bulletPrefab, transform.position, Quaternion.identity);
        bullet.Shoot(directtion.normalized, this);
        UserManager.instance.Shooted();
        return true;
    }

    
}
