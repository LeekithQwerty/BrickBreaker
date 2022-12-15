using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : MonoBehaviour
{
    
    [SerializeField] GameObject PowerUpball;
    [SerializeField] int numNewBalls;
    static int powerState = 0;
    bool isExtraBall = false;


    GameObject extraBall;


    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
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
            if (powerState == 1)
            {
                for (int i = 0; i < numNewBalls; i++)
                {
                    Instantiate(PowerUpball, transform.position, Quaternion.identity);
                    FindObjectOfType<Ball>().DestroyExtraBall();
                }
                isExtraBall = true;
                //Update();
            }
            //FindObjectOfType<Ball>().DestroyExtraBall();
            powerState = 0;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "LoseCollider")
        {
            Destroy(gameObject);
        }
    }
}
