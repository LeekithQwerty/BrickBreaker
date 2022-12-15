
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float velX= 2f;
    [SerializeField] float velY= 15f;
    [SerializeField] AudioClip[] ballSounds;
    public bool isStatic= true;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] bool usePowerUps;
    [SerializeField] int powerUpLimit;
    [SerializeField] int chanceOfNotGettingPowerUp;
    [SerializeField] bool isNormalBall=false;
    //State 
    [SerializeField] static int lives = 3;
    int changeInLives=0;
    int flag = 0;

    // Cached component references
    AudioSource audioSource;
    Rigidbody2D rigidbody2D;
    // PowerUps
    [SerializeField] GameObject[] powerUps;



    public float magnitude;
    


    void Start()
    {
        if (gameObject.tag == "Ball")
            isNormalBall = true;
        else if (gameObject.tag == "PowerUpBall")
        {
            isNormalBall = false;
            isStatic = false;
        }
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (isNormalBall)                              //normal ball
        {
            FindObjectOfType<GameSession>().DisplayLives(lives);
            //lifeText.text = "" + lives;
        }


        Vector2 check = new Vector2(1f, 1f);
        
        
    }

    // Update is called once per frame
    void Update()
    {



        if (isNormalBall)        // normal ball
        {


            FindObjectOfType<GameSession>().DisplayLives(lives);


            if (!isStatic)
            {
                Debug.Log("MAGNITUDE: " + rigidbody2D.velocity.magnitude);

                if (rigidbody2D.velocity.magnitude < 5 )
                {
                    Debug.Log("TRUE");
                    rigidbody2D.velocity = new Vector2(-2f, -10);
                }

            }
            if (isStatic)
            {
                GlueBallToPaddle();   //attach ball to paddle

                LaunchOnMouseClick(); //LAUNCH BALL ON CLICK

            }
        }
        if (!isNormalBall && flag==0)
        {
            isStatic = false;
            
            rigidbody2D.velocity = new Vector2(Random.Range(-2f, 2f), 10);
            flag = 1;
        }



    }

    
    private void LaunchOnMouseClick()
    {
       if (Input.GetMouseButtonDown(0))
        {
            isStatic = false;
            rigidbody2D.velocity = new Vector2(velX, velY);
        }
    }

    private void GlueBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, 0.807f);
        transform.position = paddlePos;
    }

    private void OnTriggerEnter2D(Collider2D collapse)
    {
        if (isNormalBall)                              //normal ball
        {
            if (collapse.gameObject.CompareTag("LoseCollider"))
            {
                changeInLive(-1);
                rigidbody2D.velocity = new Vector2(0, 0);
                isStatic = true;

            }
            if (lives <= 0)
            {
                FindObjectOfType<Ball>().SetLive(3);
                SceneManager.LoadScene("End Game");

            }
        }
        else
        {
            if (collapse.gameObject.CompareTag("LoseCollider"))
            {
                Destroy(gameObject);
            }
        }
    }


    public void SetLive(int setLive)
    {
        lives = setLive;
    }




    public void changeInLive(int change)
    {
        lives += change;
        Debug.Log("lives :" + lives);
        FindObjectOfType<GameSession>().DisplayLives(lives);
        //lifeText.text = "" + lives;
        if (lives <= 0)
        {
            FindObjectOfType<Ball>().SetLive(3);
            FindObjectOfType<Level>().SetLevelNumber(1);
            SceneManager.LoadScene("End Game");

        }
        Update();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor), 
            Random.Range(0f,randomFactor));
        if (isStatic == false)
        {
            rigidbody2D.velocity += velocityTweak;
            SoundEffects(collision);

        }
    }


    public void DestroyExtraBall()
    {
        Debug.Log("In Destroy extra ball");
        Destroy(gameObject,5f);
    }






    private void SoundEffects(Collision2D collision)
    {
        bool lastStateOfBlock;
        lastStateOfBlock = FindObjectOfType<Block>().LastStateOfBlock();
        
        if (collision.gameObject.tag == "BreakableBlock")
        {
            if (lastStateOfBlock==true)
            {
                if (usePowerUps)
                {
                    Debug.Log("IN powerup");
                    int randomChance = Random.Range(1, 101);
                    if (randomChance > chanceOfNotGettingPowerUp)
                    {
                        Debug.Log("powerup drop");
                        int randomPowerups = Random.Range(0, powerUpLimit);
                        Instantiate(powerUps[randomPowerups], collision.transform.position, collision.transform.rotation);
                    }
                }
                AudioClip clip = ballSounds[3];
                audioSource.PlayOneShot(clip);
            }
            else
            {
                AudioClip clip = ballSounds[0];
                audioSource.PlayOneShot(clip);
            }
            
        }

        if (collision.gameObject.tag == "Paddle")
        {
            AudioClip clip = ballSounds[1];
            audioSource.PlayOneShot(clip);
        }

        if (collision.gameObject.tag == "UnbreakableBlock")
        {
            AudioClip clip = ballSounds[1];
            audioSource.PlayOneShot(clip);
        }

        if (collision.gameObject.tag == "Wall")
        {
            AudioClip clip = ballSounds[2];
            audioSource.PlayOneShot(clip);
        }
    }
}
