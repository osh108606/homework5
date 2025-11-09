using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;



public class Player : MonoBehaviour
{
    public static Player instance;
    public Rigidbody2D rb2d;
    public Weapon currentWeapon; // 현재 "들고있는" 무기
    public WeaponSlot[] slots = new WeaponSlot[4];// 무기슬롯
    public Ammor[] ammors;
    

    public int slotIdx;
    public float mhp; //최대 체력
    public float hp; // 현재 체력
    public float moveSpeed = 0;
    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        //weapons = GetComponentsInChildren<Weapon>();
        slots = GetComponentsInChildren<WeaponSlot>();
    }
    private void Start()
    {
        UpdateSlot();
        UserWeapon userWeapon = UserManager.instance.GetDrawUserWeapon();
        ChangeDrawWeapon(userWeapon.key);   
    }

    

    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
        }
        Move();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryCanvas.Instance.gameObject.activeSelf == false)
            {
                InventoryCanvas.Instance.OpenMainInventory();
            }
            else
            {
                InventoryCanvas.Instance.OpenMainInventory();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ///인벤토리 관련
            if (InventoryCanvas.Instance.mainInventory.gameObject.activeSelf == true)
                InventoryCanvas.Instance.mainInventory.Close();

            if (InventoryCanvas.Instance.weaponInventory.gameObject.activeSelf == true)
            {
                InventoryCanvas.Instance.weaponInventory.Close();
                InventoryCanvas.Instance.mainInventory.Open();
            }
            if (InventoryCanvas.Instance.ammorInventory.gameObject.activeSelf == true)
            {
                InventoryCanvas.Instance.ammorInventory.Close();
                InventoryCanvas.Instance.mainInventory.Open();
            }

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
            Talk();

        }

        //q와e로 무기슬롯 변경
        if (Input.GetKeyDown(KeyCode.E))
        {
            slotIdx++;
            if (slotIdx >= slots.Length)
            {
                slotIdx = 0;
            }
            ChangeDrawWeapon(slotIdx);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            slotIdx--;
            if (slotIdx < 0)
            {
                slotIdx = slots.Length -1;
            }
            ChangeDrawWeapon(slotIdx);
        }
        //숫자키 무기슬롯변경
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeDrawWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeDrawWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeDrawWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeDrawWeapon(3);
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
                StageSelectNpc stageSelectnNpc = cols[i].GetComponent<StageSelectNpc>();
                stageSelectnNpc.TalkUI();
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
        if(item.itemType == ItemType.Weapon)
        {
            UserManager.instance.AddWeapon(item.key);
            SaveManager.SaveData("UserData.json", UserManager.instance.userData);
        }
        else if(item.itemType == ItemType.Ammor)
        {
            UserManager.instance.AddWeapon(item.key);
            SaveManager.SaveData("UserData.json", UserManager.instance.userData);
        }
        else if (item.itemType == ItemType.Consume)
        {
            UserManager.instance.Additem(item.key);
            SaveManager.SaveData("UserData.json", UserManager.instance.userData);
        }
            
        Destroy(item.gameObject);
    }
    //무기슬롯교체
    public void ChangeDrawWeapon(int Idx)
    {
        currentWeapon = slots[Idx].weapon;
        currentWeapon.AmmoMatch();
        UserManager.instance.ChangeDrawWeapon(currentWeapon.key);
    }
    public void ChangeDrawWeapon(string key)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].weapon.key == key)
            {
                currentWeapon = slots[i].weapon;
                slotIdx = i;
                break;
            }
        }
        currentWeapon.AmmoMatch();
        UserManager.instance.ChangeDrawWeapon(currentWeapon.key);
    }
    //인벤토리에서 무기장착변경
    public void ChangeWeapon(string key)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (key == slots[i].weapon.key)
            {
                currentWeapon = slots[i].weapon;
                break;
            }
        }
        
        currentWeapon.AmmoMatch();
        UserManager.instance.ChangeWeapon(currentWeapon.key);
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
        rb2d.linearVelocity = dir.normalized * moveSpeed;
    }

    public void UpdateSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].WeaponEquip();
        }
    }
    public void TakeDamage(float damage)
    {

        hp -= damage;
        Debug.Log(hp);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
