using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject weaponPanelPrefab;
    public GameObject weaponList;
    public List<GameObject> weaponPanels;
    private void OnEnable()
    {
        
        for(int i = 0; i < weaponPanels.Count; i++)
        {
            Destroy(weaponPanels[i]);
        }
        weaponPanels.Clear();
        
        for (int i = 0; i < UserManager.Instance.userData.userWeapons.Count; i++)
        {
            GameObject panel = Instantiate(weaponPanelPrefab, weaponList.transform);

        }
           
    }
}
