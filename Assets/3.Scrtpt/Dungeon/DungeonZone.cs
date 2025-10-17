using UnityEngine;
using UnityEngine.UIElements;

public class DungeonZone : MonoBehaviour, IEnemySpawner
{
    public string key;
    public Transform playerRespawnPoint;
    //���� ���� �÷��̾� ����������Ʈ
    public Transform[] enemySpawnPoint;
    //�������� ���� �� ��������Ʈ
    public DungeonWave[] dungeonWaves;


    public Enemy enemyPrefab;
    public int firstEnmey;
    public int waveCount;
    public bool zoneEnd;

    public void Awake()
    {
        zoneEnd = false;

        dungeonWaves = GetComponentsInChildren<DungeonWave>();
    }

    public void ZoneStart()
    {
        zoneEnd = false;
        waveCount = dungeonWaves.Length;
        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {

            Enemy enemy = Instantiate(enemyPrefab, enemySpawnPoint[i].transform.position, Quaternion.identity);
            enemy.transform.parent = transform;
            firstEnmey++;
        }

    }

    public void WaveEnd()
    {
        waveCount--;
        if (waveCount == 0)
        {
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
        if (firstEnmey == 0)
        {
            dungeonWaves[0].StartWave();
        }
    }

    //1. ù���̺꿡�� �ι�° ���� ���ؼ���
    //2. ������ ���̺� Ŭ���� �ߴ��� üũ - ������ Ŭ���� �α� ���
    //3. �÷��̰� �� �� ���� ����
}
