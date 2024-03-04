using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Punch_Type
{
    EITHER, LEFT, RIGHT
}

public class CrossHair : MonoBehaviour
{
    //Crosshair movement variables
    private RectTransform crosshair_rect_trans;
    private RectTransform canvas_rect_transform;
    private Vector2 canvas_size_delta;

    //Punching variables
    //...

    //Sprite variables
    [SerializeField] private RectTransform left_punch_spot_trans;
    [SerializeField] private RectTransform right_punch_spot_trans;
    [SerializeField] private GameObject left_punch_prefab;
    [SerializeField] private GameObject right_punch_prefab;
 
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
        //instantiate the desired punch
        if(punch_type == Punch_Type.LEFT)
        {
            Instantiate(left_punch_prefab, left_punch_spot_trans.anchoredPosition, Quaternion.identity, canvas_rect_transform);
        }   
        else if(punch_type == Punch_Type.RIGHT)
        {

        }

        //calculate worldposition of mouse
        Vector3 screen_pos = Input.mousePosition;
        screen_pos.z = 1000f;
        Vector3 world_pos = Camera.main.ScreenToWorldPoint(screen_pos);

        //make raycast from camera in the direction of worldposition
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.gameObject.transform.position, world_pos, out hit, Mathf.Infinity))
        {
            // Debug.DrawRay(Camera.main.gameObject.transform.position, world_pos, Color.red);
            // Debug.Log("Hit!");
            // if(punch_type == Punch_Type.LEFT)
            // {
            //     Debug.Log("Hit with LEFT");
            // }
            // else if(punch_type == Punch_Type.RIGHT)
            // {
            //     Debug.Log("Hit with RIGHT");
            // }

            Destructable destructable_component = hit.collider.gameObject.GetComponent<Destructable>();
            if(destructable_component)
            {
                if(destructable_component.objectPunchType == punch_type || destructable_component.objectPunchType == Punch_Type.EITHER)
                {
                    //Play correct hit sound
                    //...

                    //tell destructable object to destroy itself
                    destructable_component.Destroy_Destructable();
                }
                else
                {
                    //Play wrong hit sound
                    //...
                }
            }
            else
            {
                //Play hit solid object sound
                //...
            }
        }
        else
        {
            // Debug.DrawRay(Camera.main.gameObject.transform.position, world_pos, Color.green);
            // Debug.Log("Missed!");
            // if(punch_type == Punch_Type.LEFT)
            // {
            //     Debug.Log("Missed with LEFT");
            // }
            // else if(punch_type == Punch_Type.RIGHT)
            // {
            //     Debug.Log("Missed with RIGHT");
            // }

            //Play missed sound
            //...
        }

    }
}
