using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour
{
    private Transform trans;
    private float anim_prog = 0f;
    [SerializeField] private float anim_duration = 0.5f;
    private bool jump = false;
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
        if(jump)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;

            //***** make the sprite "jump" *****
            if(anim_prog > 0f && anim_prog <= anim_duration)
            {
                trans.localPosition = Vector3.up * (Mathf.Sin(anim_prog) / anim_duration);
            }
            else if(anim_prog > anim_duration)
            {
                jump = false;
            }
        }
        else if(squish)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;
        }
        else if(spin)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;
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
            Jump();
        else if(selection == 1)
            Squish();
    }

    public void PlayDeathAnimation()
    {
        int selection = Random.Range(0, 2);
        if(selection == 0)
            Spin();
        else if(selection == 1)
            Fly();
    }

    private void Jump()
    {
        //reset sprite position
        Reset_Trans();

        //reset animation progress
        anim_prog = 0f;

        //tell Update to play jump animation
        jump = true;

        //cancel other animations
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
        jump = false;
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
        jump = false;
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
        jump = false;
        squish = false;
        spin = false;
    }
}
