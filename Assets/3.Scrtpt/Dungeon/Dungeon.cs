using UnityEngine;
using System.Collections;
public class Dungeon : MonoBehaviour
{
    public string key;
    //��������
    public Transform playerSpawnPoint;
    //�����̵�����Ʈ&ù����������Ʈ
    
    public GameObject[] zone;
    //������ ����
    public void OnTriggerEnter2D(Collider2D collision)
    {
        DungeonStart();
    }

    public void DungeonStart()
    {
        for (int i = 0; i < zone.Length; i++)
        {
            zone[i].GetComponent<DungeonZone>().ZoneStart();
        }
        
    }
    public void DungeonEnd()
    {

    }
}
