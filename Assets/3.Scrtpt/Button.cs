using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button : MonoBehaviour
{
    public Player player;

    
    public void OnClickB(int value)
    {
        player.WeaponChange(value);
    }
    
}
