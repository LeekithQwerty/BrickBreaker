using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float powerUpSpeed;
    void Start()
    {
        Debug.Log("Extra life drop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Paddle")
        {
            
            FindObjectOfType<Ball>().changeInLive(1);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "LoseCollider")
        {
            Destroy(gameObject);
        }
    }
}
