using UnityEngine;


public class AmmorSlot : MonoBehaviour
{
    public AmmorEquipSlot ammorEquipSlot;
    public Ammor ammor;
    public void AmmorEquip()
    {
        if (ammor != null)
            Destroy(ammor.gameObject);

        UserAmmor userAmmor = UserManager.instance.GetEquipUserAmmor(ammorEquipSlot);
        if (userAmmor == null)
        {
            return;
        }
        Ammor ammorPrefab = Resources.Load<Ammor>("Ammor/" + userAmmor.key);

        ammor = Instantiate(ammorPrefab, transform.position, Quaternion.identity);
        ammor.transform.parent = transform;
    }
}
