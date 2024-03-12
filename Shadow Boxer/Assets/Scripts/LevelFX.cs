using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFX : MonoBehaviour
{
    //component vars
    private Transform left_curtain_trans;
    private Vector3 left_closed_pos;
    private Vector3 left_open_pos;
    private Transform right_curtain_trans;
    private Vector3 right_closed_pos;
    private Vector3 right_open_pos;

    //speed vars
    [SerializeField] private float curtainMoveSpeed = 1f;

    //bool vars
    private bool opening = false;
    private bool closing = false;

    void Awake()
    {
        left_curtain_trans = transform.GetChild(0).GetComponent<Transform>();
        left_closed_pos = transform.GetChild(1).GetComponent<Transform>().position;
        left_open_pos = transform.GetChild(2).GetComponent<Transform>().position;
        right_curtain_trans = transform.GetChild(3).GetComponent<Transform>();
        right_closed_pos = transform.GetChild(4).GetComponent<Transform>().position;
        right_open_pos = transform.GetChild(5).GetComponent<Transform>().position;

        left_curtain_trans.position = left_closed_pos;
        right_curtain_trans.position = right_closed_pos;
    }

    // Update is called once per frame
    void Update()
    {
        if(opening)
        {
            left_curtain_trans.position = Vector3.MoveTowards(left_curtain_trans.position, left_open_pos, curtainMoveSpeed * Time.deltaTime);
            right_curtain_trans.position = Vector3.MoveTowards(right_curtain_trans.position, right_open_pos, curtainMoveSpeed * Time.deltaTime);
        }

        if(closing)
        {
            left_curtain_trans.position = Vector3.MoveTowards(left_curtain_trans.position, left_closed_pos, curtainMoveSpeed * Time.deltaTime);
            right_curtain_trans.position = Vector3.MoveTowards(right_curtain_trans.position, right_closed_pos, curtainMoveSpeed * Time.deltaTime);
        }
    }

    public void OpenCurtains()
    {
        opening = true;

        closing = false;

        left_curtain_trans.position = left_closed_pos;
        right_curtain_trans.position = right_closed_pos;
    }

    public void CloseCurtains()
    {
        closing = true;

        opening = false;

        left_curtain_trans.position = left_open_pos;
        right_curtain_trans.position = right_open_pos;
    }
}
