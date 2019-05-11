﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimeMin = 1f;
    public float spawnTimeMax = 10f;

    private float nextSpawn;
    private string[] enemies = new[] {"Wolf", "AngryCloud"};
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawn -= Time.deltaTime;
        if (nextSpawn < 0)
        {
            nextSpawn = Random.Range(spawnTimeMin, spawnTimeMax);
            int index = Random.Range(0, enemies.Length);
            Instantiate(Resources.Load(enemies[index]), GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        GameObject[] borderWalls = GameObject.FindGameObjectsWithTag("Bounds");
        int index = Random.Range(0, borderWalls.Length);
        GameObject obj = borderWalls[index];
        Bounds b = obj.GetComponent<Collider2D>().bounds;
        return new Vector3(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            Random.Range(b.min.z, b.max.z)
        );
    }
}
