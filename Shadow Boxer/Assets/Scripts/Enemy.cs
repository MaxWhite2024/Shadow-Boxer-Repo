using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Overriding Take_Damage() function from Destructable class
    public void Take_Damage()
    {
        health -= 1;

        if(health <= 0)
        {
            Destroy_Destructable();
        }

        Debug.Log("Damage!");
    }
}
