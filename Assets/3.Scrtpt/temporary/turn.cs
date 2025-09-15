using UnityEngine;

public class turn : MonoBehaviour
{
    [SerializeField] public GameObject objPrefab;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            GameObject obj = Instantiate(objPrefab);
            obj.transform.position = (Vector2)transform.position + new Vector2(0, 2.5f);
            Destroy(this.gameObject);
            Debug.Log("Ãæµ¹");
        }
    }
}
