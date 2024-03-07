using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructable
{
    [SerializeField] public SpriteEffects child_sprite_effects;

    void Start()
    {
        if(gameObject.transform.childCount == 1)
        {
            child_sprite_effects = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteEffects>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //overriden Take_Damage
    public new void Take_Damage()
    {
        health -= 1;

        if(health <= 0)
        {
            Destroy_Destructable();
        }
        else
        {
            if(gameObject.transform.childCount == 1)
            {
                //play damage animation
                child_sprite_effects.PlayDamageAnimation();
            }
        }

        Debug.Log("Damage!");
    }
}
