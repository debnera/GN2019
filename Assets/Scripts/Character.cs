using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Damageable
{
    public float movementSpeed = 100f;
    public float respawnTime = 5f;
    
    private float respawnTimer;
    private int lastHit;

    public EnemyType counteredEnemy;
    public Color instrumentColor;
    
    public PlayerController currentPlayerController;
    private CharacterDef characterDef;

    private SpriteSwapper spriteSwapper;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        spriteSwapper = GetComponent<SpriteSwapper>();
        GetComponent<SpriteSwapper>().isPlaying = false;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteSwapper)
        {
            spriteSwapper.isPlaying = !dead && !audio.mute;
        }
        if (!currentPlayerController)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            audio.volume -= Time.deltaTime / 5f;
            if (audio.volume <= 0) audio.mute = true;
        }
        else
        {
            
            audio.volume = 1;
        }

        if (dead)
        {
            audio.mute = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            respawnTimer -= Time.deltaTime;
            if (respawnTimer < 0)
            {
                dead = false;
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = !dead;
        }
    }

    public void Move(Vector2 velocity)
    {
        if (dead) return;
        GetComponent<Rigidbody2D>().velocity = velocity * movementSpeed;
    }

    public void PlayInstrument()
    {
        var controller = GetComponentInParent<BeatController>();
        if (!controller.onBeat() || lastHit == controller.hit_count || dead) return;
        lastHit = controller.hit_count;
        var audio = GetComponent<AudioSource>();
        audio.mute = false;
        spriteSwapper = GetComponent<SpriteSwapper>();
        GetComponent<SpriteSwapper>().isPlaying = true;

        GameObject obj = Resources.Load<GameObject>("InstrumentEffect");
        //obj = Instantiate(obj, transform.position, Quaternion.identity);
        obj = Instantiate(obj, transform);
        obj.transform.localPosition = Vector3.zero;
        InstrumentEffect instrumentEffect = obj.AddComponent<InstrumentEffect>();
        instrumentEffect.SetSize(3f);  // TODO: Change size based on something
        instrumentEffect.owner = this;
        instrumentEffect.counteredEnemy = counteredEnemy;
        instrumentEffect.SetColor(instrumentColor);
        Debug.Log(instrumentEffect.GetComponent<SpriteRenderer>().color);
    }


    public override void ReceiveDamage(float amount)
    {
        dead = true;
        respawnTimer = respawnTime;
        GetComponent<SpriteSwapper>().isPlaying = false;
    }
}
