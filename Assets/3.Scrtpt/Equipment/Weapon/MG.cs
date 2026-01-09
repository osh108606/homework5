using UnityEngine;

public class MG : Weapon
{
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && Player.Instance.currentWeapon == this)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }
}
