using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float movementSpeed = 100f;
    
    public PlayerController currentPlayerController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentPlayerController)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void Move(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity * movementSpeed;
    }

    public void PlayInstrument()
    {
        GameObject obj = Resources.Load<GameObject>("InstrumentEffect");
        //obj = Instantiate(obj, transform.position, Quaternion.identity);
        obj = Instantiate(obj, transform);
        obj.transform.localPosition = Vector3.zero;
        InstrumentEffect instrumentEffect = obj.AddComponent<InstrumentEffect>();
        instrumentEffect.SetSize(3f);  // TODO: Change size based on something
    }
}
