using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    public float attackSpeed = 0.5f; // Attacks per second (Sync this to BPM?)
    public float maxFollowDistance = 5f;
    private bool canAttack = true;

    // Update is called once per frame
    protected void FixedUpdate()
    {
        
        if (!target || Vector3.Distance(target.transform.position, transform.position) > maxFollowDistance)
        {
            // Reset to house if current target dies
            House house = FindObjectOfType<House>();
            target = house.gameObject;
        }
        Character character = target.GetComponent<Character>();
        if (character)
        {
            movementMultiplier = 3;
            if (character.dead)
            {
                target = null;
                return;
            }
        }
        base.FixedUpdate();
    }

    public override void OnInstrumentHit(InstrumentEffect instrumentEffect)
    {
        Vector2 direction = instrumentEffect.transform.position - transform.position;
        rbody.AddForce(-direction * 5f, ForceMode2D.Impulse);
        if (instrumentEffect.owner) target = instrumentEffect.owner.gameObject;
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

    private void OnCollisionExit2D(Collision2D other)
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            GetComponent<SpriteSwapper>().EndAttack();
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1 / attackSpeed);
        canAttack = true;
    }

}
