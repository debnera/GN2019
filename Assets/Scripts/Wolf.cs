using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private float acceleration = 10f;
    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        House house = FindObjectOfType<House>();
        target = house.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        GetComponent<Rigidbody2D>().AddForce(direction * acceleration * Time.deltaTime);
    }

    public void HitByInstrument(InstrumentEffect instrumentEffect)
    {
        Vector2 direction = instrumentEffect.transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(-direction * 5f, ForceMode2D.Impulse);
    }
}
