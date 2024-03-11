using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_Rate_Limiter : MonoBehaviour
{
    [SerializeField] private int target_frame_rate;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = target_frame_rate;
    }
}