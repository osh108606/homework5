using System.Collections;
using UnityEngine;

public class HG : Weapon
{
    public override void Reload()
    {
        if (reLoading == true)
            return;
        if (userAmmo.count <= 0)
            return;
        if (userWeapon.ammoCount < maxAmmo)
        {
            StartCoroutine(CoReload1());
        }
        else if (userWeapon.ammoCount == maxAmmo)//일부총기 한정 약실시스템
        {
            StartCoroutine(CoReload2());
        }

        else if (userWeapon.ammoCount >= maxAmmo + 1)
            return;

    }

    public override IEnumerator CoReload1()
    {
        reLoading = true;
        reloadTimer = maxReloadTime;

        while (true) //reloadTimer가 0일때까지 기다리는 코드
        {
            if (reloadTimer <= 0)
                break;

            yield return null;
            reloadTimer -= Time.deltaTime;
        }

        userWeapon.ammoCount += (maxAmmo - userWeapon.ammoCount);
        reLoading = false;
    }
    public override IEnumerator CoReload2()
    {
        reLoading = true;
        reloadTimer = maxReloadTime / 2;

        while (true) //reloadTimer가 0일때까지 기다리는 코드
        {
            if (reloadTimer <= 0)
                break;

            yield return null;
            reloadTimer -= Time.deltaTime;
        }

        userWeapon.ammoCount++;

        reLoading = false;
    }

}
