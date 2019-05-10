using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public PlayerController currentPlayerController;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public void PlayInstrument()
    {
        GameObject obj = Resources.Load<GameObject>("InstrumentEffect");
        obj = Instantiate(obj, transform.position, Quaternion.identity);
        InstrumentEffect instrumentEffect = obj.AddComponent<InstrumentEffect>();
        instrumentEffect.SetSize(3f);  // TODO: Change size based on something
    }
    
    /*
     * GetComponent<Rigidbody>().velocity =
            new Vector3(-Input.GetAxis("p" + playerNum.ToString() + "_joystick_horizontal") * speed * 60,
                        0, -Input.GetAxis("p" + playerNum.ToString() + "_joystick_vertical") * speed * 60);
     */
}
