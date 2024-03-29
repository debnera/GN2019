﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : Damageable
{
    public float HP = 10f;
    private float maxHP = 10f;
    public float newCharacterMin = 10f;
    public float newCharacterMax = 30f;
    public float spawnTimer;
    private GameObject healthBar;

    public override void ReceiveDamage(float value)
    {
        HP -= value;
        healthBar.GetComponent<Slider>().value = HP / maxHP;
        if (HP < 0)
        {
            Debug.Log("You lose!");
            GameObject.Find("StateEngine").GetComponent<Animator>().Play("End");
        }
        else
        {
            Debug.Log("House HP: " + HP);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(newCharacterMin, newCharacterMax);
        healthBar = GameObject.Find("HealthBar");
        maxHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        //spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            spawnTimer = Random.Range(newCharacterMin, newCharacterMax);
            GameObject obj = Resources.Load<GameObject>("Character_1");
            obj = Instantiate(obj, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
        }
    }
}
