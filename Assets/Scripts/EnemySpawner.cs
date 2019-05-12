using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimeMin = 10f;
    public float spawnTimeMax = 15f;
    public float difficultyIncrease = 0.02f;
    public float difficultyCD = 10f;

    private bool canIncrease = true;
    private float nextSpawn;
    private string[] enemies = new[] {"Wolf", "AngryCloud", "AngryWave", "AngryBoulder", "Hamster"};
    
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
            Character[] players = FindObjectsOfType<Character>();
            if(players.Length > 0)
            {
                int index = Random.Range(0, players.Length);
                string enemy = players[index].counteredEnemy.ToString();
                Instantiate(Resources.Load(enemy), GetRandomSpawnPosition(enemy), Quaternion.identity);
            }
        }
        if(canIncrease)
        {
            IncreaseDifficulty();
            canIncrease = false;
        }
    }

    Vector3 GetRandomSpawnPosition(string enemy)
    {
        string spawns = "Bounds";
        if (enemy == "AngryCloud") spawns = "CloudSpawn";
        if (enemy == "AngryWave") spawns = "WaveSpawn";
        GameObject[] borderWalls = GameObject.FindGameObjectsWithTag(spawns);
        int index = Random.Range(0, borderWalls.Length);
        GameObject obj = borderWalls[index];
        Bounds b = obj.GetComponent<Collider2D>().bounds;
        return new Vector3(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            Random.Range(b.min.z, b.max.z)
        );
    }
    IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(difficultyCD);
        float factor = 1 - difficultyIncrease;
        spawnTimeMax *= factor;
        spawnTimeMin *= factor;
        canIncrease = true;
    }
}
