using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<GameObject> bulletHolder;      // create a list to store all the bullets
    public GameObject bullet;  // get a bullet game object to instantiate
    Vehicle audi; // my audi
    public float shootRate = .5f; // shoot a bullet every .5 second
    float nextShot = 0.0f;

    void Start()
    {
        bulletHolder = new List<GameObject>();
    }

    void Update()
    {

    }

    public void Shoot()
    {
        audi = GameObject.Find("Audi").GetComponent<Vehicle>();   // get the audi
        if (Time.time > nextShot)
        {
            nextShot = Time.time + shootRate;    // shoot a bullet every .5 second
            GameObject bullets = Instantiate(bullet, audi.vehiclePosition + audi.direction, Quaternion.identity);
            bulletHolder.Add(bullets);
        }
    }


}
