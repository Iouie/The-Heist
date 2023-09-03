using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Vector3 policePosition;
    public Vector3 direction = new Vector3(1, 0, 0);
    public Vector3 velocity = Vector3.zero;
    public float accelRate = .9f;
    public Camera cam;


    // Use this for initialization
    void Start()
    {
        policePosition = new Vector3(Random.Range(-26, 26), Random.Range(-10, 10), 0);   // spawns enemy at random position
        velocity = new Vector3(Random.Range(-.01f, 0.01f), Random.Range(-.01f, 0.01f), 0); // gets random velocity every time
        velocity *= accelRate;
        velocity = Vector3.ClampMagnitude(velocity, 0.2f);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        Wrap();
        setTransform();
    }



    public void Wrap()
    {
        float camHeight = cam.orthographicSize * 2f;     // divide by 2 to get the height
        float totalCamWidth = camHeight * cam.aspect;    // divide by 2 to get the width
        if (policePosition.x < -totalCamWidth / 2)
        {
            policePosition.x = totalCamWidth / 2;
        }
        if (policePosition.x > totalCamWidth / 2)
        {
            policePosition.x = -totalCamWidth / 2;
        }
        if (policePosition.y > camHeight / 2)
        {
            policePosition.y = -camHeight / 2;
        }
        if (policePosition.y < -camHeight / 2)
        {
            policePosition.y = camHeight / 2;
        }
    }

    void Move()
    {
        policePosition += velocity; // enemies move at reandom velocity
    }

    void setTransform()
    {
        transform.position = policePosition;  
    }

}

