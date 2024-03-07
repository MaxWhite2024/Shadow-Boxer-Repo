using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructable
{
    private SpriteEffects child_sprite_effects;
    private Transform proj_spawn_trans;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenAttack = 2f;
    private float count = 0f;
    [SerializeField] private Sprite attackSprite;

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
        //is enemy is alive,
        if(health > 0)
        {
            //increment count
            count += Time.deltaTime;

            if(count >= timeBetweenAttack + child_sprite_effects.attack_duration)
            {
                //play attack animation
                child_sprite_effects.PlayAttackAnimation();

                //save previous sprite and change to attack Sprite

                //wait then shoot
                StartCoroutine(WaitThenShoot(child_sprite_effects.attack_duration));

                //reset count
                count = 0f;
            }
        }
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

    public virtual IEnumerator WaitThenShoot(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);

        Instantiate(projectilePrefab, proj_spawn_trans.position, Quaternion.identity);
    }
}
