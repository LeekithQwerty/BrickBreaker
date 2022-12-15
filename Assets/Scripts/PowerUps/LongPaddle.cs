using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;


public class LongPaddle : MonoBehaviour
{
    static int powerState = 0;
    public GameObject paddle;
    bool timerReached = false;
    float timer = 0;
    bool isPaddleLong = false;
    private void Start()
    {
        paddle = GameObject.FindWithTag("Paddle");
    }



    private void Update()
    {
        
        if (isPaddleLong)
        {
            
            if (!timerReached)
            {
                timer += Time.deltaTime;
                
            }
            if (!timerReached && timer > 7)
            {
                
                timerReached = true;
                paddle.transform.localScale += new Vector3(-0.6f, 0, 1f);
                isPaddleLong = false;
                powerState = 0;
            }
        }
    }
    public void SetPowerState(int powerStateSet)
    {
        powerState = powerStateSet;
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            
            powerState = powerState + 1;
            
            if(powerState == 1)
            {
                
                Debug.Log("in powerState" + powerState);
                collision.transform.localScale += new Vector3(0.6f, 0, 1);
                isPaddleLong = true;

                
            }
            Destroy(gameObject, 10f);
            
        }
        if (collision.gameObject.tag == "LoseCollider")
        {
            Destroy(gameObject,10f);
        }

    }
    
     
   
}
