using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    // https://opengameart.org/content/free-top-down-car-sprites-by-unlucky-studio   sprites
    // https://opengameart.org/content/2d-top-down-highway-background  background
    // https://opengameart.org/content/tx-bullet-0 bullet
    // font https://www.dafont.com/roadtest.font?text=the+
    //intro theme https://www.youtube.com/watch?v=C7Qo2QZ8NE8
    // ingame music https://www.fesliyanstudios.com/royalty-free-music/download/action-fight/280
    // bloody bg <a href="https://www.freepik.com/free-photos-vectors/background">Background photo created by kjpargeter - www.freepik.com</a>
    // car crash http://cd.textfiles.com/10000soundssongs/WAV/CARCR222.WAV
    // gun http://soundbible.com/2120-9mm-Gunshot.html
    // explosion https://www.freesoundeffects.com/free-sounds/explosion-10070/

    // Necessary
    public float accelRate;                     // Small, constant rate of acceleration
    public Vector3 vehiclePosition;             // Local vector for movement calculation
    public Vector3 direction;                   // Way the vehicle should move
    public Vector3 velocity;                    // Change in X and Y
    public Vector3 acceleration;                // Small accel vector that's added to velocity
    public float angleOfRotation;               // 0 
    public float maxSpeed;                      // 0.5 per frame, limits mag of velocity
    public Camera cam;
    public BulletManager bulletManager;
    public SoundManager getGun;
    

    // Use this for initialization
    void Start()
    {
        vehiclePosition = new Vector3(0, 0, 0);     // Or you could say Vector3.zero
        direction = new Vector3(0, 1, 0);           // Facing up
        velocity = new Vector3(0, 0, 0);            // Starting still (no movement)
        cam = Camera.main;
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        getGun = GameObject.Find("AudioObject").GetComponent<SoundManager>();

    }

    // Update is called once per frame
    void Update()
    {
       // direction = transform.up;
        // shoot bullets if spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            bulletManager.Shoot(); // shoots bullets
            getGun.GunShot();
        }

        RotateVehicle();

        Drive();

        SetTransform();

        Accelerate();

        Wrap();
    }

    /// <summary>
    /// Changes / Sets the transform component
    /// </summary>
    public void SetTransform()
    {
        // Rotate vehicle sprite
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        // Set the transform position
        transform.position = vehiclePosition;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Drive()
    {
        // Accelerate
        // Small vector that's added to velocity every frame
        acceleration = accelRate * direction;

        // We used to use this, but acceleration will now increase the vehicle's "speed"
        // Velocity will remain intact from one frame to the next
        //  velocity += acceleration;

        // Limit velocity so it doesn't become too large
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Add velocity to vehicle's position
        vehiclePosition += velocity;
    }

    public void RotateVehicle()
    {
        // Player can control direction
        // Left arrow key = rotate left by 2 degrees
        // Right arrow key = rotate right by 2 degrees
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angleOfRotation += 2;
            direction = Quaternion.Euler(0, 0, 2) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            angleOfRotation -= 2;
            direction = Quaternion.Euler(0, 0, -2) * direction;
        }
    }

    public void Accelerate()
    {
        // player can accelerate and decelerate

        if (Input.GetKey(KeyCode.UpArrow))
        {
            maxSpeed = .15f;
            accelRate += .00003f;
            velocity += acceleration;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else if (velocity.magnitude > 0)
        {
            velocity *= .94f;
        }
        else
        {

            velocity = new Vector3(0, 0, 0);
        }
    }

    public void Wrap()
    {
        // wrap around the camera
        float camHeight = cam.orthographicSize * 2f;     // divide by 2 to get the height
        float totalCamWidth = camHeight * cam.aspect;    // divide by 2 to get the width

        if (vehiclePosition.x > (totalCamWidth / 2))
        {
            vehiclePosition.x = -(totalCamWidth / 2);
        }
        if (vehiclePosition.x < -(totalCamWidth / 2))
        {
            vehiclePosition.x = totalCamWidth / 2;
        }
        if (vehiclePosition.y > camHeight / 2)
        {
            vehiclePosition.y = -camHeight / 2;
        }
        if (vehiclePosition.y < -camHeight / 2)
        {
            vehiclePosition.y = camHeight / 2;
        }
    }
}
