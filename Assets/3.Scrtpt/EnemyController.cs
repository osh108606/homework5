using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Enemyobj;
    float t = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t+=Time.deltaTime;
        Debug.Log(t);
        if(t>5f)
        {
            for(int i=0; i<5; i++)
            {
                Vector2 position = new Vector2(-6f+3*i, 11);
                Debug.Log(position);


                Instantiate(Enemyobj, position,Quaternion.identity);
            }
            
            t = 0;
        }   
    }
}
