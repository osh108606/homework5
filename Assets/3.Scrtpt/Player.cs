using Unity.VisualScripting;
using UnityEngine;



public class Player : MonoBehaviour
{
    
    public Rigidbody2D rb2d;
    public GameObject bulletObjectPrefab;
    public Bullet bulletPrefab;
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
    }

    
}
