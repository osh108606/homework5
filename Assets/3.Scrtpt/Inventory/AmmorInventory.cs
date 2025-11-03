using UnityEngine;

public class AmmorInventory : SubInventory
{
    public void AmmorSelected(AmmorData ammorData)
    {
        if (ammorData == null)
        {
            curEuiptmentNameText.enabled = false;
            curEuiptmentImage.enabled = false;
            return;
        }

        UserManager.instance.ChangeWeapon(ammorData.key);
        Player.instance.ChangeWeapon(ammorData.key);



        curEuiptmentNameText.enabled = true;
        curEuiptmentImage.enabled = true;

        curEuiptmentNameText.text = ammorData.ammorName;
        curEuiptmentImage.sprite = ammorData.sprite;
    }
}
