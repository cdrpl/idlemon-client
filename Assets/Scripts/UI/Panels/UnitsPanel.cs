using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Scroll panel that displays the user's units.
/// </summary>
public class UnitsPanel : Panel
{
    public Transform content;
    public GameObject unitIconPrefab;

    /// <summary>
    /// List of all the spawned scroll items.
    /// </summary>
    List<UnitIcon> unitIcons = new List<UnitIcon>();

    /// <summary>
    /// Will give the List of the redrawn unit icons.
    /// </summary>
    public UnityEvent<List<UnitIcon>> OnRedraw;

    void Start()
    {
        Draw();
    }

    void OnEnable()
    {
        content.localPosition = new Vector3(0, -100, 0);
    }

    /// <summary>
    /// Draw the scroll items.
    /// </summary>
    public void Draw()
    {
        foreach (Unit unit in Global.Units)
        {
            // spawn scroll item
            var scrollItem = GameObject.Instantiate<GameObject>(this.unitIconPrefab);
            scrollItem.transform.SetParent(content);
            scrollItem.transform.localScale = Vector3.one;

            // update unit icon
            var icon = scrollItem.GetComponent<UnitIcon>();
            icon.SetUnit(unit);
            unitIcons.Add(icon);
        }

        OnRedraw.Invoke(unitIcons);
    }
}
