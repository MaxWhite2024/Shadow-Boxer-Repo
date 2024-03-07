using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour
{
    private Transform trans;
    private float anim_prog = 0f;
    [SerializeField] public float anim_duration = 0.1f;
    [SerializeField] public float death_duration = 2f;
    [SerializeField] public float attack_duration = 1f;
    private bool jump_left = false;
    private bool jump_right = false;
    private bool squish = false;
    private bool spin = false;
    private bool fly = false;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
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
                //stretch sprite vertically and shrink sprite horizontally
                trans.localScale += new Vector3(-1, 1, 0) * Time.deltaTime;
            }
            else if(anim_prog > attack_duration/2f && anim_prog <= attack_duration)
            {
                //shrink sprite vertically and stretch sprite horizontally
                trans.localScale += new Vector3(1, -1, 0) * Time.deltaTime;
            }
            else if(anim_prog > attack_duration)
            {
                squish = false;
                
                Reset_Trans();
            }
        }
        else if(spin)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;

            //***** make the sprite "spin" *****
            if(anim_prog > 0f && anim_prog <= (death_duration/3f) * 2f)
            {
                //spin around y axis
                trans.Rotate(0f, 900f * Time.deltaTime, 0f, Space.Self);
            }
            else if(anim_prog > death_duration)
            {
                spin = false;
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
