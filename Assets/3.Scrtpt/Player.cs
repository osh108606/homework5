using Unity.VisualScripting;
using UnityEngine;



public class Player : MonoBehaviour
{
    public static Player instance;
    public Rigidbody2D rb2d;
    public GameObject[] weapons;
    public Weapon currentWeapon;
    public float speed = 0;
    private void Awake()
    {
        instance = this;
        //Debug.Log("Awake");
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        WeaponChange(0);
    }
    // Update is called once per frame
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponChange(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponChange(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponChange(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            WeaponChange(3);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
        }
        Move();

    }
    public void WeaponChange(int slot)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[slot].SetActive(true);
        currentWeapon = weapons[slot].GetComponent<Weapon>();
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
