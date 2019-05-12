using System.Collections;
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
    public bool isPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/"+spriteName);
        spriteRenderer = GetComponent<SpriteRenderer>();
        string layer = LayerMask.LayerToName(gameObject.layer);
        /*
        SpriteSwapper[] swaps = FindObjectsOfType<SpriteSwapper>();
        foreach(SpriteSwapper swap in swaps)
        {
            if(swap.gameObject != gameObject)
            {
                currentIndex = swap.currentIndex > 0 ? maximumIndex : 0;
                break;
            }
        }
        */
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
        if (isPlaying)
        {
            currentIndex += increment;
            if (currentIndex > maximumIndex) currentIndex = 0;
            Swap();
        }
    }

    public void SwapImage(bool first)
    {
        if(isPlaying)
        {
            currentIndex = first ? 0 : maximumIndex;
            Swap();
        }
    }
    private void Swap()
    {

        if (transform.childCount > 0)
        {
            if (currentIndex == 2) transform.GetChild(0).gameObject.SetActive(true);
            else if (maximumIndex == 2) transform.GetChild(0).gameObject.SetActive(false);
        }
        spriteRenderer.sprite = sprites[currentIndex];
        if (currentIndex == 2)
        {
            GetComponent<Enemy>().DealDamage();
        }
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

    
}
