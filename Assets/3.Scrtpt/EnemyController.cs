using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyobj;
    public Transform[] points;
    
    float t = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
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
        
    }

    public void Spawn()
    {
        int currentIdx = Random.Range(0, points.Length);
        int spawnCount = 6;
        for (int i = 0; i < spawnCount; i++)
        {
            int idx = (currentIdx + i) % points.Length;
            Instantiate(enemyobj, points[idx].position, Quaternion.identity);
        }
    }
}
