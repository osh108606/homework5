using UnityEngine;

public class SG : Weapon
{
    public override void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time <= nextFireTime)
        {
            SpreadShoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    public virtual bool SpreadShoot()
    {
        if (userWeapon.ammoCount <= 0) //총알 없으면 발사 불가
        {
            return false;
        }
        if (reLoading) //재장전 중이면 발사 불가
            return false;
        userWeapon.ammoCount--;//사용중인 총알

        // 마우스 방향으로 조준 회전
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDir = (mouseWorld - (Vector2)transform.position).normalized;

        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(aimAngle - 90, Vector3.forward);

        transform.rotation = q;

        //Debug.Log("화면클릭");
        Vector2 screenPoint = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 direction = worldPoint - (Vector2)transform.position;


        // 산탄 좌우 각도
        float spreadAngle = 45f;
        Vector2 leftDir = (Quaternion.Euler(0, 0, spreadAngle) * aimDir).normalized;
        Vector2 rightDir = (Quaternion.Euler(0, 0, -spreadAngle) * aimDir).normalized;

        Player.Instance.animator.SetTrigger("Fire");
        // 왼쪽 탄
        Bullet bulletLeft = Instantiate(weaponData.bulletPrefab, transform.position, Quaternion.identity);
        bulletLeft.Shoot(leftDir.normalized, this);
        // 오른쪽 탄
        Bullet bulletRight = Instantiate(weaponData.bulletPrefab, transform.position, Quaternion.identity);
        bulletRight.Shoot(rightDir.normalized, this);
        return true;

        // 나중에 pelets 수에 따라 발사 방향 조정하는 코드로 변경
    }
}
