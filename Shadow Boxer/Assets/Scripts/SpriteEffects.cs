using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour
{
    //component vars
    private Transform trans;
    private SpriteRenderer sprite_renderer;

    //animation vars
    private float anim_prog = 0f;
    [SerializeField] public float anim_duration = 0.1f;
    private float death_duration;
    private float attack_duration;
    private bool jump_left = false;
    private bool jump_right = false;
    private bool squish = false;
    private bool spin = false;
    private bool fly = false;

    //sprite vars
    [SerializeField] private Sprite[] moveSprites;
    [SerializeField] private Sprite[] attackSprites;
    [SerializeField] private Sprite deathSprite;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        if(gameObject.name == "Basic Sprite Obj")
        {
            death_duration = trans.parent.gameObject.GetComponent<WalkerEnemy>().deathDuration;
            attack_duration = trans.parent.gameObject.GetComponent<WalkerEnemy>().attackDuration;
        }
        else
        {
            death_duration = trans.parent.gameObject.GetComponent<Enemy>().deathDuration;
            attack_duration = trans.parent.gameObject.GetComponent<Enemy>().attackDuration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(jump_left)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;

            //***** make the sprite "jump" *****
            if(anim_prog > 0f && anim_prog <= anim_duration/3f)
            {
                //move up and to left
                trans.localPosition += new Vector3(-2, 2, 0) * Time.deltaTime;
            }
            else if(anim_prog > (anim_duration/3f) * 2f && anim_prog <= anim_duration)
            {
                //move down and to right
                trans.localPosition += new Vector3(2, -2, 0) * Time.deltaTime;
            }
            else if(anim_prog > anim_duration)
            {
                jump_left = false;
                
                Reset_Trans();
            }
        }
        else if(jump_right)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;

            //***** make the sprite "jump" *****
            if(anim_prog > 0f && anim_prog <= anim_duration/3f)
            {
                //move up and to right
                trans.localPosition += new Vector3(2, 2, 0) * Time.deltaTime;
            }
            else if(anim_prog > (anim_duration/3f) * 2f && anim_prog <= anim_duration)
            {
                //move down and to left
                trans.localPosition += new Vector3(-2, -2, 0) * Time.deltaTime;
            }
            else if(anim_prog > anim_duration)
            {
                jump_right = false;
                
                Reset_Trans();
            }
        }
        else if(squish)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;

            //***** make the sprite "squish" *****
            if(anim_prog > 0f && anim_prog <= attack_duration/2f)
            {
                //change sprite to attack frame 0
                sprite_renderer.sprite = attackSprites[0];

                //stretch sprite vertically and shrink sprite horizontally
                trans.localScale += new Vector3(-1, 1, 0) * Time.deltaTime;
            }
            else if(anim_prog > attack_duration/2f && anim_prog <= attack_duration)
            {
                //change sprite to attack frame 1
                sprite_renderer.sprite = attackSprites[1];

                //shrink sprite vertically and stretch sprite horizontally
                trans.localScale += new Vector3(1, -1, 0) * Time.deltaTime;
            }
            else if(anim_prog > attack_duration)
            {
                sprite_renderer.sprite = moveSprites[0];

                squish = false;
                
                Reset_Trans();
            }
        }
        else if(spin)
        {
            //change sprite to death sprite
            sprite_renderer.sprite = deathSprite;

            //increment animation progress
            anim_prog += Time.deltaTime;

            //***** make the sprite "spin" *****
            if(anim_prog > 0f && anim_prog <= (death_duration/3f) * 2f)
            {
                //spin around y axis
                trans.Rotate(0f, 900f * Time.deltaTime, 0f, Space.Self);
            }
        }
        else if(fly)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;
        }
    }

    private void Reset_Trans()
    {
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = Vector3.one;
    }

    public void PlayDamageAnimation()
    {
        int selection = Random.Range(0, 2);
        if(selection == 0)
            Jump_Left();
        else if(selection == 1)
            Jump_Right();
    }

    public void PlayDeathAnimation()
    {
        int selection = Random.Range(0, 2);
        if(selection == 0)
            Spin();
        else if(selection == 1)
            Spin();
    }

    public void PlayAttackAnimation()
    {
        Squish();
    }

    private void Jump_Left()
    {
        //reset sprite position
        Reset_Trans();

        //reset animation progress
        anim_prog = 0f;

        //tell Update to play jump animation
        jump_left = true;

        //cancel other animations
        jump_right = false;
        squish = false;
        spin = false;
        fly = false;
    }

    private void Jump_Right()
    {
        //reset sprite position
        Reset_Trans();

        //reset animation progress
        anim_prog = 0f;

        //tell Update to play jump animation
        jump_right = true;

        //cancel other animations
        jump_left = false;
        squish = false;
        spin = false;
        fly = false;
    }

    private void Squish()
    {
        //reset sprite position
        Reset_Trans();

        //reset animation progress
        anim_prog = 0f;

        //tell Update to play squish animation
        squish = true;

        //cancel other animations
        jump_left = false;
        jump_right = false;
        spin = false;
        fly = false;
    }

    private void Spin()
    {
        //reset sprite position
        Reset_Trans();

        //reset animation progress
        anim_prog = 0f;

        //tell Update to play spin animation
        spin = true;

        //cancel other animations
        jump_left = false;
        jump_right = false;
        squish = false;
        fly = false;
    }

    private void Fly()
    {
        //reset sprite position
        Reset_Trans();

        //reset animation progress
        anim_prog = 0f;

        //tell Update to play fly animation
        fly = true;

        //cancel other animations
        jump_left = false;
        jump_right = false;
        squish = false;
        spin = false;
    }
}
