using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour
{
    private Transform trans;
    private float anim_prog = 0f;
    private bool jump = false;
    [SerializeField] private float jump_speed = 1f;
    private bool spin = false;
    private bool fly = false;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;

        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if(jump)
        {
            //increment animation progress
            anim_prog += Time.deltaTime;

            //***** make the sprite "jump" *****
            trans.localPosition += Vector3.up * jump_speed * Time.deltaTime;
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
        trans.localScale = Vector3.zero;
    }

    public void Jump()
    {
        //reset sprite position
        Reset_Trans();

        //reset animation progress
        anim_prog = 0f;

        //tell Update to play jump animation
        jump = true;

        //cancel other animations
        spin = false;
        fly = false;
    }

    public void Spin()
    {

    }

    public void Fly()
    {

    }
}
