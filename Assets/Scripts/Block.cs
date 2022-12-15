using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // Cached references
    Level level;
    GameSession gameSession;


    // config params
    [SerializeField] GameObject blockParticleEffect;
    [SerializeField] Sprite[] hitSprites;


    //state Variables
    [SerializeField] int timesHit;
    int maxHit;
    
    private void Start()
    {
        CountBreakableBlocks();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "BreakableBlock")
        {

            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "BreakableBlock")
        {
            HandleHit(collision);

        }
    }

    private void HandleHit(Collision2D collision)
    {
        timesHit++;
        maxHit = hitSprites.Length + 1;
        if (timesHit >=maxHit)
        {
            DestroyBlock(collision);
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
           
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }

        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }
    }

    private void DestroyBlock(Collision2D collision)
    {
        Destroy(gameObject);
        level.CountDestroiedBlocks();
        gameSession.AddToScore();
        TriggerParticleEffect();
    }


    public void TriggerParticleEffect()
    {
        GameObject sparkles = Instantiate(blockParticleEffect,transform.position,transform.rotation);
        Destroy(sparkles, 1f);
    }
    

    public bool LastStateOfBlock()
    {
        bool lastStateOfBlock;
         if (maxHit==timesHit || maxHit==1)
        {
            lastStateOfBlock=true;
        }
         else
        {
            lastStateOfBlock = false;
        }
        return lastStateOfBlock;
    }
}
