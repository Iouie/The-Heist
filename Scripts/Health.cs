using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
    public Image[] cars;

     void Update()
    {
        GetHealth();
    }

    void GetHealth()
    {
        if(health == 2)
        {
            Destroy(cars[2]);
        }

        if(health == 1)
        {
            Destroy(cars[1]);
        }
        if(health == 0)
        {
            Destroy(cars[0]);
        }
    }
}
