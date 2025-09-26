using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;



public class Player : MonoBehaviour
{
    public static Player instance;
    public Rigidbody2D rb2d;
    public Weapon[] weapons;
    public Weapon currentWeapon;
    public Equipment[] equipments;
    public float speed = 0;
    public GameObject inventory;
    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        UserWeapon userWeapon = UserManager.Instance.GetCurrentUserWeapon();
        ChangeWeapon(userWeapon.key);
        inventory.SetActive(false);
    }
    
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeWeapon(4);
        }
        */
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
        }
        Move();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf == true)
            {
                inventory.SetActive(false);
            }
            else
            {
                inventory.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
        }
            
    }

    public void PickUp()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 3,LayerMask.GetMask("DropItem"));
        if (cols.Length <= 0)
        {
            return;
        }
        
        
        DropItem item = cols[0].GetComponent<DropItem>();
        
        UserManager.Instance.Additem(item.key);
        Destroy(item.gameObject);
    }

    public void ChangeWeapon(string key)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (key == weapons[i].key)
            {
                currentWeapon = weapons[i].GetComponent<Weapon>();
                break;
            }
        }
        
        currentWeapon.AmmoMatch();
        UserManager.Instance.ChangeWeapon(currentWeapon.key);
    }

    public void ChangeEquipment(string key)
    {

        //UserManager.Instance.ChangeEquipment(equipments[i].key);
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
