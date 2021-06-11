using UnityEngine;

public class Panel : MonoBehaviour
{
    public string panelName;

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
