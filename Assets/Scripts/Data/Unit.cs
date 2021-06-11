using UnityEngine;

public class Unit
{
    public string id;
    public int template;
    public int level;
    public int stars;
    public bool isLocked;

    public int curHp { get; private set; }
    public int hp => level * Template.hp;
    public int atk => level * Template.atk;
    public int def => level * Template.def;
    public int spd => level * Template.spd;

    public bool IsDead => curHp <= 0;
    public int PowerLevel => hp + atk + def + spd;
    public UnitTemplate Template => Global.UnitTemplates[template - 1];
    public Sprite Icon => Resources.Load<Sprite>("Unit Icons/" + template);

    /// <summary>
    /// This is the prefab of the unit that is shown during battle scenes.
    /// </summary>
    public GameObject Prefab => Resources.Load<GameObject>("Unit Prefabs/" + template);
}
