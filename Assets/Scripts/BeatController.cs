
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        countdown_loop -= Time.deltaTime;
        if (countdown_loop < 0)
        {
            countdown_loop = loop_length;
            foreach (var character in FindObjectsOfType<Character>())
            {
                var audio = character.GetComponent<AudioSource>();
                audio.Stop();
                audio.Play();
            }
        }

        countdown_beat -= Time.deltaTime;
        if (countdown_beat < 0)
        {
            Debug.Log("Beat");
            countdown_beat = beat_length;
        }
    }

    public bool onBeat()
    {
        return countdown_beat <= margin || countdown_beat >= beat_length - margin;
    }
}
