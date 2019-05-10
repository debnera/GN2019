using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentEffect : MonoBehaviour
{
    private float timeToLive = 0.2f;
    private float timer;
    private float originalScale;

    public void SetSize(float size)
    {
        originalScale = size;
        transform.localScale = new Vector3(size, size);
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
        transform.localScale = new Vector3(newScale, newScale);
        if (timer < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Do stuff to enemies
    }
}
