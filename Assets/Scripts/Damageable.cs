using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    public bool dead = false;
    public abstract void ReceiveDamage(float amount);
}
