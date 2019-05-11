
using UnityEngine;

public class BeatController : MonoBehaviour
{

    public const float bpm = 150;
    public const float bars = 8;
    public const float hits = 4;

    public const float margin = 0.2F;

    public float countdown { get; private set; }
    private const float countdown_max = (bars * hits) / (bpm / 60);

    // Start is called before the first frame update
    void Start()
    {
        resetCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0.0)
        {
            resetCountdown();
            foreach (var character in GameObject.FindGameObjectsWithTag("Player"))
            {
                var audio = character.GetComponent<AudioSource>();
                audio.Stop();
                audio.Play();
            }
        }
    }

    void resetCountdown()
    {
        countdown = countdown_max;
    }

    bool onBeat()
    {
        return countdown <= margin || countdown >= countdown_max - margin;
    }
}
