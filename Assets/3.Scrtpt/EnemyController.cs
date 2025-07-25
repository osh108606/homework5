using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public GameObject enemyobj;
    public Transform[] points;
    public Transform spawnPoint;
    float t = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        //Spawn();
        StartCoroutine(Spawn());
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

    IEnumerator CoStart()
    {
        Debug.Log("CoStart() 1");
        yield return new WaitForSeconds(5);
        Debug.Log("CoStart() 2");
    }
    //points[idx].position
    IEnumerator Spawn()
    {
        int currentIdx = Random.Range(0, points.Length);
        int spawnCount = 6;
        for (int i = 0; i < spawnCount; i++)
        {
            int idx = (currentIdx + i) % points.Length;
            yield return new WaitForSeconds(1);
            GameObject obj = Instantiate(enemyobj, spawnPoint.position, Quaternion.identity);
            Enemy e = obj.GetComponent<Enemy>();
            e.desPoint = points[idx].position;
        }
    }
}
