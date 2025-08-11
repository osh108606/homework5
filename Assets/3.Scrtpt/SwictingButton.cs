using UnityEngine;


public class SwictingButton : MonoBehaviour
{
    public int clickValue;
    public void OnClickB()
    {
        Player.instance.WeaponChange(clickValue);
    }
}
