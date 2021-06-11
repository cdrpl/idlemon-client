using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this to a button.
/// It will play the button sound effect when the button is clicked.
/// </summary>
public class PlayButtonAudio : MonoBehaviour
{
    const string AUDIO_CLIP_NAME = "button";

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayAudio);
    }

    public void PlayAudio()
    {
        AudioManager.Instance.Play(AUDIO_CLIP_NAME);
    }
}
