using System;
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
    public GameObject inventory;
    public Weapon[] weaponSlots = new Weapon[4];

    public int slotIdx = 0;
    public float mhp; //최대 체력
    public float hp; // 현재 체력
    public float moveSpeed = 0;
    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        weaponSlots = GetComponentsInChildren<Weapon>();    
    }
    private void Start()
    {
        for (int i = 0; i < weaponSlots.Length; i++)//각 슬롯에 장착된 무기데이터 넣기
        {
            weaponSlots[i].weaponData = UserManager.Instance.GetEuipedUserWeapons(i).weaponData;
            weaponSlots[i].ApplyWeaponData();
        }
        currentWeapon = weaponSlots[0];
        UserWeapon userWeapon = UserManager.Instance.GetEuipedUserWeapon();
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
            Talk();

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(slotIdx >= weaponSlots.Length)
            {
                slotIdx = 0;
                ChangeDrawWeapon(slotIdx);
            }
            else
            {
                slotIdx++;
                ChangeDrawWeapon(slotIdx);
            }
                
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (slotIdx <= 0)
            {
                slotIdx = weaponSlots.Length;
                ChangeDrawWeapon(slotIdx);
            }
            else
            {
                slotIdx--;
                ChangeDrawWeapon(slotIdx);
            }
        }
    }

    public void Talk()
    {
        
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 3);

        if (cols.Length <= 0)
        {
            return;
        }
        for (int i = 0;i < cols.Length; i++) 
        {
            if (cols[i].CompareTag("Npc"))
            {
                NPC npc = cols[i].GetComponent<NPC>();
                npc.TalkUI();
            }
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
    //무기슬롯교체
    public void ChangeDrawWeapon(int slotIdx)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            currentWeapon = weaponSlots[i].GetComponent<Weapon>();
        }

        currentWeapon.AmmoMatch();
        UserManager.Instance.ChangeDrawWeapon(currentWeapon.key);
    }
    //무기장착변경
    public void ChangeWeapon(string key)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (key == weaponSlots[i].key)
            {
                currentWeapon = weaponSlots[i].GetComponent<Weapon>();
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
        Rigidbody2D rd2d = GetComponent<Rigidbody2D>();
        Vector2 dir= new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            dir.x += 0;
            dir.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            dir.x += 0;
            dir.y += -1;            
        }

        if (Input.GetKey(KeyCode.A))
        {
            dir.x += -1;
            dir.y += 0;           
        }

        if (Input.GetKey(KeyCode.D))
        {
            dir.x += 1;
            dir.y += 0;            
        }
        rd2d.linearVelocity = dir.normalized * moveSpeed;
    }
    
}
