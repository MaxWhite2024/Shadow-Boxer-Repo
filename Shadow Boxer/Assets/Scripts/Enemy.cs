using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructable
{
    [SerializeField] public SpriteEffects child_sprite_effects;
    private BoxCollider box_col;

    void Start()
    {
        if(gameObject.transform.childCount == 1)
        {
            child_sprite_effects = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteEffects>();
        }

        box_col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //overriden Take_Damage
    public override void Take_Damage()
    {
        health -= 1;

        if(health <= 0)
        {
            //play death animation
            child_sprite_effects.PlayDeathAnimation();

            //disable collider
            //gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            StartCoroutine(WaitThenDestroy(child_sprite_effects.death_duration));
        }
        else
        {
            if(child_sprite_effects)
            {
                //play damage animation
                child_sprite_effects.PlayDamageAnimation();
            }
        }

        Debug.Log("Damage!!!!!!");
    }

    public virtual IEnumerator WaitThenDestroy(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);

        Destroy_Destructable();
    } 
}
