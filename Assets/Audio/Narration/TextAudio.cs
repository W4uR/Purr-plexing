using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Text Audio", menuName = "Audio/TextAudio")]
public class TextAudio : ScriptableObject
{
    public enum Phrase
    {
        PLAY,
        QUIT
    }

    [System.Serializable]
    public class PhraseData
    {
        public Phrase phrase;
        public AudioClip audio;
    }

    public PhraseData[] phrases;

    public AudioClip GetAuidoForPhrase(Phrase phrase)
    {
        return phrases.Where(d => d.phrase.Equals(phrase)).First().audio;
    }
}
