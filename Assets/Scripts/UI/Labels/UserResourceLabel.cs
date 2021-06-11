using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base class for all money labels.
/// </summary>
public class UserResourceLabel : MonoBehaviour
{
    public Const.Resource type;
    public Text label;

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
        Global.Resource(type).OnAmountChange.AddListener(UpdateLabel);
        UpdateLabel(Global.Resource(type));
    }

    public void UpdateLabel(Resource resource)
    {
        label.text = Format(resource.amount);
    }

    /// <summary>
    /// Format 1,000,000 to 1M, ect.
    /// </summary>
    string Format(int number)
    {
        if (number > 999999999)
        {
            return (number / 1000000000f).ToString(".00B");
        }
        else if (number > 999999)
        {
            return (number / 1000000f).ToString(".00M");
        }

        return number.ToString();
    }
}
