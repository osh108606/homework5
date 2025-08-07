using UnityEngine;

[CreateAssetMenu(fileName = "SwictingButton", menuName = "Scriptable Objects/SButton")]
public class SwictingButton : ScriptableObject
{
    
    public void OnClickB(int value)
    {
        Player.instance.WeaponChange(value);
    }
}
