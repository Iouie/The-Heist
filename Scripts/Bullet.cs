using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 bulletPosition = Vector3.zero;
    public Vector3 direction;
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    Vehicle myAudi;
    // Use this for initialization
    void Start()
    {
        myAudi = GameObject.FindGameObjectWithTag("Audi").GetComponent<Vehicle>();
        direction = myAudi.direction;
    }

    // Update is called once per frame
    void Update()
    {
        BulletDirection();
        SetTransform();
    }

    void BulletDirection()
    {
        bulletPosition = transform.position;
        acceleration = .1f * direction; // bullets travel


        velocity += acceleration;

        bulletPosition += velocity;


    }
     
    void SetTransform()
    {
        transform.position = bulletPosition;
    }
}
