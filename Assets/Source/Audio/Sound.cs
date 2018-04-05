using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public bool loop = false;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    [Range(0.1f, 3.0f)]
    public float pitch = 1.0f;

    [HideInInspector]
    public AudioSource source;
}
