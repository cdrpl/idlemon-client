using UnityEngine;

/// <summary>
/// Will call panel manager change to on start. Meant to be used when a panel starts as active in the scene.
/// </summary>
public class PanelStartActive : MonoBehaviour
{
    public Panel panel;

    void Start()
    {
        foreach (var panel in PanelManager.Instance.panels)
        {
            if (this.panel != panel && panel.gameObject.activeInHierarchy)
            {
                panel.gameObject.SetActive(false);
            }
        }

        PanelManager.Instance.ChangePanel(panel);
    }
}
