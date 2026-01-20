using UnityEngine;



public class Player : MonoSingleton<Player>
{
    // ???? ???????? 1.?????? 2.????????
    // ?????? ???? 1.?????? 2.????
    // ???????? ???? 1.public 2.private 3.none
    public PlayerAbility playerAbility;
    public Animator animator;
    public Rigidbody2D rb2d;
    public Weapon currentWeapon; // ???? "????????" ????
    public WeaponSlot[] weaponSlots = new WeaponSlot[4];// ????????
    public ArmorSlot[] armorSlots = new ArmorSlot[6];
    public Armor[] armors;
    public Transform rootTr;
    public Transform upperTransform;
    Camera mainCamera;

    public int slotIdx; //???????? ??????
    float maxHealthPoint; //???? ????
    float healthPoint; // ???? ????
    float maxArmorPoint; //???? ??????
    float armorPoint; //???? ??????

    public bool runTrigger;
    public bool aimTrigger;
    public bool attackTrigger;

    private float currentX = 0f;
    private float currentY = 0f;

    public override void Awake()
    {
        base.Awake();
        playerAbility = new PlayerAbility();
        rb2d = GetComponent<Rigidbody2D>();
        weaponSlots = GetComponentsInChildren<WeaponSlot>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        maxHealthPoint = playerAbility.initHealthPoint;
        maxArmorPoint = playerAbility.initArmorPoint;
        healthPoint = maxHealthPoint;
        armorPoint = maxArmorPoint;
        runTrigger = false;
        aimTrigger = false;
        attackTrigger = false;
        playerAbility.Init();
    }

    private void Start()
    {
        UpdateWeaponSlot();
        UserWeapon userWeapon = UserManager.instance.GetDrawUserWeapon();
        ChangeWeapon(userWeapon.key, userWeapon.weaponDraw);   
    }
    



    private void Update()
    {
        #region Aim&Shot
        if (Input.GetMouseButton(0))//?????? ?????? ???? (???????? ????)
        {
            attackTrigger = true;
            runTrigger = false;
        }
        else
            attackTrigger = false;

        if (Input.GetMouseButton(1))//?????? ?????? ???? (???????? ????)
        { 
            aimTrigger = true;
            runTrigger = false;
        }
        else
            aimTrigger = false;
        #endregion

        if (Input.GetKey(KeyCode.LeftShift) && aimTrigger == false && attackTrigger == false
            && InventoryCanvas.Instance.canInteraction == false)//?????????? ???? (?????????? ????)
            runTrigger = true;
        else
            runTrigger = false;

        if (Input.GetKeyDown(KeyCode.R))//R?? ???? (??????)
            currentWeapon.Reload();

        if (Input.GetKeyDown(KeyCode.I))//I?? ???? (???????? on/off)
            InventoryCanvas.Instance.OpenMainInventory();
    
        if (Input.GetKeyDown(KeyCode.F))//F?? ???? (????????)
        {
            PickUp();
            Talk();

        }
        #region WeaponSlotCange
        //?????? ?? (???????? ????)
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            slotIdx++;

            if (slotIdx >= weaponSlots.Length)
                slotIdx = 0;

            ChangeDrawWeapon(slotIdx);
        }
        else if (wheelInput < 0)
        {
            slotIdx--;

            if (slotIdx < 0)
                slotIdx = weaponSlots.Length - 1;

            ChangeDrawWeapon(slotIdx);
        }

        //?????? ???? (????????????)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeDrawWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeDrawWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeDrawWeapon(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeDrawWeapon(3);
        #endregion

        Fire();
        Move();
    }
   
    public virtual void Fire()
    {
        if (InventoryCanvas.Instance.canInteraction == false)
            return;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (worldPos - upperTransform.transform.position).normalized;
        float angle = Vector2.Angle(upperTransform.transform.up, dir);
        int idx = animator.GetLayerIndex("UpperAim");

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            animator.SetLayerWeight(idx, 1);
            animator.SetFloat("Angle", angle);

            if (dir.x > 0)  // ?????????? //??????
                rootTr.localScale = new Vector2(1, 1);
            else if (dir.x < 0)//????
                rootTr.localScale = new Vector2(-1, 1);
        }
        else
        {
            animator.SetLayerWeight(idx, 0);

            if (Input.GetKey(KeyCode.D))// ???????? //??????
                rootTr.localScale = new Vector2(1, 1);
            else if (Input.GetKey(KeyCode.A))//????
                rootTr.localScale = new Vector2(-1, 1);
        }

        if (aimTrigger == true)
        {
            animator.SetBool("Aim",true);
            CamaraManager.Instance.StartZoom();        
        }
        else if (aimTrigger == false)
        {
            animator.SetBool("Aim", false);
            //CamaraManager.Instance.EndZoom();            
        }
    }

    public void Talk()
    {
        if (InventoryCanvas.Instance.canInteraction == false)
            return;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 3);

        if (cols.Length <= 0)
            return;

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
        if (InventoryCanvas.Instance.canInteraction == false)
            return;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 3,LayerMask.GetMask("DropItem"));

        if (cols.Length <= 0)
            return;

        DropItem item = cols[0].GetComponent<DropItem>();
        AmmoBox aBox = cols[0].GetComponent<AmmoBox>();
        if(item.itemType == ItemType.Weapon)
        {
            DropWeaponItem weaponItem = (DropWeaponItem)item;
            UserManager.instance.AddWeapon(weaponItem.key, weaponItem.grade, weaponItem);
            SaveManager.SaveData("UserData.json", UserManager.instance.userData);
        }
        else if(item.itemType == ItemType.Armor)
        {
            DropArmorItem armorItem = (DropArmorItem)item;
            UserManager.instance.AddArmor(armorItem.key, armorItem.grade);
            SaveManager.SaveData("UserData.json", UserManager.instance.userData);
        }
        else if (item.itemType == ItemType.Consume)
        {
            UserManager.instance.Additem(item.key);
            SaveManager.SaveData("UserData.json", UserManager.instance.userData);
        }

        if (aBox != null)
        {
            aBox.GetAmmo();
            SaveManager.SaveData("UserData.json", UserManager.instance.userData);
        }
        if (item.isConsume == true)
            Destroy(item.gameObject);
    }
    //????????????
    public void ChangeDrawWeapon(int Idx)//???????? ?????? "???????? ???? ????"
    {
        WeaponEquipSlot slot = (WeaponEquipSlot)Idx;
        UserWeapon userWeapon = UserManager.instance.GetEquipUserWeapon(slot);

        currentWeapon = weaponSlots[Idx].weapon;
    }
    //???????????? ????????????
    public void ChangeWeapon(string key , bool draw)
    {  
        if(draw == true) 
        {
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                if (weaponSlots[i].weapon.key == key)
                {
                    currentWeapon = weaponSlots[i].weapon;
                    playerAbility.SetWeapon(currentWeapon);
                    slotIdx = i;
                    break;
                }
            }
        }
    }

    public void ChangeEquipment(string key)
    {

        //UserManager.Instance.ChangeEquipment(equipments[i].key);
    }
    

    
    public void Move()
    {
        if (InventoryCanvas.Instance.canInteraction == false)
            return;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        {
            float smoothX = Mathf.Lerp(currentX, x, Time.deltaTime);
            // currentX?? ?????? ???? ?????? ???????? ??
            currentX = smoothX;
            float smoothY = Mathf.Lerp(currentY, y, Time.deltaTime);
            // currentX?? ?????? ???? ?????? ???????? ??
            currentY = smoothY;
            //Vector2 dir = new Vector2(smoothX, smoothY);
        }
        Vector2 dir = new Vector2(x, y);
        
        float backWalk = 1f;
        if (Input.GetKey(KeyCode.A))
        {
            if ((attackTrigger == true || aimTrigger == true) && rootTr.localScale.x > 0)
                backWalk = 0.4f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if ((attackTrigger == true || aimTrigger == true) && rootTr.localScale.x < 0)
                backWalk = 0.4f;
        }


        if (dir.magnitude > 0.0001f && runTrigger)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
        }
        else if (dir.magnitude > 0.0001f)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
        }
        else if (dir.magnitude <= 0)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
        }

        if (dir.magnitude>1)
        {
            dir.Normalize();
        }
        //rb2d.linearVelocity = dir.normalized * moveSpeed
        if (runTrigger == false || attackTrigger == true || aimTrigger == true)//????
        {
            rb2d.linearVelocity = dir.normalized * playerAbility.initMoveSpeed * backWalk;
            //animator.SetFloat("MoveSpeed", rb2d.linearVelocity.magnitude / (moveSpeed * runSpeed * backWalk));
        }
        else if (runTrigger == true && attackTrigger != true && aimTrigger != true)//??????
        {
            aimTrigger = false;
            rb2d.linearVelocity = dir.normalized * playerAbility.initMoveSpeed * playerAbility.initRunSpeed;
            //animator.SetFloat("MoveSpeed", rb2d.linearVelocity.magnitude / (moveSpeed * runSpeed * backWalk));
        }
        //?????? ???? ????????
        /*
        if (runTrigger == false)
        {
            rb2d.linearVelocity = dir * moveSpeed * backWalk;
            animator.SetFloat("MoveSpeed", rb2d.linearVelocity.magnitude / (moveSpeed * runSpeed * backWalk));
        }//????
        else if (runTrigger == true)
        {
            rb2d.linearVelocity = dir * moveSpeed * runSpeed * backWalk;
            animator.SetFloat("MoveSpeed", rb2d.linearVelocity.magnitude / (moveSpeed * runSpeed * backWalk));
        }//??????
        */
    }

    public void UpdateWeaponSlot()//?????? ?????? ???? ????????
    {
        for (int i = 0; i < weaponSlots.Length; i++)
            weaponSlots[i].WeaponEquip();
    }

    public void UpdateWeaponSlot(WeaponEquipSlot weaponEquipSlot)//?????? ?????? ???? ????????
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if(weaponSlots[i].weaponEquipSlot == weaponEquipSlot)
            {
                weaponSlots[i].WeaponEquip();
                return;
            }
        }
    }

    public void TakeDamage(float damage, HitBox hitBox)
    {
        if (hitBox == null)
            return;

        float damageMultiplier = hitBox.GetDamageMultiplier();
        healthPoint -= damage * damageMultiplier;

        Debug.Log(healthPoint);
        if (healthPoint <= 0)
        {
            //FallDown();
            PlayerDie();
        }
    }
    public void TakeDamage(float damage)// ?????? ???? ????
    {
        if (armorPoint >= 0)
        {
            armorPoint -= damage;
            damage = 0;
            //Debug.Log(armorPoint);
        }
        else if (damage > armorPoint)
        {
            damage -= armorPoint;
            armorPoint = 0;           
        }
        healthPoint -= damage;
        Debug.Log(healthPoint);

        if (healthPoint <= 0)
        {
            //FallDown();
            PlayerDie();
        }
    }
    public void Rebone()// ???????????? ????
    {
        healthPoint = maxHealthPoint;
    }
    public void FallDown()// ???????? (??????)
    {
        //???? ??????????->???????? ????
        //???????? ???? ????
    }
    public void PlayerDie()//????
    {
        if(DungeonManager.instance.curDungeon != null)
            DungeonManager.instance.curDungeon.DungeonFail();
    }
}

[System.Serializable]
public class PlayerAbility
{
    public float Damage
    {
        get
        {
            return curWeapon.Damage;
        }
    }

    public float CrtChance
    {
        get
        {
            return initCrtChance + curWeapon.weaponAbility.GetValue(WeaponSubElement.CriticalChance);
        }
    }
    
    public float crtDamage
    {
        get
        {
            return initCrtDamage + curWeapon.weaponAbility.GetValue(WeaponSubElement.CriticalDamage);
        }
    }
    //????-??????
    public float initWeaponDamage;
    public float initCrtChance;
    public float initCrtDamage;
    public float initHealthPointDamage;
    public float initArmorPointDamage;
    public float initPrecisionDamage; //???????????? ????
    public float initArmorPlateDamage;
    public float initWeakPointDamage;
    public float initUnCoverDamage;
    //????-????
    public float initReload;
    public float initAccuracy;
    public float initRecoil;
    //????
    public float initHealthPoint;
    public float initArmorPoint;    
    //????
    public float initMoveSpeed;
    public float initRunSpeed;
    public float initInvincibleTime;
    //????????
    //????
    public int initHGAmmoLimit;
    public int initARAmmoLimit;
    public int initSMGAmmoLimit;
    public int initMGAmmoLimit;
    public int initRFAmmoLimit;
    public int initSGAmmoLimit;
    public int initSRAmmoLimit;
    public int initSPAmmoLimit;
    public void Init()
    {
        //????-??????
        initWeaponDamage = 0;// ????damage?? ???? damage?? ???? ??????
        initHealthPoint = 100;
        initArmorPoint = 100;
        initCrtChance = 1f;
        initCrtDamage = 1.5f;
        initHealthPointDamage = 0;
        initArmorPointDamage = 0;
        initPrecisionDamage = 1.5f; //???????????? ????
        initArmorPlateDamage = 0;
        initWeakPointDamage = 0;
        initUnCoverDamage = 0;
        //????-????
        initReload = 0;
        initAccuracy = 0;
        initRecoil = 0;
        //????
        initHealthPoint = 100;
        initArmorPoint = 100;
        //????
        initMoveSpeed = 7;
        initRunSpeed = 2;
        //????

        initHGAmmoLimit = 100;
        initARAmmoLimit = 600; //30x20
        initSMGAmmoLimit = 600; //30x20
        initMGAmmoLimit = 2000; //100x20
        initRFAmmoLimit = 200; //10x20
        initSGAmmoLimit = 160; //8x20
        initSRAmmoLimit = 100; //5x20
        initSPAmmoLimit = 24;
    }

    public Weapon curWeapon;
    public void SetWeapon(Weapon weapon)
    {
        curWeapon = weapon;
    }
}