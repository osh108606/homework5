using UnityEngine;
using System.Collections;
public class Dungeon : MonoBehaviour
{
    public string key;
    //던전구분
    public Transform playerSpawnPoint;
    //던전이동포인트&첫리스폰포인트
    
    public GameObject[] zone;
    //던전내 구역
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
