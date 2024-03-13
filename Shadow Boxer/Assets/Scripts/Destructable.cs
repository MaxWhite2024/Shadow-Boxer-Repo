using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] public Punch_Type objectPunchType;
    [SerializeField] public int health = 1;

    public void Destroy_Destructable()
    {
        Destroy(gameObject);
    }

    public virtual void Take_Damage()
    {
        health -= 1;

        if(health <= 0)
        {
            Destroy_Destructable();
        }

        //Debug.Log("Damage!");
    }
}
