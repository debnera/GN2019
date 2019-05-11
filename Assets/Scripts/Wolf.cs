using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    public float movementForce = 10f;
    private GameObject target;
    private Rigidbody2D rbody;
    
    // Start is called before the first frame update
    void Start()
    {
        House house = FindObjectOfType<House>();
        target = house.gameObject;
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        rbody.AddForce(direction * movementForce * Time.deltaTime);
    }

    public override void OnInstrumentHit(InstrumentEffect instrumentEffect)
    {
        Vector2 direction = instrumentEffect.transform.position - transform.position;
        rbody.AddForce(-direction * 5f, ForceMode2D.Impulse);
    }
}
