using UnityEngine;
using UnityEngine.UI;

public class UserExpLabel : MonoBehaviour
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
            Init();
        }
        else
        {
            Global.OnUserInit.AddListener(Init);
        }
    }

    void Init()
    {
        UpdateLabel(Global.User.exp);
        Global.User.OnExpGain.AddListener(UpdateLabel);
    }

    void UpdateLabel(int exp)
    {
        label.text = "Exp: " + exp.ToString();
    }
}
