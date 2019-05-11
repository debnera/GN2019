using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    public float movementForce = 10f;
    public float attackSpeed = 0.5f; // Attacks per second (Sync this to BPM?)
    public float maxFollowDistance = 5f;

    private bool canAttack = true;
    private GameObject target;
    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!target || Vector3.Distance(target.transform.position, transform.position) > maxFollowDistance)
        {
            // Reset to house if current target dies
            House house = FindObjectOfType<House>();
            target = house.gameObject;
        }
        
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        spriteRenderer.flipX = direction.x > 0;
        
        rbody.AddForce(direction * movementForce * Time.deltaTime);
    }

    public override void OnInstrumentHit(InstrumentEffect instrumentEffect)
    {
        Vector2 direction = instrumentEffect.transform.position - transform.position;
        rbody.AddForce(-direction * 5f, ForceMode2D.Impulse);
        if (instrumentEffect.owner) target = instrumentEffect.owner.gameObject;
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
