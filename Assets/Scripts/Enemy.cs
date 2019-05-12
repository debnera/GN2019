using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void OnInstrumentHit(InstrumentEffect instrumentEffect);
    public float damage = 1f;
    public float movementForce = 10f;
    protected GameObject target;
    protected Rigidbody2D rbody;
    protected float movementMultiplier = 1;
    private SpriteRenderer spriteRenderer;
    public EnemyType type;
    public Damageable damageable;

    static public int killCounter = 0;

    protected void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void FixedUpdate()
    {
        spriteRenderer.sortingOrder = (int) transform.position.y*10;
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        spriteRenderer.flipX = direction.x > 0;

        rbody.AddForce(direction * movementForce * movementMultiplier * Time.deltaTime);
    }
    public void DealDamage()
    {
        if (damageable)
        {
            damageable.ReceiveDamage(damage);
        }
    }
    private void OnDestroy()
    {
        killCounter++;
    }
}


