using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeaponType
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

public class Weapon : MonoBehaviour
{
    public string key;
    public int idx;
    public int pellets;
    public float atkTime;
    public float atkSpeed;
    public bool reLoading;
    public bool auto;
    public bool SpreadShot;
    public WeaponData weaponData;
    public UserAmmo userAmmo;
    
    //public void Awake()
    //{
    //    weaponData = Resources.Load<WeaponData>("WeaponData/"+ key);
        
        
    //}
    public void Start()
    {   
            reLoading = false;        
    }

    public void ApplyWeaponData()
    {
        if (weaponData == null) return;
        key = weaponData.key;
        SpreadShot = weaponData.SpreadShot;
        auto = weaponData.auto;
        atkSpeed = weaponData.atkSpeed;
        pellets = weaponData.Pellets;
    }

    public void AmmoMatch()//�ٸ��̸� �����غ���
    {
        //�Ѿ� ��ġ
        userAmmo = UserManager.Instance.GetUserAmmo(key); 
    }
    
    public virtual void Update()
    {
        atkTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Player.instance.currentWeapon == this && auto == false && SpreadShot == false)
        {
            Shoot();
        }
        else if (Input.GetMouseButton(0) && Player.instance.currentWeapon == this && auto == true && SpreadShot == false)
        {
            if (atkTime >= atkSpeed)
            {
                Shoot();
                atkTime = 0;
            }
        }
        else if (Input.GetMouseButtonDown(0) && Player.instance.currentWeapon == this && auto == false && SpreadShot == true)
        {
            SpreadShoot();
        }
        else if (Input.GetMouseButton(0) && Player.instance.currentWeapon == this && auto == true && SpreadShot == true)
        {
            if (atkTime >= atkSpeed)
            {
                SpreadShoot();
                atkTime = 0;
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
        UserManager.Instance.Shooted();
        return true;
    }

    public virtual bool SpreadShoot()
    {
        if (userAmmo.count <= 0) //�Ѿ� ������ �߻� �Ұ�
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


        // ��ź �¿� ����
        float spreadAngle = 45f;
        Vector2 leftDir = (Quaternion.Euler(0, 0, spreadAngle) * aimDir).normalized;
        Vector2 rightDir = (Quaternion.Euler(0, 0, -spreadAngle) * aimDir).normalized;


        // ���� ź
        Bullet bulletLeft = Instantiate(weaponData.bulletPrefab, transform.position, Quaternion.identity);
        bulletLeft.Shoot(leftDir.normalized, this);
        // ������ ź
        Bullet bulletRight = Instantiate(weaponData.bulletPrefab, transform.position, Quaternion.identity);
        bulletRight.Shoot(rightDir.normalized, this);
        return true;

        // ���߿� pelets ���� ���� �߻� ���� �����ϴ� �ڵ�� ����
    }
}
