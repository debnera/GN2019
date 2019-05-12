using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryCloud : Enemy
{
    private bool checkPoint = false;
    public float attackSpeed = 0.5f; // Attacks per second (Sync this to BPM?)
    private bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        movementForce = 15f;
        target = GameObject.FindGameObjectWithTag("CloudTarget");
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target = FindObjectOfType<House>().gameObject;
            checkPoint = true;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(ResetAttack());
                damageable.ReceiveDamage(1);
                GetComponent<SpriteSwapper>().StartAttack();
            }
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1 / attackSpeed);
        canAttack = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            GetComponent<SpriteSwapper>().EndAttack();
        }
    }
    public override void OnInstrumentHit(InstrumentEffect instrumentEffect)
    {
        if (type == instrumentEffect.counteredEnemy)
        {
            float strength = 1; //instrumentEffect.GetStrength();
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * strength;
            if (transform.localScale.x <= 0.3f) Destroy(gameObject);
        }
    }


}
