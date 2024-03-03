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
      
    void Update()
    {
        Move_CrossHair();

        //if left mouse button pressed,...
        if(Input.GetMouseButtonDown(0))
        {
            Punch(Punch_Type.LEFT);
        }
        
        //if right mouse button pressed,...
        if(Input.GetMouseButtonDown(1))
        {
            Punch(Punch_Type.RIGHT);
        }
    }

    void Move_CrossHair()
    {
        //move crosshair to mouse pos
        crosshair_rect_trans.anchoredPosition = (Vector2)(Input.mousePosition / canvas_rect_transform.localScale.x) - (canvas_size_delta / 2f);
    }

    void Punch(Punch_Type punch_type)
    {
        //calculate worldposition of mouse
        Vector3 screen_pos = Input.mousePosition;
        screen_pos.z = 1000f;
        Vector3 world_pos = Camera.main.ScreenToWorldPoint(screen_pos);

        //make raycast from camera in the direction of worldposition
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.gameObject.transform.position, world_pos, out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(Camera.main.gameObject.transform.position, world_pos, Color.red);
            //Debug.Log("Hit!");

            //
            if(punch_type == Punch_Type.LEFT)
            {
                Debug.Log("Hit with LEFT");
            }
            else if(punch_type == Punch_Type.RIGHT)
            {
                Debug.Log("Hit with RIGHT");
            }
        }
        else
        {
            //Debug.DrawRay(Camera.main.gameObject.transform.position, world_pos, Color.green);
            //Debug.Log("Missed!");

            //
            if(punch_type == Punch_Type.LEFT)
            {
                Debug.Log("Missed with LEFT");
            }
            else if(punch_type == Punch_Type.RIGHT)
            {
                Debug.Log("Missed with RIGHT");
            }
        }

    }
}
