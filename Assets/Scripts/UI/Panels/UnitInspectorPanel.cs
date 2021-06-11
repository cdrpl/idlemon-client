using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI panel for displaying a unit's details.
/// </summary>
public class UnitInspectorPanel : Panel
{
    public UnitInspector unitInspector;

    // UI elements
    public Text unitName;
    public Text unitType;
    public Text unitLevel;
    public Text unitPower;
    public Text hp;
    public Text atk;
    public Text def;
    public Text spd;

    /// <summary>
    /// Cache the last spawned unit prefab.
    /// </summary>
    GameObject unitPrefab;

    void Awake()
    {
        unitInspector.OnUnitChange.AddListener(ReDraw);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        ReDraw();
    }

    public override void Hide()
    {
        DestroyGraphics();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Will redraw the UI based on the currently viewed unit in the unit inspector.
    /// </summary>
    public void ReDraw()
    {
        Unit unit = unitInspector.Unit;

        // update the UI
        unitName.text = unit.Template.name;
        unitType.text = unit.Template.type;
        unitLevel.text = "LV: " + unit.level.ToString() + "/255";


        // stats UI
        hp.text = unit.hp.ToString();
        atk.text = unit.atk.ToString();
        def.text = unit.def.ToString();
        spd.text = unit.spd.ToString();

        SpawnUnitPrefab();
    }

    void SpawnUnitPrefab()
    {
        DestroyGraphics();

        // determine graphics game obj
        GameObject unitPrefab = unitInspector.Unit.Prefab;

        // calculate graphics spawn pos
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(0.25f, 0.4f, 0));

        // instantiate graphics
        this.unitPrefab = Instantiate<GameObject>(unitPrefab, spawnPos, Quaternion.identity);
    }

    public void DestroyGraphics()
    {
        if (unitPrefab != null)
        {
            Destroy(unitPrefab);
        }
    }
}
