using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Unit inspector will hold an array of units that can be viewed as well as track the currently viewed unit.
/// </summary>
public class UnitInspector : MonoBehaviour
{
    /// <summary>
    /// Array of units that can be inspected.
    /// </summary>
    public Unit[] units { get; private set; }

    /// <summary>
    /// The index of the currently viewed unit.
    /// </summary>
    public int index { get; private set; }

    /// <summary>
    /// This is the currently viewed unit.
    /// </summary>
    public Unit Unit => units[index];

    /// <summary>
    /// Will trigger when the currently viewed unit is changed.
    /// </summary>
    public UnityEvent OnUnitChange;

    public void SetUnits(Unit[] units)
    {
        this.units = units;
        index = 0;

        if (units.Length < 1)
        {
            Debug.LogWarning("UnitInspector received an array of units with length less than 1");
        }
    }

    /// <summary>
    /// Will set the currently viewed unit.
    /// </summary>
    public void SetUnit(Unit unit)
    {
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].id == unit.id)
            {
                index = i;
            }
        }
    }

    public void NextUnit()
    {
        index++;

        if (index > units.Length - 1)
        {
            index = 0;
        }

        OnUnitChange.Invoke();
    }

    public void PreviousUnit()
    {
        index--;

        if (index < 0)
        {
            index = units.Length - 1;
        }

        OnUnitChange.Invoke();
    }
}
