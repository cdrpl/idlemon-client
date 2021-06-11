using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the panel that appears when an HTTP request is waiting for a response.
/// </summary>
public class LoadingBar : MonoBehaviour
{
    public static LoadingBar Instance { get; private set; }

    public float loadingImageDelay = 0.75f; // delay in seconds before showing the loading image
    public float timeout = 10f; // disable the panel if timeout has been reached

    GameObject panel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            panel = transform.GetChild(0).gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show()
    {
        StartCoroutine(ShowLoadingImage(loadingImageDelay)); // show loading image
    }

    public void Hide()
    {
        StopAllCoroutines();
        panel.SetActive(false);
    }

    private IEnumerator ShowLoadingImage(float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(true);
    }
}
