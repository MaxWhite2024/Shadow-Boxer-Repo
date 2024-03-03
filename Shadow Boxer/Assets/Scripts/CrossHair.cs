using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    private enum Punch_Type
    {
        LEFT, RIGHT
    }

    //Crosshair movement variables
    private RectTransform crosshair_rect_trans;
    private RectTransform canvas_rect_transform;
    private Vector2 canvas_size_delta;

    //Punching variables
    //...
 
    void Start()
    {
        //make mouse cursor invisible
        Cursor.visible = false;

        //setup vars
        crosshair_rect_trans = GetComponent<RectTransform>();
        canvas_rect_transform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        canvas_size_delta = canvas_rect_transform.sizeDelta;
    }
      
    void FixedUpdate()
    {
        Move_CrossHair();

        Punch(Punch_Type.LEFT);
        // //if left mouse button pressed,...
        // if(Input.GetMouseButton(0))
        // {
        //     Punch(Punch_Type.LEFT);
        // }
        
        // //if right mouse button pressed,...
        // if(Input.GetMouseButton(1))
        // {
        //     Punch(Punch_Type.RIGHT);
        // }
    }

    void Move_CrossHair()
    {
        //move crosshair to mouse pos
        crosshair_rect_trans.anchoredPosition = (Vector2)(Input.mousePosition / canvas_rect_transform.localScale.x) - (canvas_size_delta / 2f);
    }

    void Punch(Punch_Type punch_type)
    {
        Vector3 screen_pos = Input.mousePosition;
        screen_pos.z = 1000f;
        Vector3 world_pos = Camera.main.ScreenToWorldPoint(screen_pos);

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.gameObject.transform.position, world_pos, out hit, Mathf.Infinity))
        {
            
            Debug.DrawRay(Camera.main.gameObject.transform.position, world_pos, Color.red);
        }
        else
        {
            Debug.DrawRay(Camera.main.gameObject.transform.position, world_pos, Color.green);
        }
    }
}
