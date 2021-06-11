using UnityEngine;

/// <summary>
/// Will auto login using the saved credentials.
/// Meant to be used during development and will only run in the Unity editor.
/// </summary>
public class AutoLogin : MonoBehaviour
{
    async void Awake()
    {
        if (Application.isEditor && Credentials.HasSavedCredentials && Global.User == null)
        {
            SignInResponse response = await SignInRequest.Send(Credentials.SavedEmail, Credentials.SavedPassword);

            if (response.HasError)
            {
                Debug.LogWarning("Auto login failed: " + response.Error);
            }
            else
            {
                // sign in successful. Set the global user values.
                Global.Init(response);
                Debug.Log("Auto login successful");
            }
        }

        Destroy(gameObject);
    }
}
