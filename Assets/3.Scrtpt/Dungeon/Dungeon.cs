using UnityEngine;
using System.Collections;
public class Dungeon : MonoBehaviour
{
    
    public string key;
    //��������
    public Transform playerSpawnPoint;
    //�����̵�����Ʈ&ù����������Ʈ
    
    public DungeonZone[] zones;
    public int zoneCount;
    //������ ����
    public void Awake()
    {
        zoneCount = 0;
        zones = GetComponentsInChildren<DungeonZone>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    public void DungeonStart()
    {
        Debug.Log("DungeonStart");
        zoneCount = zones.Length;
        zones[0].GetComponent<DungeonZone>().ZoneStart();
    }
    public void DungeonEnd()
    {
        Debug.Log("DungeonEnd");
        //����
        //���� ��Ȱ��ȭ
    }

    public void ZoneEnd()
    {
        zoneCount--;
        Debug.Log("Zone End");
        for (int i = 1; i < zones.Length; i++)
        {
            if (zones[i - 1].zoneEnd == true && zones[i].zoneEnd == false)
            {
                zones[i].ZoneStart();
                break;
            }
        }
        if (zoneCount == 0)
        {
            DungeonEnd();
        }
    }

}
