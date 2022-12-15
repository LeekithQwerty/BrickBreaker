using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int breakableBlocks;
    static int levelNumber=1;

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void CountDestroiedBlocks()
    {
        breakableBlocks--;
        if ( breakableBlocks== 0)
        {
            LevelNumber();
            FindObjectOfType<SceneLoader>().LoadNextScene();

        }
    }

    public void LevelNumber()
    {
        levelNumber++;
        FindObjectOfType<GameSession>().DisplayLevelNumber(levelNumber);
    }



    public void SetLevelNumber(int setLevelNumber)
    {
        levelNumber = setLevelNumber;
        FindObjectOfType<GameSession>().DisplayLevelNumber(levelNumber);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
