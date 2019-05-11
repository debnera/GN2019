using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    public float movementForce = 10f;
    public float attackSpeed = 0.5f; // Attacks per second (Sync this to BPM?)

    private bool canAttack = true;
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

    private void OnCollisionStay2D(Collision2D other)
    {
        House house = other.gameObject.GetComponent<House>();
        if (house)
        {
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(ResetAttack());
                house.ReduceHP(1);
            }
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1 / attackSpeed);
        canAttack = true;
    }
}
