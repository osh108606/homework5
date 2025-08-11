using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : Weapon
{
    public float atkLmit;
    public float atkSpeed;
    
    

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
