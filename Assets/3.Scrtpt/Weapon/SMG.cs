using System;
using UnityEngine;

public class SMG : Weapon
{
    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0) && Time.time <= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }
}
