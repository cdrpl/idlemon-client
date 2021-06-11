using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// The resources directory where the audio clips are loaded from.
    /// </summary>
    const string DIRECTORY = "Sound/";

    public static AudioManager Instance { get; private set; }

    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup bgmMixer;

    AudioSource sfxAudioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        DontDestroyOnLoad(gameObject);
        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource.outputAudioMixerGroup = sfxMixer;
        sfxAudioSource.playOnAwake = false;
    }

    /// <summary>
    /// Will play the audio clip with the given name.
    /// </summary>
    public void Play(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(DIRECTORY + name);

        if (clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Unable to find audio clip named: " + name, this);
        }
    }
}
