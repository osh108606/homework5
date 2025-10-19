using UnityEngine;
using UnityEngine.UIElements;

public class DungeonZone : MonoBehaviour, IEnemySpawner
{
    public string key;
    public Transform playerRespawnPoint;
    //���� ���� �÷��̾� ����������Ʈ
    public Transform[] enemySpawnPoints;
    //�������� ���� �� ��������Ʈ
    public DungeonWave[] dungeonWaves;
    //�������� ���̺��

    public Enemy enemyPrefab;
    public int firstEnmey;
    public int waveCount;
    public bool zoneEnd;

    public void Awake()
    {
        zoneEnd = false;
        firstEnmey = 0;
        dungeonWaves = GetComponentsInChildren<DungeonWave>();
        //enemySpawnPoints = GetComponentsInChildren<Transform>();
    }

    public void ZoneStart()
    {
        Debug.Log("DungeonZone Start");
        waveCount = dungeonWaves.Length;
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, enemySpawnPoints[i].transform.position, Quaternion.identity);
            enemy.transform.parent = enemySpawnPoints[i].transform;
            firstEnmey++;
        }

    }

    public void WaveEnd()
    {
        Debug.Log("WaveEnd");
        waveCount--;
        if (waveCount == 0)
        {
            zoneEnd = true;
            GetComponentInParent<Dungeon>().ZoneEnd();
        }
        for(int i = 1; i< dungeonWaves.Length; i++ )
        {
            if( dungeonWaves[i-1].waveEnd == true && dungeonWaves[i].waveEnd == false)
            {
                dungeonWaves[i].StartWave();
                break;
            }
        }
    }
    

    public void KilledEnemy(Enemy e)
    {
        firstEnmey--;
        Debug.Log(firstEnmey);
        if (firstEnmey == 0)
        {
            Debug.Log("FirstEnemy End");
            dungeonWaves[0].StartWave();
        }
    }

    //1. ù���̺꿡�� �ι�° ���� ���ؼ���
    //2. ������ ���̺� Ŭ���� �ߴ��� üũ - ������ Ŭ���� �α� ���
    //3. �÷��̰� �� �� ���� ����
}
