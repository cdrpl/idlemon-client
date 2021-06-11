using UnityEngine;
using UnityEngine.UI;

public class UsernameLabel : MonoBehaviour
{
    Text label;

    void Awake()
    {
        label = GetComponent<Text>();
    }

    void Start()
    {
        if (Global.IsInit)
        {
            UpdateLabel();
        }
        else
        {
            Global.OnUserInit.AddListener(UpdateLabel);
        }
    }

    void UpdateLabel()
    {
        label.text = Global.User.name;
    }
}
