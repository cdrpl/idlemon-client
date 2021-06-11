using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace Idlemon.UI
{
    public class SignUpForm : MonoBehaviour
    {
        public InputField username, email, password, password2;
        public Button signUpBtn;

        /// <summary>
        /// Triggered on user sign up success.
        /// </summary>
        public UnityEvent OnSignUpSuccess;

        void Awake()
        {
            signUpBtn.onClick.AddListener(OnBtnClick);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                signUpBtn.onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (EventSystem.current.currentSelectedGameObject == username.gameObject)
                {
                    SelectInput(email);
                }
                else if (EventSystem.current.currentSelectedGameObject == email.gameObject)
                {
                    SelectInput(password);
                }
                else if (EventSystem.current.currentSelectedGameObject == password.gameObject)
                {
                    SelectInput(password2);
                }
                else
                {
                    SelectInput(username);
                }
            }
        }

        void OnEnable()
        {
#if UNITY_EDITOR || !UNITY_ANDROID
            username.Select();
#endif
        }

        async void OnBtnClick()
        {
            // validate input fields
            if (username.text == string.Empty) // username is required
            {
                SelectInput(username);
                FlashMessage.Instance.Flash("username is required");
                return;
            }
            else if (email.text == string.Empty) // email is required
            {
                SelectInput(email);
                FlashMessage.Instance.Flash("email is required");
                return;
            }
            else if (password.text == string.Empty) // password is required
            {
                SelectInput(password);
                FlashMessage.Instance.Flash("password is required");
                return;
            }
            else if (password.text.Length < 8) // password must be 8 chars long
            {
                SelectInput(password);
                FlashMessage.Instance.Flash("password must contain 8 characters");
                return;
            }
            else if (password.text != password2.text) // passwords must match
            {
                SelectInput(password2);
                FlashMessage.Instance.Flash("passwords do not match");
                return;
            }

            try
            {
                LoadingBar.Instance.Show();

                var response = await SignUpRequest.Send(username.text, email.text, password.text);

                if (response.HasError)
                {
                    if (response.status == -1)
                    {
                        FlashMessage.Instance.Flash("email is already taken, choose a different email");
                    }
                    else if (response.status == -2)
                    {
                        FlashMessage.Instance.Flash("name is already taken, choose a different name");
                    }
                    else
                    {
                        FlashMessage.Instance.Flash(response.Error);
                    }
                }
                else
                {
                    ClearInputs();
                    OnSignUpSuccess.Invoke();
                    FlashMessage.Instance.Flash("Sign up successful");
                }
            }
            catch (Exception e)
            {
                FlashMessage.Instance.Flash(e.Message);
            }
            finally
            {
                LoadingBar.Instance.Hide();
            }
        }

        /// <summary>
        /// Select the specified input.
        /// </summary>
        void SelectInput(InputField input)
        {
            input.ActivateInputField();
            input.Select();
        }

        /// <summary>
        /// Clears the input fields of the form.
        /// </summary>
        void ClearInputs()
        {
            username.text = string.Empty;
            email.text = string.Empty;
            password.text = string.Empty;
            password2.text = string.Empty;
        }
    }
}
