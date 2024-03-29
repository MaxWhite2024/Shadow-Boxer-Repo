using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Destructable
{
    [SerializeField] private float controlHeight = 3f;
    private Vector3 start_pos;
    private Vector3 end_pos;
    private Vector3 control_pos;
    private Vector3 cur_x;
    private Vector3 cur_y;
    private float count;
    [SerializeField] float projectileSpeed = 0.0f;

    void Start()
    {
        //starting position is starting position
        start_pos = gameObject.transform.position;

        //end position is camera position
        end_pos = Camera.main.gameObject.transform.position;

        //control position is above halway between start_pos and end_pos with horizontal randomness
        control_pos = Vector3.Lerp(start_pos, end_pos, 0.5f) + (Vector3.up * controlHeight) + (Vector3.right * Random.Range(-5, 6));
    }

    void Update()
    {
        count += projectileSpeed * Time.deltaTime;
        cur_x = Vector3.Lerp(start_pos, control_pos, count);
        cur_y = Vector3.Lerp(control_pos, end_pos, count);
        gameObject.transform.position = Vector3.Lerp(cur_x, cur_y, count);

        //if final destination is reached, 
        if(gameObject.transform.position == end_pos)
        {
            //tell player to take damage
            GameManagement.Take_Damage();
                
            //destroy self
            base.Destroy_Destructable();
        }
    }
}
