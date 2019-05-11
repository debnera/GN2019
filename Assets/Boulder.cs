using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Wolf
{
    private float spinSpeed = 5f;
    private float slowSpeed = 0.999f;

    private void Start()
    {
        base.Start();
        movementForce = 50f;
    }
    // Start is called before the first frame update
    public override void OnInstrumentHit(InstrumentEffect instrumentEffect)
    {
        Vector2 direction = instrumentEffect.transform.position - transform.position;
        rbody.AddForce(-direction * 0.5f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        transform.RotateAround(transform.position, new Vector3(0, 0, 1), spinSpeed);
        ReduceSpeed();
    }
    private void ReduceSpeed()
    {
        spinSpeed *= slowSpeed;
        movementForce *= slowSpeed;
        if(movementForce <= 1f)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == target)
        {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            if (damageable)
            {
                damageable.ReceiveDamage(3);
            }
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
