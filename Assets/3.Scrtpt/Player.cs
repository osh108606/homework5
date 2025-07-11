using Unity.VisualScripting;
using UnityEngine;



public class Player : MonoBehaviour
{
    
    public Rigidbody2D rb2d;
    public GameObject bulletObjectPrefab;
    public Bullet bulletPrefab;
    public float speed = 0;
    private void Awake()
    {
        //Debug.Log("Awake");
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //Debug.Log("Start");
    }
    // Update is called once per frame
    

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (touchPos - (Vector2)transform.position).normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            transform.rotation = q;
            //Debug.Log("화면클릭");
            Vector2 screenPoint = Input.mousePosition;
            Vector2 worldPoint= Camera.main.ScreenToWorldPoint(screenPoint); ;
            Vector2 directtion = worldPoint - (Vector2)transform.position;


            //Debug.Log(screenPoint);
            //Debug.Log(worldPoint);

            Bullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.transform.position = transform.position;
            bullet.Shoot(directtion.normalized);
        }

        Move();
    }
    public void Move()
    {
        Vector2 dir= new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            dir.x = 0;
            dir.y = 1;
            transform.position += (Vector3)dir* speed *Time.deltaTime;
            Debug.Log(dir);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.x = 0;
            dir.y = -1;
            transform.position = transform.position + (Vector3)dir * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            dir.y = 0;
            transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            dir.y = 0;
            transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
    }
    
}
