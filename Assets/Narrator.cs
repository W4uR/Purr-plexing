using UnityEngine;

public class Narrator : Singleton<Narrator>
{
    [SerializeField]
    AudioSource speaker;
    [SerializeField]
    TextAudio englishTextAudio;

    void Start()
    {
        current = englishTextAudio;
    }

    TextAudio current;

    public void Speak(TextAudio.Phrase phrase)
    {
        speaker.PlayOneShot(current.GetAuidoForPhrase(phrase));
    }
}
