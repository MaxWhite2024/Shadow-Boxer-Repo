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

    private RectTransform crosshair_rect_trans;
    private RectTransform canvas_rect_transform;
    private Vector2 crosshair_pos;
    private Vector2 canvas_size_delta;
    private GameObject camera_object;
    private Camera camera_camera;
 
    void Start()
    {
        Cursor.visible = false;
        crosshair_rect_trans = GetComponent<RectTransform>();
        canvas_rect_transform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        canvas_size_delta = canvas_rect_transform.sizeDelta;
        camera_object = GameObject.Find("Main Camera");
        camera_camera = camera_object.GetComponent<Camera>();
    }
      
    void FixedUpdate()
    {
        //Debug.Log("Mouse is: " + Input.mousePosition + "and rect is: " + crosshair_rect_trans.anchoredPosition);
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
        Ray ray = camera_camera.ScreenPointToRay(camera_camera.ViewportToWorldPoint(crosshair_rect_trans.anchoredPosition));
        //Debug.DrawRay(ray.origin, ray.direction, Color.green);
        Debug.Log(crosshair_rect_trans.anchoredPosition);
        
        Vector3 ray_origin = camera_object.transform.position;
        //Vector3 ray_dir = camera_camera.ViewportToWorldPoint(crosshair_rect_trans.anchoredPosition) - camera_object.transform.position;
        Vector3 ray_dir = camera_camera.ScreenToWorldPoint(crosshair_rect_trans.anchoredPosition);
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = 10f;
        Debug.DrawRay(ray_origin, ray_dir, Color.green);
        RaycastHit hit;
        //Debug.Log("Origin = " + ray_origin + "and dir = " + ray_dir);
        
        if(Physics.Raycast(ray_origin * 1000f, ray_dir, out hit, Mathf.Infinity))
        {
            Debug.Log("hit something!!!");
            
        }
    }
}
