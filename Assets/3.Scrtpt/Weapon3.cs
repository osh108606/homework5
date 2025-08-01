using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : Weapon1
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    public override void Shoot()
    {
        Vector2 screenPoint = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 directtion = worldPoint - (Vector2)transform.position;



        Bullet bullet = Instantiate(bulletPrefab);
        bullet.gameObject.transform.position = transform.position;
        bullet.Shoot(directtion.normalized);

        float angle = 45f;
        Vector2 leftDirection = Quaternion.Euler(0, 0, angle) * directtion.normalized;
        Vector2 rightDirection = Quaternion.Euler(0, 0, -angle) * directtion.normalized;

        //directtion 방향 왼쪽45도
        Bullet bulletLeft = Instantiate(bulletPrefab);
        bulletLeft.gameObject.transform.position = transform.position;
        bulletLeft.Shoot(leftDirection.normalized);
        //directtion 방향 오른쪽45도
        Bullet bulletRight = Instantiate(bulletPrefab);
        bulletRight.gameObject.transform.position = transform.position;
        bulletRight.Shoot(rightDirection.normalized);
    }
}
