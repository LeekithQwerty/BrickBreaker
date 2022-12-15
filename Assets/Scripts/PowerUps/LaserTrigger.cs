using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    static int powerState = 0;
    // Start is called before the first frame update
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
                Debug.Log("Iin laser" + powerState);
                FindObjectOfType<Paddle>().StartShooting();
                
            }
            powerState = 0;
            Destroy(gameObject);
            
        }


        if (collision.gameObject.tag == "LoseCollider")
        {
            Destroy(gameObject);
        }
    }



}
