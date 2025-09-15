using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : Weapon
{
    public override bool Shoot()
    {
        bool result = base.Shoot(); 
        if(result == false)
        {
            return false;
        }
        Vector2 screenPoint = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 directtion = worldPoint - (Vector2)transform.position;


        float angle = 45f;
        Vector2 leftDirection = Quaternion.Euler(0, 0, angle) * directtion.normalized;
        Vector2 rightDirection = Quaternion.Euler(0, 0, -angle) * directtion.normalized;

        //directtion 방향 왼쪽45도
        Bullet bulletLeft = Instantiate(weaponData.bulletPrefab);
        bulletLeft.gameObject.transform.position = transform.position;
        bulletLeft.Shoot(leftDirection.normalized, this);
        //directtion 방향 오른쪽45도
        Bullet bulletRight = Instantiate(weaponData.bulletPrefab);
        bulletRight.gameObject.transform.position = transform.position;
        bulletRight.Shoot(rightDirection.normalized, this);
        return true;
    }
}
