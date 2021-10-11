using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparkelsVFX;
    [SerializeField] Sprite[] hitSprites;
   
    Level level;
    GameStatus gamestatus;

    [SerializeField] int timesHit;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gamestatus = FindObjectOfType<GameStatus>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            handleHit();
        }
    }

    private void handleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1; 
        if (timesHit >= maxHits)
        {
            DestroyBlocks();
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
            Debug.LogError("Block Sprite Is Missing From Array" + gameObject.name);
        }
    }


        private void DestroyBlocks()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        gamestatus.AddToScore();
        Destroy(gameObject);
        level.blocksDestroyed();
        triggerSparkels();
    }

    private void triggerSparkels()
    {
        GameObject sparkels = Instantiate(blockSparkelsVFX, transform.position, transform.rotation);
        Destroy(sparkels, 1f);
    }
}
