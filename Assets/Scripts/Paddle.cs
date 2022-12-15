using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float minX = 1f;

    [SerializeField] AudioClip[] laserSounds;

    AudioSource audioSource;

    //cached references
    GameSession gameSession;
    Ball ball;


    public Projectile laserPrefab;

    public bool PaddleIsShooting;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, 0.269f);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }




    public void StartShooting()
    {
        if (!PaddleIsShooting)
        {
            PaddleIsShooting = true;
            StartCoroutine(StartShoutingRoutine());
        }
    }

    public IEnumerator StartShoutingRoutine()
    {
        float fireCoolDown = 0.5f;
        float fireCoolDownLeft = 0;

        float shootingDuration = 8;
        float shootingDurationLeft = shootingDuration;

        Debug.Log("Start shooting");

        while (shootingDurationLeft >= 0)
        {
            fireCoolDownLeft -= Time.deltaTime;
            shootingDurationLeft -= Time.deltaTime;

            if (fireCoolDownLeft <= 0)
            {
                shoot();
                fireCoolDownLeft = fireCoolDown;
                Debug.Log("Shoot at" + Time.time);

            }

            yield return null;
        }
        Debug.Log("StopShooting");
        PaddleIsShooting = false;
    }

    private void shoot()
    {

        SpawnLeftLaser();
        SpawnRightLaser();
    }

    private void SpawnRightLaser()
    {
        AudioClip clip = laserSounds[0];
        audioSource.PlayOneShot(clip);
        Vector2 rightLaser = new Vector2(gameObject.transform.position.x + 1f, gameObject.transform.position.y + 0.5f);
        Vector2 spawnPosition = rightLaser;
        Projectile laser = Instantiate(laserPrefab, rightLaser, Quaternion.identity);
        Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        laserRb.AddForce(new Vector2(0, 450f));
    }

    private void SpawnLeftLaser()
    {
        Vector2 leftLaser = new Vector2(gameObject.transform.position.x - 1f, gameObject.transform.position.y + 0.5f);
        Vector2 spawnPosition = leftLaser;
        Projectile laser = Instantiate(laserPrefab, leftLaser, Quaternion.identity);
        Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        laserRb.AddForce(new Vector2(0, 450f));
    }


    public void DecreasePaddleLength(Collider2D collision)     // I want this code to be executed after 10 secondes 
    {
        Debug.Log("Decrease");
        collision.gameObject.transform.localScale += new Vector3(-1, 0, -1);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            Debug.Log("Power Up SOund Plays");
            AudioClip clip = laserSounds[1];
            audioSource.PlayOneShot(clip);
        }

    }



}

  