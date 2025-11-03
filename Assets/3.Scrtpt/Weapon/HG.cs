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
        if (currentAmmo < maxAmmo)
        {
            StartCoroutine(CoReload1());
        }
        else if (currentAmmo == maxAmmo)//일부총기 한정 약실시스템
        {
            StartCoroutine(CoReload2());
        }

        else if (currentAmmo >= maxAmmo + 1)
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

        currentAmmo += (maxAmmo - currentAmmo);
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

        currentAmmo++;

        reLoading = false;
    }

}
