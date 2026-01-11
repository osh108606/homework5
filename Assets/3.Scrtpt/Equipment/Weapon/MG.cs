using UnityEngine;

public class MG : Weapon
{
    public override void Update()
    {
        base.Update();
        if(Input.GetMouseButton(0) && nextFireTime >= fireInterval && Player.Instance.currentWeapon == this)
        {
            Shoot();
            nextFireTime = 0;
        }
    }
}
