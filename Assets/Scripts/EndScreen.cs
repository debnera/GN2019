
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text killCounterText;
    
    // Update is called once per frame
    void Update()
    {
        if (killCounterText) killCounterText.text = "You defeated " + Enemy.killCounter + " threats!";
        if (Input.GetKey(KeyCode.Space))
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }
}
