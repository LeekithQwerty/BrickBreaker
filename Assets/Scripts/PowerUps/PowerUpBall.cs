using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBall : MonoBehaviour
{

    [SerializeField] float velX = 3f;
    [SerializeField] float velY = 15f;


    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(velX, velY);
    }
}
