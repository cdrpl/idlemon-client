using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for displaying unit icon.
/// </summary>
public class UnitIcon : MonoBehaviour
{
    public Image icon;
    public Text level;
    public Button button;
    public Unit unit { get; private set; } // the displayed unit

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;

        if (unit != null)
        {
            icon.sprite = unit.Icon;
            level.text = unit.level.ToString();
        }
    }

    /// <summary>
    /// Enable/disable unit icon children.
    /// </summary>
    public void EnableChildren(bool enable)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(enable);
        }
    }
}
