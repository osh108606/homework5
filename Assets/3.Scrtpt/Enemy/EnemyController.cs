using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public GameObject enemyobj;
    public Transform[] points;
    public Transform spawnPoint;
    
    public int spawnCount=6;
    public int enemiesCount=0;
    //float t = 0f;
    public Enemy[] enemies;
    public bool spawnAll = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //t+=Time.deltaTime;

        //if(t>5f)
        //{
        //    for(int i=0; i<5; i++)
        //    {
        //        Vector2 position = new Vector2(-6f+3*i, 11);
        //        Debug.Log(position);


        //        Instantiate(enemyobj, position,Quaternion.identity);
        //    }

        //    t = 0;
        //}

        // 적 개수가 0일때
        if(enemiesCount == 0)
        {
            StartCoroutine(Spawn());
        }
        
        if (spawnAll == true)
        {
            cheakArrive();
        }
        
    }

    IEnumerator CoStart()
    {
        Debug.Log("CoStart() 1");
        yield return new WaitForSeconds(5);
        Debug.Log("CoStart() 2");
    }
    //points[idx].position
    
    IEnumerator Spawn()
    {
        spawnAll = false;
        int currentIdx = Random.Range(0, points.Length);
        enemies = new Enemy[spawnCount];
        for (int i = 0; i < spawnCount; i++)
        {
            int idx = (currentIdx + i) % points.Length;
            enemiesCount++;
            yield return new WaitForSeconds(1);
            
            GameObject obj = Instantiate(enemyobj, spawnPoint.position, Quaternion.identity);
            
            enemies[i] = obj.GetComponent<Enemy>();
            enemies[i].desPoint = points[idx].position;
        }
        spawnAll = true;
    }
    public int cheak =0;

    void cheakArrive()
    {
        bool arrivedAll = true;
        for (int i = 0; i < spawnCount; i++)
        {
            if (enemies[i] != null && enemies[i].arrived == false)
            {
                arrivedAll = false;
                break;
            }   
        }

        if(arrivedAll == true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                if(enemies[i] != null)
                {
                    enemies[i].Move();
                }
            }
        }
        cheak = 0;
    }
    
}
