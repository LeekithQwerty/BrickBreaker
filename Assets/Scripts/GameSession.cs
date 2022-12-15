using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSession : MonoBehaviour
{
    [Range(0.1f , 10f )] [SerializeField] float gameSpeed=1f;
    [SerializeField] int pointsPerBlockDestroied = 40;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI ShowLevelText;

    [SerializeField] bool isAutoPlayEnabled;

    //State variables
    [SerializeField] int score;
    int levelNumber=0;

    //cached references
    



    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount>1)
        {
            //FindObjectOfType<LongPaddle>().SetPowerState(0);
            //FindObjectOfType<ExtraBall>().SetPowerState(0);
            
            Destroy(gameObject);

        }
        else
        {
            
            //FindObjectOfType<LongPaddle>().SetPowerState(0);
            //FindObjectOfType<ExtraBall>().SetPowerState(0);
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        FindObjectOfType<Ball>().SetLive(3);
        DisplayLevelNumber(1);
        scoreText.text = score.ToString();
     
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        

    }

    public void DisplayLevelNumber(int levelNumber)    // Testing
    {
        ShowLevelText.text = "LEVEL: " + levelNumber;
    }


    public void DisplayLives(int lives)    // Testing
    {
        lifeText.text = "" + lives;
    }



   public void AddToScore()
   {
       score = score + pointsPerBlockDestroied;
        
       scoreText.text = score.ToString();
   }


    public void SetScore(int setScore)
    {
        score = setScore;
        scoreText.text = score.ToString();
        Debug.Log("score=" + score);
    }


    public void ResetGame()
    {
        Debug.Log("In reset");
        //ChangeInScore(-score);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public void ChangeInScore(int changeInScore)
    {
        score += changeInScore;
        scoreText.text = score.ToString();
    }
}
