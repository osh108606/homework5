using UnityEngine;


public class ArmorSlot : MonoBehaviour
{
    public ArmorEquipSlot armorEquipSlot;
    public Armor armor;
    public void ArmorEquip()
    {
        if (armor != null)
            Destroy(armor.gameObject);

        UserArmor userArmor = UserManager.instance.GetEquipUserArmor(armorEquipSlot);
        if (userArmor == null)
        {
            return;
        }
        Armor armorPrefab = Resources.Load<Armor>("Armor/" + userArmor.key);

        armor = Instantiate(armorPrefab, transform.position, Quaternion.identity);
        armor.transform.parent = transform;
    }
}
