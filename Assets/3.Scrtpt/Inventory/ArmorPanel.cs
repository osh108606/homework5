using UnityEngine;

public class ArmorPanel : GearPanel
{
    public UserArmor userArmor;
    public ArmorData armorData;
    public ArmorEquipSlot armorEquipSlot;

    public override void SetData(UserArmor userArmor)
    {
        armorData = Resources.Load<ArmorData>("ArmorData/" + userArmor.key);
        text.text = armorData.armorName;
        image.sprite = armorData.sprite;
        armorEquipSlot = armorData.armorEquipSlot;


        this.userArmor = userArmor;
        if (userArmor.armorEuiped == true)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.black;
        }
    }
    public override void OnClicked()
    {
        //1번클릭했을시 선택상태
        //if (select == false)
        //{
        //    select = true;
        //    GetComponentInParent<ArmorInventory>().ArmorSelected(armorData);
        //}
        //else
        //{
        //    select = false;
        //    GetComponentInParent<ArmorInventory>().ArmorSelected(null);
        //}

    }
    public void OnClickedRemove()
    {
        Debug.Log("작동됨");
        if (userArmor.armorEuiped == false)
        {
            UserManager.instance.RemoveArmor(userArmor);
            Destroy(this.gameObject);
        }
        else
        {

        }


    }
}
