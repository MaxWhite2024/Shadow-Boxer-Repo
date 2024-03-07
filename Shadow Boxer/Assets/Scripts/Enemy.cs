using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructable
{
    [SerializeField] private SpriteEffects child_sprite_effects;
    private Transform proj_spawn_trans;
    [SerializeField] private GameObject projectile_prefab;

    void Start()
    {
        int child_count = gameObject.transform.childCount;
        if(child_count > 0)
        {
            child_sprite_effects = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteEffects>();

            if(child_count == 2)
            {
                proj_spawn_trans = gameObject.transform.GetChild(1);
            }
        }
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
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

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

        //Debug.Log("Damage!!!!!!");
    }

    public virtual IEnumerator WaitThenDestroy(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);

        Destroy_Destructable();
    } 

    private void Shoot()
    {
        Instantiate(projectile_prefab, proj_spawn_trans.position, Quaternion.identity);
    }
}
