
using UnityEngine;

public class BeatController : MonoBehaviour
{

    public const float bpm = 150;
    public const float bars = 8;
    public const float hits = 4;

    public const float margin = 0.2F;

    public float countdown_loop { get; private set; } = 0;
    private const float loop_length = (bars * hits) / (bpm / 60);

    public float countdown_beat { get; private set; } = 0;
    private const float beat_length = hits / (bpm / 60);

    public float countdown_halfBeat { get; private set; } = 0;

    private bool overHalf = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        countdown_halfBeat -= Time.deltaTime;
        if (countdown_halfBeat < 0)
        {
            Debug.Log("halfBeat");
            countdown_halfBeat = beat_length*0.5f;
            Animate();
        }

        countdown_beat -= Time.deltaTime;
        if (countdown_beat < 0)
        {
            Debug.Log("Beat");
            countdown_beat = beat_length;
        }

        countdown_loop -= Time.deltaTime;
        if (countdown_loop < 0)
        {
            countdown_loop = loop_length;
            countdown_beat = beat_length;
            countdown_halfBeat = beat_length * 0.5f;
            foreach (var character in FindObjectsOfType<Character>())
            {
                var audio = character.GetComponent<AudioSource>();
                audio.Stop();
                audio.Play();
            }
        }
    }

    private void Animate()
    {
        SpriteSwapper[] swaps = FindObjectsOfType<SpriteSwapper>();
        foreach (SpriteSwapper swap in swaps)
        {
            swap.SwapImage();
        }
    }

    public bool onBeat()
    {
        return countdown_beat <= margin || countdown_beat >= beat_length - margin;
    }
}
