using UnityEngine;

public class Ammor : MonoBehaviour
{
    public int idx;
    public AmmorData ammorData;
    public string key;

    public void Awake()
    {
        ammorData = Resources.Load<AmmorData>("EquipmentData/" + key);
    }
}
