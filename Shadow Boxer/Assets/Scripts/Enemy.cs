using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructable
{
    private SpriteChanges child_sprite_changes;
    private SpriteEffects child_sprite_effects;
    private Transform proj_spawn_trans;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenAttack = 0.5f;
    [SerializeField] public float attackDuration = 0.5f;
    [SerializeField] public float deathDuration = 1.5f; 
    private float count = 0f;

    void Start()
    {
        child_sprite_changes = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteChanges>();
        child_sprite_effects = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteEffects>();
        proj_spawn_trans = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        //is enemy is alive,
        if(health > 0)
        {
            //increment count
            count += Time.deltaTime;

            if(count >= timeBetweenAttack + attackDuration)
            {
                //play attack animation
                child_sprite_effects.PlayAttackAnimation();

                //wait then shoot
                StartCoroutine(WaitThenShoot(attackDuration));

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

            StartCoroutine(WaitThenDestroy(deathDuration));
        }
        else
        {
            //play damage animation
            child_sprite_effects.PlayDamageAnimation();
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
