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
