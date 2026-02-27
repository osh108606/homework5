using UnityEngine;

public class SG : Weapon
{
//샷건 8발씩 발사 풀링&범위 문제있음
    public override bool MouseOn()
    {
        if (!CanFire())
            return false;
        
        userWeapon.ammoCount--;//사용중인 총알

        // 기준 위치(총구/상체)
        Vector2 origin = Player.Instance.upperTransform.position;
        // 마우스 월드 좌표
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        transform.rotation = Quaternion.AngleAxis(aimAngle - 90, Vector3.forward);
        
        
        // 애니메이션
        //Player.Instance.animator.SetTrigger("Fire");
        int idx = Player.Instance.animator.GetLayerIndex("UpperAim");
        Player.Instance.animator.Play("UP_fire light front",idx,0);
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
            for (int i = 0; i < 8; i++)
            {
                Bullet bullet = Instantiate(weaponData.bulletPrefab, shotPoint.position, Quaternion.identity);
                bulletPool.Add(bullet);
                bullet.Shoot(shotDir, this);
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < bulletPool.Count; j++)
                {
                    if (bulletPool[j].gameObject.activeSelf == false)
                    {
                        bulletPool[j].gameObject.SetActive(true);
                        bulletPool[j].Shoot(shotDir, this);
                        break;
                    }
                }
            }
        }
        #endregion
        UserManager.instance.Save();
        return true;
    }
}
