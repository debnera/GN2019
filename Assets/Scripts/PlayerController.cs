﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Character currentCharacter;
    public int playerNum = 1;

    private bool useKeyboard = true;

    private int curCharacterIndex;

    
    // Start is called before the first frame update
    void Start()
    {
        SelectNextCharacter();
    }

    void SelectNextCharacter()
    {

        
        List<Character> characters = new List<Character>(FindObjectsOfType<Character>());
        //characters.Sort((a,b)=>a.GetInstanceID().CompareTo(b.GetInstanceID())); 
        
        int startIndex = curCharacterIndex + 1; // Start iterating from current character index
        for (int i = 0; i < characters.Count; i++)
        {
            int index = startIndex + i;
            if (index >= characters.Count) index -= characters.Count;  // Wrap around to 0
            Character newCharacter = characters[index];
            if (!newCharacter.currentPlayerController)
            {
                // Switch to the new character
                if (currentCharacter) currentCharacter.currentPlayerController = null;
                newCharacter.currentPlayerController = this;
                currentCharacter = newCharacter;
                curCharacterIndex = index;
                return;
            }
        }
        
        
        Debug.LogError("Could not find a free character!");
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentCharacter)
        {
            Debug.LogError("PlayerController has no character!");
            return;
        }
        
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
            if (Input.GetKeyDown(KeyCode.LeftShift)) SelectNextCharacter();
            if (Input.GetKeyDown(KeyCode.Space)) currentCharacter.PlayInstrument();
        }
        Vector3 velocity = new Vector2(horizontal * Time.deltaTime, vertical * Time.deltaTime);
        currentCharacter.Move(velocity);
    }
}
