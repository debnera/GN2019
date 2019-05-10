using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Character currentCharacter;
    private int playerNum = 1;
    private float speed = 100;
    private bool useKeyboard = true;

    
    // Start is called before the first frame update
    void Start()
    {
        SelectNextCharacter();
    }

    void SelectNextCharacter()
    {
        // Select the first available character
        // TODO: Currently this will just toggle between the first two characters...
        foreach (Character newCharacter in FindObjectsOfType<Character>())
        {
            if (!newCharacter.currentPlayerController)
            {
                if (currentCharacter) currentCharacter.currentPlayerController = null;
                newCharacter.currentPlayerController = this;
                currentCharacter = newCharacter;
                return;
            }
        }
        
        Debug.LogError("Could not find a free character!");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = -Input.GetAxis("p" + playerNum.ToString() + "_joystick_horizontal");
        float vertical = -Input.GetAxis("p" + playerNum.ToString() + "_joystick_vertical");
//        Vector3 velocity =
//            new Vector3(-Input.GetAxis("p" + playerNum.ToString() + "_joystick_horizontal") * speed * Time.deltaTime,
//                0, -Input.GetAxis("p" + playerNum.ToString() + "_joystick_vertical") * speed * Time.deltaTime);


        if (useKeyboard)
        {
            if (Input.GetKey(KeyCode.W)) vertical = 1;
            if (Input.GetKey(KeyCode.S)) vertical = -1;
            if (Input.GetKey(KeyCode.A)) horizontal = -1;
            if (Input.GetKey(KeyCode.D)) horizontal = 1;
        }
        
        Vector3 velocity = new Vector2(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);
        currentCharacter.Move(velocity);




        if ((useKeyboard && Input.GetKeyDown(KeyCode.Space)))
        {
            currentCharacter.PlayInstrument();
        }
    }
}
