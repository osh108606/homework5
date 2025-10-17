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
        zones = GetComponentsInChildren<DungeonZone>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    public void Update()
    {
        if (zoneCount == 0)
        {
            DungeonEnd();
        }
    }
    public void DungeonStart()
    {
        zoneCount = zones.Length;
        zones[0].GetComponent<DungeonZone>().ZoneStart();
    }
    public void DungeonEnd()
    {
        //����
        //���� ��Ȱ��ȭ
    }

    public void ZoneEnd()
    {
        zoneCount--;
    }

}
