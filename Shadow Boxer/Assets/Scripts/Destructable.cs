using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] public Punch_Type objectPunchType;

    public void Destroy_Destructable()
    {
        Destroy(gameObject);
    }
}
