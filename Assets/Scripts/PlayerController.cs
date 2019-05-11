using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Vector3 playerNameOffset;
    private Character currentCharacter;
    private GameObject playerName;
    public int playerNum = 1;

    private bool useKeyboard = true;
    private float flyingNameTime = 0.2f;
    private bool animatingFlyingName = false;

    private int curCharacterIndex;

    
    // Start is called before the first frame update
    void Start()
    {
        playerName = Resources.Load<GameObject>("PlayerName");
        playerName = Instantiate(playerName);
        playerName.GetComponent<TextMesh>().text = "P" + playerNum.ToString();
        Color[] colors = new[] {Color.green, Color.blue, Color.yellow, Color.red};
        playerName.GetComponent<TextMesh>().color = colors[playerNum-1];
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
            if (!newCharacter.currentPlayerController && !newCharacter.dead)
            {
                // Switch to the new character
                if (currentCharacter) currentCharacter.currentPlayerController = null;
                newCharacter.currentPlayerController = this;
                currentCharacter = newCharacter;
                curCharacterIndex = index;
                StartCoroutine(FlyingName());
                return;
            }
        }
        
        
        Debug.LogError("Could not find a free character!");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCharacter && currentCharacter.dead) SelectNextCharacter();
        if (!currentCharacter)
        {
            Debug.LogError("PlayerController has no character!");
            return;
        }
        
        float vertical = Input.GetAxis("p" + playerNum.ToString() + "_joystick_horizontal");
        float horizontal = -Input.GetAxis("p" + playerNum.ToString() + "_joystick_vertical");
        if (Input.GetButtonDown("p" + playerNum.ToString() + "_trigger_back")) SelectNextCharacter();
        if (Input.GetButtonDown("p" + playerNum.ToString() + "_trigger_front")) currentCharacter.PlayInstrument();
        //        Vector3 velocity =
        //            new Vector3(-Input.GetAxis("p" + playerNum.ToString() + "_joystick_horizontal") * speed * Time.deltaTime,
        //                0, -Input.GetAxis("p" + playerNum.ToString() + "_joystick_vertical") * speed * Time.deltaTime);
        if (playerNum == 1)
        {
            if (useKeyboard)
            {
                if (Input.GetKey(KeyCode.W)) vertical = 1;
                if (Input.GetKey(KeyCode.S)) vertical = -1;
                if (Input.GetKey(KeyCode.A)) horizontal = -1;
                if (Input.GetKey(KeyCode.D)) horizontal = 1;
                if (Input.GetKeyDown(KeyCode.LeftShift)) SelectNextCharacter();
                if (Input.GetKeyDown(KeyCode.Space)) currentCharacter.PlayInstrument();
            } 
        }
        if (playerNum == 2)
        {
            if (useKeyboard)
            {
                if (Input.GetKey(KeyCode.I)) vertical = 1;
                if (Input.GetKey(KeyCode.K)) vertical = -1;
                if (Input.GetKey(KeyCode.J)) horizontal = -1;
                if (Input.GetKey(KeyCode.L)) horizontal = 1;
                if (Input.GetKeyDown(KeyCode.N)) SelectNextCharacter();
                if (Input.GetKeyDown(KeyCode.M)) currentCharacter.PlayInstrument();
            }
        }
        Vector3 velocity = new Vector2(horizontal * Time.deltaTime, vertical * Time.deltaTime);
        currentCharacter.Move(velocity);

        if (!animatingFlyingName)
            playerName.transform.position = GetPlayerTargetNamePos();
    }

    Vector3 GetPlayerTargetNamePos()
    {
        return currentCharacter.transform.position + playerNameOffset;
    }

    IEnumerator FlyingName()
    {
        if (animatingFlyingName) yield break; // Prevent multiple animations running simultaneously
        animatingFlyingName = true;
        Vector3 startPos = playerName.transform.position;
        float timer = 0;
        while (timer < flyingNameTime)
        {
            timer += Time.deltaTime;
            float i = timer / flyingNameTime;
            playerName.transform.position =
                Vector3.Lerp(startPos, GetPlayerTargetNamePos(), i);
            yield return null;
        }

        animatingFlyingName = false;
    }
}
