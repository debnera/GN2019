using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentEffect : MonoBehaviour
{
    public Character owner;
    
    private float timeToLive = 0.2f;
    private float timer;
    private float originalScale;
    private float curSize;

    public void SetSize(float size)
    {
        originalScale = size;
        transform.localScale = new Vector3(size, size);
    }

    public float GetStrength()
    {
        // Strength affects how greatly the instrument affects enemies
        return curSize;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timer = timeToLive;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        float newScale = originalScale * (timer / timeToLive);
        curSize = newScale;
        transform.localScale = new Vector3(newScale, newScale);
        if (timer < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy hittable = other.GetComponent<Enemy>();
        if (hittable)
        {
            hittable.OnInstrumentHit(this);
        }
    }


}
