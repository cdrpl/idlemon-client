using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

/// <summary>
/// Script to check that the client version matches the expected version received from the server.
/// </summary>
public class VerifyClient : MonoBehaviour
{
    public string sceneName = "Sign In"; // scene to load if verification is successful
    public GameObject outdatedPanel;
    public GameObject errorPanel;

    async void Start()
    {
        await SendHTTPRequest();
    }

    public async Task SendHTTPRequest()
    {
        LoadingBar.Instance.Show();

        try
        {
            VersionResponse response = await new VersionRequest().Send();

            if (Application.genuineCheckAvailable)
            {
                if (Application.genuine && response.client == Application.version)
                {
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    outdatedPanel.SetActive(true);
                }
            }
            else
            {
                if (response.client == Application.version)
                {
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    outdatedPanel.SetActive(true);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            errorPanel.SetActive(true);
        }
        finally
        {
            LoadingBar.Instance.Hide();
        }
    }

    /// <summary>
    /// Fire and forget version of SendHTTPRequest.
    /// </summary>
    public async void ResendHTTPRequest()
    {
        await SendHTTPRequest();
    }
}
