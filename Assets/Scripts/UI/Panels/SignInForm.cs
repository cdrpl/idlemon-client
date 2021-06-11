using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SignInForm : MonoBehaviour
{
    public InputField email, password;
    public Toggle rememberMe;
    public Button signInBtn;

    void Start()
    {
        signInBtn.onClick.AddListener(OnBtnClick);

        // Fill in form is remember me data is present
        if (Credentials.HasSavedCredentials)
        {
            rememberMe.isOn = true;
            email.text = Credentials.SavedEmail;
            password.text = Credentials.SavedPassword;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            signInBtn.onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (EventSystem.current.currentSelectedGameObject == email.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(password.gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(email.gameObject);
            }
        }
    }

    /// <summary>
    /// Triggered when the sign in button is clicked.
    /// </summary>
    public async void OnBtnClick()
    {
        // validate the form inputs
        if (email.text.Length < 2)
        {
            FlashMessage.Instance.Flash("username must be longer than 1 character");
            return;
        }
        else if (password.text.Length < 8)
        {
            FlashMessage.Instance.Flash("password must have at least 8 characters");
            return;
        }

        LoadingBar.Instance.Show();

        try
        {
            SignInResponse response = await SignInRequest.Send(email.text, password.text);

            if (response.HasError)
            {
                FlashMessage.Instance.Flash(response.Error);
                Credentials.Delete();
            }
            else
            {
                // sign in successful. Set the global user values.
                Global.Init(response);

                Credentials.UpdatePlayerPrefs(email.text, password.text, rememberMe.isOn);

                SceneManager.LoadScene("Overworld");
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            FlashMessage.Instance.Flash(e.Message);
        }
        finally
        {
            LoadingBar.Instance.Hide();
        }
    }
}
