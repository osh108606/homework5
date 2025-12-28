using UnityEngine;
using UnityEngine.Animations;



public class Player : MonoBehaviour
{
    // 기준 우선순위 1.자료형 2.보호타입
    // 자료형 순서 1.클래스 2.변수
    // 보호타입 순서 1.public 2.private 3.none
    public static Player instance;
    public Animator animator;
    public Rigidbody2D rb2d;
    public Weapon currentWeapon; // 현재 "들고있는" 무기
    public WeaponSlot[] weaponSlots = new WeaponSlot[4];// 무기슬롯
    public AmmorSlot[] ammorSlots = new AmmorSlot[6];
    public Ammor[] ammors;
    public Transform rootTr;
    public Transform upperTransform;
    Camera mainCamera;

    public int slotIdx; //무기슬롯 인덱스
    public float maxHealthPoint; //최대 체력
    public float healthPoint; // 현재 체력
    public float maxAmmorPoint; //최대 방어도
    public float ammorPoint; //현제 방어도
    public float moveSpeed = 7;
    public float runSpeed = 2f;
    public bool runTrigger;
    public bool aimTrigger;
    public bool attackTrigger;

    private float currentX = 0f;
    private float currentY = 0f;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        weaponSlots = GetComponentsInChildren<WeaponSlot>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        healthPoint = maxHealthPoint;
        ammorPoint = maxAmmorPoint;
        runTrigger = false;
        aimTrigger = false;
        attackTrigger = false;
    }

    private void Start()
    {
        UpdateWeaponSlot();
        UserWeapon userWeapon = UserManager.instance.GetDrawUserWeapon();
        ChangeDrawWeapon(userWeapon.key);   
    }

    

    private void Update()
    {
        #region Aim&Shot
        if (Input.GetMouseButton(0))//마우스 오른쪽 입력 (조준상태 전환)
        {
            attackTrigger = true;
            runTrigger = false;
        }
        else
            attackTrigger = false;

        if (Input.GetMouseButton(1))//마우스 오른쪽 입력 (조준상태 전환)
        { 
            aimTrigger = true;
            runTrigger = false;
        }
        else
            aimTrigger = false;
        #endregion

        if (Input.GetKey(KeyCode.LeftShift) && aimTrigger == false && attackTrigger == false)//왼쪽쉬프트 입력 (달리기상태 전환)
            runTrigger = true;
        else
            runTrigger = false;

        if (Input.GetKeyDown(KeyCode.R))//R키 입력 (재장전)
            currentWeapon.Reload();

        if (Input.GetKeyDown(KeyCode.I))//I키 입력 (인벤토리 on/off)
            InventoryCanvas.Instance.OpenMainInventory();
    
        if (Input.GetKeyDown(KeyCode.F))//F키 입력 (상호작용)
        {
            PickUp();
            Talk();

        }
        #region WeaponSlotCange
        //마우스 휠 (무기슬롯 변경)
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

        //숫자키 입력 (무기슬롯변경)
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
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (worldPos - upperTransform.transform.position).normalized;
        float angle = Vector2.Angle(upperTransform.transform.up, dir);
        int idx = animator.GetLayerIndex("UpperAim");

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            animator.SetLayerWeight(idx, 1);
            animator.SetFloat("Angle", angle);

            if (dir.x > 0)  // 마우스방향 //오른쪽
                rootTr.localScale = new Vector2(1, 1);
            else if (dir.x < 0)//왼쪽
                rootTr.localScale = new Vector2(-1, 1);
        }
        else
        {
            animator.SetLayerWeight(idx, 0);

            if (Input.GetKey(KeyCode.D))// 이동방향 //오른쪽
                rootTr.localScale = new Vector2(1, 1);
            else if (Input.GetKey(KeyCode.A))//왼쪽
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
            CamaraManager.Instance.EndZoom();            
        }
    }

    public void Talk()
    { 
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
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 3,LayerMask.GetMask("DropItem"));
        if (cols.Length <= 0)
            return;

        DropItem item = cols[0].GetComponent<DropItem>();
        
        if(item.itemType == ItemType.Weapon)
        {
            DropWeaponItem weaponItem = (DropWeaponItem)item;
            UserManager.instance.AddWeapon(weaponItem.key, weaponItem.grade, weaponItem);
            SaveManager.SaveData("UserData.json", UserManager.instance.userData);
        }
        else if(item.itemType == ItemType.Ammor)
        {
            DropAmmorItem ammorItem = (DropAmmorItem)item;
            UserManager.instance.AddAmmor(ammorItem.key, ammorItem.grade);
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
    public void ChangeDrawWeapon(int Idx)//장착중인 무기중 "들고있는 무기 변경"
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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        {
            float smoothX = Mathf.Lerp(currentX, x, Time.deltaTime);
            // currentX는 클래스 내부 변수로 저장해야 함
            currentX = smoothX;
            float smoothY = Mathf.Lerp(currentY, y, Time.deltaTime);
            // currentX는 클래스 내부 변수로 저장해야 함
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
        if (runTrigger == false || attackTrigger == true || aimTrigger == true)//걷기
        {
            rb2d.linearVelocity = dir.normalized * moveSpeed * backWalk;
            //animator.SetFloat("MoveSpeed", rb2d.linearVelocity.magnitude / (moveSpeed * runSpeed * backWalk));
        }
        else if (runTrigger == true && attackTrigger != true && aimTrigger != true)//달릴때
        {
            aimTrigger = false;
            rb2d.linearVelocity = dir.normalized * moveSpeed * runSpeed;
            //animator.SetFloat("MoveSpeed", rb2d.linearVelocity.magnitude / (moveSpeed * runSpeed * backWalk));
        }
        //또다른 이동 구현방식
        /*
        if (runTrigger == false)
        {
            rb2d.linearVelocity = dir * moveSpeed * backWalk;
            animator.SetFloat("MoveSpeed", rb2d.linearVelocity.magnitude / (moveSpeed * runSpeed * backWalk));
        }//기본
        else if (runTrigger == true)
        {
            rb2d.linearVelocity = dir * moveSpeed * runSpeed * backWalk;
            animator.SetFloat("MoveSpeed", rb2d.linearVelocity.magnitude / (moveSpeed * runSpeed * backWalk));
        }//달릴때
        */
    }

    public void UpdateWeaponSlot()//슬롯에 장착된 무기 업데이트
    {
        for (int i = 0; i < weaponSlots.Length; i++)
            weaponSlots[i].WeaponEquip();
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
    public void TakeDamage(float damage)// 피해를 입는 기능
    {
        if (ammorPoint <= 0)
        {
            healthPoint -= damage;
            Debug.Log(healthPoint);
        }
        else if (ammorPoint >= 0)
        { 
            ammorPoint -= damage;
            //Debug.Log(ammorPoint);
        }
        else if (ammorPoint >= 0 && ammorPoint < damage)
        { 
            healthPoint -= (damage - ammorPoint);
            ammorPoint = 0;
            Debug.Log(healthPoint);
        }

        if (healthPoint <= 0)
        {
            //FallDown();
            PlayerDie();
        }
    }
    public void Rebone()// 다운상태에서 회복
    {
        healthPoint = maxHealthPoint;
    }
    public void FallDown()// 다운상태 (미구현)
    {
        //다운 애니메이션->다운상태 대기
        //다운상태 전환 함수
    }
    public void PlayerDie()//사망
    {
        if(DungeonManager.instance.curDungeon != null)
            DungeonManager.instance.curDungeon.DungeonFail();
    }
}
