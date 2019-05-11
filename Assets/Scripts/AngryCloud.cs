using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryCloud : Wolf
{
    public override void OnInstrumentHit(InstrumentEffect instrumentEffect)
    {
        float strength = 1;//instrumentEffect.GetStrength();
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * strength;
        if (transform.localScale.x <= 0.3f) Destroy(gameObject);
    }
}
