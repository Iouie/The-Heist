using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip gunShot;
    public AudioClip crash;
    public AudioClip explode;
    public AudioClip bgMusic;
    public AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource.PlayOneShot(bgMusic);
    }

    // Update is called once per frame
    void Update()
    {
    }

   public void GunShot()
    {
        audioSource.PlayOneShot(gunShot);
    }

   public void Crash()
    {
        audioSource.PlayOneShot(crash);
    }
   public void Explosion()
    {
        audioSource.PlayOneShot(explode);
    }

}
