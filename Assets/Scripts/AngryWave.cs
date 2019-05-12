using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryWave : Enemy
{
    private float scaleSpeed = 0.001f;
    private bool checkPoint = false;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        movementForce = 20f;
        target = GameObject.FindGameObjectWithTag("WaveTarget");
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        if(Vector3.Distance(transform.position, target.transform.position) < 0.01f) 
        {
            target = FindObjectOfType<House>().gameObject;
            checkPoint = true;
            movementForce *= 2f;
            scaleSpeed *= 4f;
        }
        Scale();
    }

    private void Scale()
    {
        transform.localScale += new Vector3(1f, 1f, 1f) * scaleSpeed;
    }

    private void Hit()
    {
        FindObjectOfType<House>().ReceiveDamage(2f);
        Destroy(gameObject);
    }

    public override void OnInstrumentHit(InstrumentEffect instrumentEffect)
    {
        if (type == instrumentEffect.counteredEnemy)
        {
            float strength = 1; //instrumentEffect.GetStrength();
            transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f) * strength;
            if (transform.localScale.x <= 0.3f) Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target && checkPoint)
        {
            Hit();
        }
    }
}
