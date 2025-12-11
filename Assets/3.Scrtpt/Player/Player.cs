using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;



public class Player : MonoBehaviour
{
    public static Player instance;
    public Animator animator;
    public Rigidbody2D rb2d;
    public Weapon currentWeapon; // 현재 "들고있는" 무기
    public WeaponSlot[] weaponSlots = new WeaponSlot[4];// 무기슬롯
    public AmmorSlot[] ammorSlots = new AmmorSlot[6];
    public Ammor[] ammors;
    Camera mainCamera;
    public Transform rootTr;
    public HitBox middleHitBoxe;
    public int slotIdx;
    public float mhp; //최대 체력
    public float hp; // 현재 체력
    public float moveSpeed = 0;
    public float runSpeed = 1.2f;
    
    public bool runTrigger;
    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        //weapons = GetComponentsInChildren<Weapon>();
        weaponSlots = GetComponentsInChildren<WeaponSlot>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        runTrigger = false;
        middleHitBoxe = GetComponentInChildren<HitBox>();
        hp = mhp;
    }
    private void Start()
    {
        UpdateWeaponSlot();
        UserWeapon userWeapon = UserManager.instance.GetDrawUserWeapon();
        ChangeDrawWeapon(userWeapon.key);   
    }

    

    private void Update()
    {
        
        int idx = animator.GetLayerIndex("UpperAim");
        if (DungeonManager.instance.curDungeon != null)
        {
            animator.SetLayerWeight(idx, 1);
        }
        else
        {
            animator.SetLayerWeight(idx, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
        }
        

        if(Input.GetKey(KeyCode.LeftShift))
        {
            runTrigger = true;
        }
        else
        {
            runTrigger = false;
        }

        

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (worldPos - transform.position).normalized;
        if (dir.x > 0)
        {
            //오른쪽
            rootTr.localScale = new Vector2(1, 1);
        }
        else
        {
            //왼쪽
            rootTr.localScale = new Vector2(-1, 1);
        }
        Move();
        float angle = Vector2.Angle(transform.up,dir);
        //Debug.Log(angle);
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
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
            Talk();

        }

        //q와e로 무기슬롯 변경
        if (Input.GetKeyDown(KeyCode.E))
        {
            slotIdx++;
            if (slotIdx >= weaponSlots.Length)
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
                slotIdx = weaponSlots.Length -1;
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
        currentWeapon = weaponSlots[Idx].weapon;
        UserManager.instance.ChangeDrawWeapon(currentWeapon.key);
    }
    public void ChangeDrawWeapon(string key)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i].weapon.key == key)
            {
                currentWeapon = weaponSlots[i].weapon;
                slotIdx = i;
                break;
            }
        }
        UserManager.instance.ChangeDrawWeapon(currentWeapon.key);
    }
    //인벤토리에서 무기장착변경
    public void ChangeWeapon(string key)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (key == weaponSlots  [i].weapon.key)
            {
                currentWeapon = weaponSlots[i].weapon;
                break;
            }
        }
        
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
        float backWalk = 1f;
        if (Input.GetKey(KeyCode.A))
        {
            if(rootTr.localScale.x > 0)
            {
                backWalk = 0.3f;
            }
            dir.x += -1;
            dir.y += 0;           
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (rootTr.localScale.x < 0)
            {
                backWalk = 0.3f;
            }
            dir.x += 1;
            dir.y += 0;            
        }

        if (dir.magnitude > 0 && runTrigger == false)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
        }
        else if(dir.magnitude > 0 && runTrigger == true)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
        }
        else if (dir.magnitude <= 0)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
        }

        //rb2d.linearVelocity = dir.normalized * moveSpeed
        if (runTrigger == false)
        {
            rb2d.linearVelocity = dir.normalized * moveSpeed* backWalk;
        }//기본
        else if(runTrigger == true)
        {
            rb2d.linearVelocity = dir.normalized * moveSpeed * runSpeed* backWalk;
        }//달릴때
        
    }

    public void UpdateWeaponSlot()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            weaponSlots[i].WeaponEquip();
        }
    }
    public void TakeDamage(float damage, Collider2D collision)
    {
        HitBox hitBox = collision.GetComponent<HitBox>();
        if (hitBox == null)
        {
            return;
        }
        float damageMultiplier = hitBox.GetDamageMultiplier();
        hp -= damage * damageMultiplier;

        Debug.Log(hp);
        if (hp <= 0)
        {
            //FallDown();
            PlayerDie();
        }
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log(hp);
        if (hp <= 0)
        {
            //FallDown();
            PlayerDie();
        }
    }
    public void Rebone()
    {
        hp = mhp;
    }
    public void FallDown()
    {
        //다운 애니메이션->다운상태 대기
        //다운상태 전환 함수
    }
    public void PlayerDie()
    {
        if(DungeonManager.instance.curDungeon != null)
        {
            DungeonManager.instance.curDungeon.DungeonFail();
        }
    }
}
