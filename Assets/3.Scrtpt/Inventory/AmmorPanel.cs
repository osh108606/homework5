using UnityEngine;

public class AmmorPanel : GearPanel
{
    public UserAmmor userAmmor;
    public AmmorData ammorData;

    public override void SetData(UserAmmor userAmmor)
    {
        ammorData = Resources.Load<AmmorData>("AmmorData/" + userAmmor.key);
        text.text = ammorData.ammorName;
        image.sprite = ammorData.sprite;



        this.userAmmor = userAmmor;
        if (userAmmor.ammorEuiped == true)
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
        if (select == false)
        {
            select = true;
            GetComponentInParent<AmmorInventory>().AmmorSelected(ammorData);
        }
        else
        {
            select = false;
            GetComponentInParent<AmmorInventory>().AmmorSelected(null);
        }

    }
    public void OnClickedRemove()
    {
        Debug.Log("작동됨");
        if (userAmmor.ammorEuiped == false)
        {
            UserManager.instance.RemoveAmmor(userAmmor);
            Destroy(this.gameObject);
        }
        else
        {

        }


    }
}
