using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Wolf
{
    private float spinSpeed = 2f;

    private void Start()
    {
        base.Start();
        movementForce = 20f;
    }
    // Start is called before the first frame update
    public override void OnInstrumentHit(InstrumentEffect instrumentEffect)
    {
        float strength = 1;//instrumentEffect.GetStrength();
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * strength;
        if (transform.localScale.x <= 0.3f) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        transform.RotateAround(transform.position, new Vector3(0, 0, 1), spinSpeed);
    }
}
