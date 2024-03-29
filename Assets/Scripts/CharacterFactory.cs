﻿
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    
    
    public int spawnCount { get; private set; } = 0;

    public float spawnDelay { get; private set; } = 20;
    public float spawnTimer { get; set; } = 0;

    public const string audioRoot = "Sounds/";

    public List<AudioSource> audioSources;
    
    private CharacterDef startingChracter;
    private List<CharacterDef> characterPool;
    

    // Start is called before the first frame update
    void Start()
    {
        
        startingChracter = 
            new CharacterDef("Character_1", "Trubadur", EnemyType.Wolf, new Color32(215,25,28, 255));

        characterPool = new List<CharacterDef> {
            new CharacterDef("Character_2", "Bass2", EnemyType.AngryCloud, new Color32(253,174,97, 255)),
            new CharacterDef("Character_3", "Syna2", EnemyType.AngryWave, new Color32(255,255,191, 255)),
            new CharacterDef("Character_4", "Crash", EnemyType.AngryBoulder, new Color32(166,217,106, 255)),
            new CharacterDef("Character_5", "Flute", EnemyType.Hamster, new Color32(26,150,65, 255)),
        };
        characterPool.OrderBy(i => UnityEngine.Random.value).ToList();
        characterPool.Insert(0, startingChracter);
        
        audioSources = new List<AudioSource>();

        foreach (CharacterDef def in characterPool)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = Resources.Load<AudioClip>(audioRoot + def.audioName);
            if (source.clip == null)
            {
                throw new NullReferenceException(source.clip.name);
            }
            source.mute = true;
            audioSources.Add(source);
        }

        //spawnTimer = spawnDelay;
    }

    // Initializes, instantiates and returns the next character
    public GameObject popCharacter()
    {
        if (spawnCount == characterPool.Count)
        {
            return null;
        }
        var def = characterPool[spawnCount];
        AudioSource audioSource = audioSources[spawnCount];
        spawnCount++;
        var obj = Resources.Load<GameObject>(def.prefabName);
        
        obj = Instantiate(obj, transform);

/*
        var audio = obj.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>(audioRoot + def.audioName);
        if (audio.clip == null)
        {
            throw new NullReferenceException(audio.clip.name);
        }
        audio.mute = true;
*/
        obj.GetComponent<Character>().counteredEnemy = def.counteredEnemy;
        obj.GetComponent<Character>().instrumentColor = def.instrumentColor;
        obj.GetComponent<Character>().audioSource = audioSource;
        //Debug.Log(obj.GetComponent<Character>().instrumentColor);

        return obj;
    }

    public void spawnCharacter()
    {
        var character = popCharacter();
        if (character != null)
        {
            character.transform.position = new Vector3(0, -0.5F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = spawnDelay;

            int players = GetComponentInChildren<PlayerFactory>().numPlayer;
            if (spawnCount < players * 2 + 1)
            {
                spawnCharacter();
            }
        }
    }
}
