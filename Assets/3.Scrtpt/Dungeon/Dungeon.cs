using UnityEngine;
using System.Collections;
public class Dungeon : MonoBehaviour
{
    public string key;
    //��������
    public Transform playerSpawnPoint;
    //�����̵�����Ʈ&ù����������Ʈ
    
    public DungeonZone[] zones;
    //������ ����
    public void Awake()
    {
        zones = GetComponentsInChildren<DungeonZone>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    public void DungeonStart()
    {
        zones[0].GetComponent<DungeonZone>().ZoneStart();
    }
    public void DungeonEnd()
    {

    }
}
