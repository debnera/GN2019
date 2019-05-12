﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    public string spriteName;
    private Sprite[] sprites;
    public int currentIndex = 0;
    private SpriteRenderer spriteRenderer;
    private int maximumIndex = 1;
    private int increment = 1;
    private bool canSwap = true;
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/"+spriteName);
        spriteRenderer = GetComponent<SpriteRenderer>();
        string layer = LayerMask.LayerToName(gameObject.layer);
    }
    private void Update()
    {
        /*
        if(canSwap)
        {
            canSwap = false;
            StartCoroutine(Swap());
        }
        */
    }

    public void SwapImage()
    {
        currentIndex += increment;
        if (currentIndex > maximumIndex) currentIndex = 0;
        if(transform.childCount > 0)
        {
            if (currentIndex == 2) transform.GetChild(0).gameObject.SetActive(true);
            else if (maximumIndex == 2) transform.GetChild(0).gameObject.SetActive(false);
        }
        
        spriteRenderer.sprite = sprites[currentIndex];
    }

    public void SwapImage(bool first)
    {
        currentIndex = first ? 0 : maximumIndex;
        if (transform.childCount > 0)
        {
            if (currentIndex == 2) transform.GetChild(0).gameObject.SetActive(true);
            else if (maximumIndex == 2) transform.GetChild(0).gameObject.SetActive(false);
        }
        spriteRenderer.sprite = sprites[currentIndex];
    }

    public void StartAttack()
    {
        //currentIndex = 0;
        increment = 2;
        maximumIndex = 2;
    }
    public void EndAttack()
    {
        //currentIndex = 0;
        increment = 1;
        maximumIndex = 1;
    }

    IEnumerator Swap()
    {
        
        yield return new WaitForSeconds(0.1f);
        canSwap = true;
        SwapImage();
    }
}
