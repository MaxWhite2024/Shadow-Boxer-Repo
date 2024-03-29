using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructable
{
    private enum Direction_Type
    {
        NONE, RIGHT, LEFT, TOWARDS, AWAY
    }

    //component vars
    private SpriteEffects child_sprite_effects;
    private Transform proj_spawn_trans;
    private ParticleSystem particleSystem;

    //Serialized vars
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenAttack = 0.5f;
    [SerializeField] public float attackDuration = 0.5f;
    [SerializeField] public float deathDuration = 1.5f; 
    [SerializeField] private Direction_Type moveDirection = Direction_Type.NONE;
    [SerializeField] private float moveSpeed = 0f;

    //sounds var
    private AudioManager audio_manager; 

    //temp vars
    private float count = 0f;
    private Vector3 vect_mov_dir = Vector3.zero;
    private Transform trans;

    private Vector3 Direction_Type_To_Vector(Direction_Type dir_type)
    {
        if(dir_type == Direction_Type.NONE)
            return Vector3.zero;
        else if(dir_type == Direction_Type.RIGHT)
            return new Vector3(1,0,0);
        else if(dir_type == Direction_Type.LEFT)
            return new Vector3(-1,0,0);
        else if(dir_type == Direction_Type.TOWARDS)
            return new Vector3(0,0,-1);
        else if(dir_type == Direction_Type.AWAY)
            return new Vector3(0,0,1);
        else
            return Vector3.zero;
    }

    void Start()
    {
        child_sprite_effects = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteEffects>();
        proj_spawn_trans = gameObject.transform.GetChild(1);
        vect_mov_dir = Direction_Type_To_Vector(moveDirection);
        trans = gameObject.transform; 
        audio_manager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    void OnEnable()
    {
        particleSystem = gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();

        //play particle system
        particleSystem.Play();

        //destroy particle system after its duration
        Destroy(particleSystem.gameObject, particleSystem.main.duration);
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
            
            //move the sprite according to vect_mov_dir
            trans.position += vect_mov_dir * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other_col)
    {
        //if a OffStage trigger is detected,
        if(other_col.gameObject.layer == LayerMask.NameToLayer("OffStage"))
        {
            //decrement enemy count
            GameManagement.cur_num_enemies--;

            //destroy the enemy
            Destroy(gameObject);
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
        //play spin sound
        audio_manager.PlaySpinSound();

        yield return new WaitForSeconds(wait_time);

        //decrement enemy count
        GameManagement.cur_num_enemies--;

        Destroy_Destructable();
    } 

    public virtual IEnumerator WaitThenShoot(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);

        if(health > 0)
        {
            Instantiate(projectilePrefab, proj_spawn_trans.position, Quaternion.identity);

            //play whoosh noise
            audio_manager.PlayWhoosh();
        }
    }
}
