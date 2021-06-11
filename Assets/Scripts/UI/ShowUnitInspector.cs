using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will add the show unit inspector logic to buttons.
/// </summary>
public class ShowUnitInspector : MonoBehaviour
{
    public UnitInspector inspector;
    public UnitInspectorPanel inspectorPanel;

    public void AddShowUnitInspectorEvent(List<UnitIcon> icons)
    {
        foreach (var icon in icons)
        {
            icon.button.onClick.AddListener(() =>
            {
                AudioManager.Instance.Play("button");
                inspector.SetUnits(Global.Units);
                inspector.SetUnit(icon.unit);

                PanelManager.Instance.ChangePanel("Unit Inspector");
            });
        }
    }
}
