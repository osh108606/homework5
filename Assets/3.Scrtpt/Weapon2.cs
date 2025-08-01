using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : Weapon1
{
    public float atkLmit;
    public float atkSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        atkSpeed += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if(atkSpeed >= atkLmit)
            {
                Shoot();
                atkSpeed = 0;
            }
            
        }
    }
    
}
