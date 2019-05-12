using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Damageable
{
    public float movementSpeed = 100f;
    public float respawnTime = 5f;
    
    private float respawnTimer;

    public EnemyType counteredEnemy;
    public Color instrumentColor;
    
    public PlayerController currentPlayerController;
    private CharacterDef characterDef;

    private SpriteSwapper spriteSwapper;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        spriteSwapper = GetComponent<SpriteSwapper>();
        GetComponent<SpriteSwapper>().isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentPlayerController)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (dead)
        {
            
            if (spriteSwapper) spriteSwapper.enabled = false;
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
            if (spriteSwapper) spriteSwapper.enabled = true;
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
        if (dead) return;
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
