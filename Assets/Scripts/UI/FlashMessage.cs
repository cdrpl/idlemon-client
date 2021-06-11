using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Used to flash messages to the player.
/// </summary>
public class FlashMessage : MonoBehaviour
{
    public static FlashMessage Instance { get; private set; }

    public float lifetime = 2.5f;

    GameObject panel;
    Text text;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            panel = transform.GetChild(0).gameObject;
            text = panel.transform.GetChild(0).GetComponent<Text>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Flash(string message)
    {
        text.text = message;
        Show();

        StopAllCoroutines();
        StartCoroutine(R_FadeOut());
    }

    void Show()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    IEnumerator R_FadeOut()
    {
        yield return new WaitForSeconds(lifetime);
        Hide();
    }
}
