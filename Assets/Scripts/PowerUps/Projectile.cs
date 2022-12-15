using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] AudioClip[] laserSounds;

    AudioSource audioSource;



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "BreakableBlock")
        {
            AudioClip clip = laserSounds[0];
            audioSource.PlayOneShot(clip);
        }
        Destroy(gameObject,0.2f);
    }
}
