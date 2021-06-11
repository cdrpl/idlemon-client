using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignOutButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SceneManager.LoadScene("Sign In");
    }
}
