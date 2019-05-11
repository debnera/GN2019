using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    bool[] c;
    public int numPlayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        c = new bool[4];

        //CreatePlayer(transform.parent.GetChild(1).GetComponent<StartMenu>().controller);
    }

    void CreatePlayer(int i)
    {
        c[i - 1] = true;
        numPlayer++;
        //GameObject newPlayer = Instantiate(player);
        //newPlayer.GetComponent<PlayerController>().playerController = "c" + i + "_";
        GameObject obj = Resources.Load<GameObject>("PlayerController_"+numPlayer);
        obj = Instantiate(obj, transform.parent);
        obj.GetComponent<PlayerController>().playerNum = numPlayer;
        obj.GetComponent<PlayerController>().playerController = i;
        //newPlayer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Doggy" + numPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i < c.Length+1; i++)
        {
            if(Input.GetButtonDown("p" + i + "_trigger_front"))
            {
                if (c[i - 1] == false)
                {
                    CreatePlayer(i);
                }
            }
        }
    }
}
