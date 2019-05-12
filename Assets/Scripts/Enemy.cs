using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void OnInstrumentHit(InstrumentEffect instrumentEffect);

    public float movementForce = 10f;
    protected GameObject target;
    protected Rigidbody2D rbody;
    protected float movementMultiplier = 1;
    private SpriteRenderer spriteRenderer;
    public EnemyType type;

    static public int killCounter = 0;

    protected void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void FixedUpdate()
    {
        
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        spriteRenderer.flipX = direction.x > 0;

        rbody.AddForce(direction * movementForce * movementMultiplier * Time.deltaTime);
    }

    private void OnDestroy()
    {
        killCounter++;
    }
}


