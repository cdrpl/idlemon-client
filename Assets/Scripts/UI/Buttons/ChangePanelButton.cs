using UnityEngine;
using UnityEngine.UI;

public class ChangePanelButton : MonoBehaviour
{
    public string panel;
    public bool previous;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (previous)
        {
            PanelManager.Instance.ChangePanelPrevious();
        }
        else
        {
            PanelManager.Instance.ChangePanel(panel);
        }
    }
}
