using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public float HP = 10f;

    public void ReduceHP(float value)
    {
        HP -= value;
        if (HP < 0)
        {
            Debug.Log("You lose!");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
